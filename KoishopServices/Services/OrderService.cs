using AutoMapper;
using DTOs.KoiFish;
using DTOs.Order;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopBusinessObjects.VnPayModel;
using KoishopRepositories.Interfaces;
using KoishopServices.Common.Exceptions;
using KoishopServices.Dtos.Order;
using KoishopServices.Interfaces;
using KoishopServices.Interfaces.Third_Party;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    public OrderService(IMapper mapper
        , IOrderRepository orderRepository
        , UserManager<User> userManager
        , IOrderItemRepository orderItemRepository
        , IKoiFishRepository koiFishRepository
        , IVnPayService vnPayService
        , IHttpContextAccessor httpContextAccessor)
    {
        this._mapper = mapper;
        this._userManager = userManager;
        this._orderRepository = orderRepository;
        this._orderItemRepository = orderItemRepository;
        this._koiFishRepository = koiFishRepository;
        this._vnPayService = vnPayService;
        this._httpContextAccessor = httpContextAccessor;
    }
    public async Task<string> AddOrder(OrderCreationDto orderCreationDto, CancellationToken cancellationToken)
    {
        //TODO: Add validation before create and mapping
        var customer = await _userManager.FindByIdAsync(orderCreationDto.UserId.ToString());
        if (customer == null)
        {
            throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST);
        }
        var order = _mapper.Map<Order>(orderCreationDto);
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
        }
        order.OrderItems = orderItems;
        order.Quantity = orderItems.Count;
        foreach (var orderItem in orderItems)
        {
            await _orderItemRepository.AddAsync(orderItem);
        }
        // Total ammount -> tính tiền
        order.TotalAmount = 10000; // chưa có func của Ngọc
        await _orderRepository.UpdateAsync(order);
        PaymentInformationModel paymentInformationModel = new PaymentInformationModel
        {
            Amount = (double)order.TotalAmount,
            OrderId = order.Id.ToString(),
        };
        var httpContext = _httpContextAccessor.HttpContext;
        var paymentUrl = _vnPayService.CreatePaymentUrl(paymentInformationModel, httpContext);
        return paymentUrl;
    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order == null)
            return null;
        return _mapper.Map<OrderDto>(order);
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

    public async Task<bool> UpdateOrder(int id, OrderUpdateDto orderUpdateDto)
    {
        var existingOrder = await _orderRepository.GetByIdAsync(id);
        if (existingOrder == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(orderUpdateDto, existingOrder);
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
}
