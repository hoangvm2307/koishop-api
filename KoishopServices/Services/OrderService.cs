using AutoMapper;
using DTOs.KoiFish;
using DTOs.Order;
using DTOs.OrderItem;
using DTOs.Rating;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopBusinessObjects.VnPayModel;
using KoishopRepositories.Interfaces;
using KoishopServices.Common.Exceptions;
using KoishopServices.Common.Interface;
using KoishopServices.Common.Pagination;
using KoishopServices.Dtos.Order;
using KoishopServices.Dtos.Rating;
using KoishopServices.Interfaces;
using KoishopServices.Interfaces.Third_Party;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace KoishopServices.Services;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IKoiFishRepository _koiFishRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IVnPayService _vnPayService;
    private readonly ICurrentUserService _currentUserService;


    public OrderService(IMapper mapper
        , IOrderRepository orderRepository
        , UserManager<User> userManager
        , IOrderItemRepository orderItemRepository
        , IKoiFishRepository koiFishRepository
        , IVnPayService vnPayService
        , IHttpContextAccessor httpContextAccessor
        , ICurrentUserService currentUserService)
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._orderRepository = orderRepository;
        this._orderItemRepository = orderItemRepository;
        this._koiFishRepository = koiFishRepository;
        this._vnPayService = vnPayService;
        this._httpContextAccessor = httpContextAccessor;
        this._currentUserService = currentUserService;
    }
    public async Task<OrderDto> AddOrder(OrderCreationDto orderCreationDto, CancellationToken cancellationToken)
    {
        //TODO: Add validation before create and mapping
        var customer = await _userManager.FindByIdAsync(_currentUserService.UserId);
        if (customer == null)
        {
            throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST);
        }
        var koiFishIds = orderCreationDto.OrderItemCreationDtos.Select(x => x.KoiFishId).Where(id => id.HasValue).Distinct();
        if (koiFishIds.Count() > 1)
        {
            throw new DuplicationException(ExceptionConstants.ORDER_ITEM_DUPLICATE_INPUT_KOIFISH_ID);
        }

        var order = _mapper.Map<Order>(orderCreationDto);
        order.UserId = customer.Id;
        order.OrderDate = DateTime.UtcNow;
        order.Status = OrderStatus.PENDING;
        await _orderRepository.AddAsync(order);
        List<OrderItem> orderItems = new List<OrderItem>();
        foreach(var orderItemDto in orderCreationDto.OrderItemCreationDtos)
        {
            var koiFish = await _koiFishRepository.FindAsync(x => x.Id == orderItemDto.KoiFishId && x.isDeleted == false, cancellationToken);
            if (koiFish == null)
            {
                throw new NotFoundException(ExceptionConstants.KOIFISH_NOT_EXIST + orderItemDto.KoiFishId);
            }
            if (koiFish.Status != KoiFishStatus.AVAILABLE)
            {
                throw new ValidationException(ExceptionConstants.INVALID_KOIFISH_STATUS);
            }
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            orderItem.Price = koiFish.Price; // check lại
            orderItem.OrderId = order.Id;
            orderItems.Add(orderItem);
            await _orderItemRepository.AddAsync(orderItem);
        }
        order.OrderItems = orderItems;
        order.Quantity = orderItems.Count;
        // Total ammount -> tính tiền
        order.TotalAmount = 10000; // chưa có func của Ngọc
        await _orderRepository.UpdateAsync(order);
        await _userManager.UpdateAsync(customer);
        order.User = customer;

        var koifishName = await _koiFishRepository.FindAllToDictionaryAsync(x => x.isDeleted == false, x => x.Id, x => x.Name, cancellationToken);
        return order.MapToOrderDto(_mapper, customer.UserName, koifishName);
    }

    public async Task<OrderDto> GetOrderById(int id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindAsync(order => order.Id == id && order.isDeleted == false,
                q => q.Include(item => item.OrderItems.Where(i => i.isDeleted == false))
                        .ThenInclude(item => item.KoiFish)
                        .Include(customer => customer.User), cancellationToken);
        Dictionary<int, string> users = new Dictionary<int, string>();
        if (order == null)
            return null;
        var koifishName = await _koiFishRepository.FindAllToDictionaryAsync(x => x.isDeleted == false, x => x.Id, x => x.Name, cancellationToken);
        return order.MapToOrderDto(_mapper, order.User.UserName, koifishName);
    }

    public async Task<IEnumerable<OrderDto>> GetListOrder()
    {
        var orders = await _orderRepository.GetAllAsync();
        var result = _mapper.Map<List<OrderDto>>(orders);
        return result;
    }

    public async Task<bool> RemoveOrder(int id)
    {
        var exist = await _orderRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _orderRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateOrderItem(int id, OrderUpdateItemDto orderUpdateDto, CancellationToken cancellationToken)
    {
        var existingOrder = await _orderRepository.FindAsync(order => order.Id == id && order.isDeleted == false,
                q => q.Include(item => item.OrderItems.Where(i => i.isDeleted == false))
                        .Include(customer => customer.User), cancellationToken);
        if (existingOrder == null)
        {
            throw new NotFoundException(ExceptionConstants.ORDER_NOT_EXIST + id);
        }
        var existingOrderItems = existingOrder.OrderItems.ToDictionary(item => item.KoiFishId);
        var updatedOrderItems = orderUpdateDto.OrderItemCreationDtos.ToDictionary(updateItem => updateItem.KoiFishId);
        //Check để xóa orderItem không còn tồn tại trong đon thanh toán
        foreach (var orderItem in existingOrderItems)
        {
            if (!updatedOrderItems.ContainsKey(orderItem.Key))
            {
                orderItem.Value.isDeleted = true;
                await _orderItemRepository.UpdateAsync(orderItem.Value);
            }
        }
        // Add item mới vào order
        foreach (var updateItem in updatedOrderItems)
        {
            if (!existingOrderItems.ContainsKey(updateItem.Key))
            {
                var koiFish = await _koiFishRepository.FindAsync(x => x.Id == updateItem.Value.KoiFishId && x.isDeleted == false, cancellationToken);
                if (koiFish == null)
                {
                    throw new NotFoundException(ExceptionConstants.KOIFISH_NOT_EXIST + updateItem.Value.KoiFishId);
                }
                if (koiFish.Status != KoiFishStatus.AVAILABLE)
                {
                    throw new ValidationException(ExceptionConstants.INVALID_KOIFISH_STATUS);
                }
                var newOrderItem = _mapper.Map<OrderItem>(updateItem.Value);
                newOrderItem.Price = koiFish.Price; // check lại
                existingOrder.OrderItems.Add(newOrderItem);
                await _orderItemRepository.AddAsync(newOrderItem);
            }
        }
        //TODO: tính lại total price của order - Đợi Ngọc
        await _orderRepository.UpdateAsync(existingOrder);
        return true;
    }

    public async Task<bool> AfterPaymentSuccess(int id, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindAsync(x => x.Id ==  id && x.isDeleted == false, cancellationToken);
        if (order == null)
        {
            throw new NotFoundException(ExceptionConstants.ORDER_NOT_EXIST + id);
        }
        if (order.Status != OrderStatus.PENDING)
        {
            throw new ValidationException(ExceptionConstants.INVALID_ORDER_STATUS);
        }
        List<KoiFish> koifishes = new List<KoiFish>();
        var orderItems = await _orderItemRepository.FindAllAsync(x => x.OrderId == order.Id && x.isDeleted == false, cancellationToken);
        foreach (var orderItem in orderItems)
        {
            var koifish = await _koiFishRepository.FindAsync(x => x.Id == orderItem.KoiFishId && x.isDeleted == false, cancellationToken);
            // Note: thông thường, nếu Order Item tồn tại mà con cá mất tiu -> phải refund vì đã thanh toán rồi mới gọi API này
            if (koifish == null)
            {
                throw new NotFoundException(ExceptionConstants.KOIFISH_NOT_EXIST + orderItem.KoiFishId);
            }
            if (koifish.Status != KoiFishStatus.AVAILABLE)
            {
                throw new ValidationException(ExceptionConstants.INVALID_KOIFISH_STATUS);
            }           
            koifishes.Add(koifish);
        }
        foreach (var koi in koifishes)
        {
            koi.Status = KoiFishStatus.SOLD;
            koi.DateModified = DateTime.Now;
            await _koiFishRepository.UpdateAsync(koi);
        }
        order.Status = OrderStatus.PROCESSING;
        order.DateModified = DateTime.Now;
        await _orderRepository.UpdateAsync(order);
        return true;
    }

    public async Task<bool> UpdateOrderStatus(OrderStatusUpdateDto orderStatusUpdateDto, CancellationToken cancellationToken)
    {
        if (!new[] { OrderStatus.PENDING, OrderStatus.PROCESSING, OrderStatus.DELIVERING, OrderStatus.DELIVERED, OrderStatus.CANCELLED }.Contains(orderStatusUpdateDto.Status))
        {
            throw new ValidationException(ExceptionConstants.INVALID_ORDER_STATUS);
        }
        var order = await _orderRepository.FindAsync(x => x.Id ==  orderStatusUpdateDto.Id && x.isDeleted == false, cancellationToken);
        if (order == null)
        {
            throw new NotFoundException(ExceptionConstants.ORDER_NOT_EXIST + orderStatusUpdateDto.Id);
        }
        order.Status = orderStatusUpdateDto.Status;
        await _orderRepository.UpdateAsync(order);
        return true;
    }

    public async Task<PagedResult<OrderDto>> FilterOrder(FilterOrderDto filterOrderDto, CancellationToken cancellationToken)
    {    
        Func<IQueryable<Order>, IQueryable<Order>> queryOptions = query =>
        {
            query = query.Include(item => item.OrderItems.Where(i => i.isDeleted == false))
                        .ThenInclude(item => item.KoiFish)
                        .Include(customer => customer.User);
            query = query.Where(x => x.isDeleted == false);
            if (filterOrderDto.UserId != -1)
            {
                query = query.Where(x => x.UserId == filterOrderDto.UserId);
            }
            if (filterOrderDto.Quantity != -1)
            {
                query = query.Where(x => x.Quantity == filterOrderDto.Quantity);
            }
            if (!string.IsNullOrEmpty(filterOrderDto.Status))
            {
                query = query.Where(x => x.Status.Contains(filterOrderDto.Status));
            }
            if (!string.IsNullOrEmpty(filterOrderDto.OrderDate))
            {
                DateTime date = DateTime.ParseExact(filterOrderDto.OrderDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None);
                query = query.Where(x => x.DateCreated.Date == date);
            }
            if (!string.IsNullOrEmpty(filterOrderDto.SortBy))
            {
                query = filterOrderDto.IsDescending
                    ? query.OrderByDescending(e => EF.Property<object>(e, filterOrderDto.SortBy))
                    : query.OrderBy(e => EF.Property<object>(e, filterOrderDto.SortBy));
            }         
            return query;
        };

        var result = await _orderRepository.FindAllAsync(filterOrderDto.PageNumber, filterOrderDto.PageSize, queryOptions, cancellationToken);
        var koifishName = await _koiFishRepository.FindAllToDictionaryAsync(x => x.isDeleted == false, x => x.Id, x => x.Name, cancellationToken);
        Dictionary<int, string> users = new Dictionary<int, string>();
        foreach(var order in result)
        {
            if (!users.ContainsValue(order.User.UserName))
            {
                users.Add((int)order.UserId, order.User.UserName);
            }
        }
        if (result == null)
            return null;
        return PagedResult<OrderDto>.Create(
               totalCount: result.TotalCount,
               pageCount: result.PageCount,
               pageSize: result.PageSize,
               pageNumber: result.PageNo,
               data: result.MapToOrderDtoList(_mapper, users, koifishName));
    }
}
