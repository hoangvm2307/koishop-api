using AutoMapper;
using DTOs.Order;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopRepositories.Interfaces;
using KoishopServices.Common.Exceptions;
using KoishopServices.Common.Interface;
using KoishopServices.Common.Pagination;
using KoishopServices.Dtos.Dashboard;
using KoishopServices.Dtos.Order;
using KoishopServices.Interfaces;
using KoishopServices.Interfaces.Third_Party;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UberSystem.Domain.Interfaces.Services;

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
    private readonly IConsignmentItemRepository _consignmentItemRepository;
    private readonly IConsignmentRepository _consignmentRepository;
    private readonly IEmailService _emailService;

    public OrderService(IMapper mapper
        , IOrderRepository orderRepository
        , UserManager<User> userManager
        , IOrderItemRepository orderItemRepository
        , IKoiFishRepository koiFishRepository
        , IVnPayService vnPayService
        , IHttpContextAccessor httpContextAccessor
        , ICurrentUserService currentUserService
        , IConsignmentItemRepository consignmentItemRepository
        , IConsignmentRepository consignmentRepository
        , IEmailService emailService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _koiFishRepository = koiFishRepository;
        _vnPayService = vnPayService;
        _httpContextAccessor = httpContextAccessor;
        _currentUserService = currentUserService;
        _consignmentItemRepository = consignmentItemRepository;
        _consignmentRepository = consignmentRepository;
        _emailService = emailService;
    }
    public async Task<OrderDto> AddOrder(OrderCreationDto orderCreationDto, CancellationToken cancellationToken)
    {
        //TODO: Add validation before create and mapping
        var customer = await _userManager.FindByIdAsync(_currentUserService.UserId);
        if (customer == null)
        {
            throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST);
        }
        var koiFishIds = orderCreationDto.OrderItemCreationDtos
                            .Where(x => x.KoiFishId.HasValue)
                            .Select(x => x.KoiFishId.Value)
                            .ToList();

        if (koiFishIds.Count != koiFishIds.Distinct().Count())
        {
            throw new DuplicationException(ExceptionConstants.ORDER_ITEM_DUPLICATE_INPUT_KOIFISH_ID);
        }

        var order = _mapper.Map<Order>(orderCreationDto);
        order.UserId = customer.Id;
        order.OrderDate = DateTime.UtcNow;
        order.Status = OrderStatus.PENDING;
        await _orderRepository.AddAsync(order);

        List<OrderItem> orderItems = new List<OrderItem>();
        decimal totalAmount = 0;
        foreach (var orderItemDto in orderCreationDto.OrderItemCreationDtos)
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
            if (koiFish.UserId.HasValue)
            {
                orderItem.Type = OrderItemType.CONSIGNMENT_ONLINE;
            }
            else
            {
                orderItem.Type = OrderItemType.SHOP_OWNER;
            }
            orderItem.Price = koiFish.Price;
            totalAmount += koiFish.Price;
            orderItem.OrderId = order.Id;
            orderItems.Add(orderItem);
            await _orderItemRepository.AddAsync(orderItem);
        }
        order.OrderItems = orderItems;
        order.Quantity = orderItems.Count;
       /* if (!order.IsConsignment)*/ order.TotalAmount = order.Quantity >= 10 ? totalAmount * CostConstant.WHOLESALE_DISCOUNT : totalAmount;

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
                if (koiFish.UserId.HasValue)
                {
                    newOrderItem.Type = OrderItemType.CONSIGNMENT_ONLINE;
                }
                else
                {
                    newOrderItem.Type = OrderItemType.SHOP_OWNER;
                }
                newOrderItem.Price = koiFish.Price; // check lại
                existingOrder.OrderItems.Add(newOrderItem);
                await _orderItemRepository.AddAsync(newOrderItem);
            }
        }

        decimal totalAmount = 0;
        foreach (var item in existingOrder.OrderItems)
        {
            if (item.isDeleted == false)
            {
                totalAmount += item.Price;
            }
        }
        if (!existingOrder.IsConsignment) 
            existingOrder.TotalAmount = existingOrder.Quantity >= 10 ? totalAmount * CostConstant.WHOLESALE_DISCOUNT : totalAmount;
        await _orderRepository.UpdateAsync(existingOrder);
        return true;
    }

    public async Task<bool> AfterPaymentSuccess(int id, CancellationToken cancellationToken)
    {
        var customer = await _userManager.FindByIdAsync(_currentUserService.UserId);
        if (customer == null)
        {
            throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST + _currentUserService.UserId);
        }
        var order = await _orderRepository.FindAsync(x => x.Id == id && x.isDeleted == false, cancellationToken);
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
        //Handle ký gửi
        switch (order.IsConsignment)
        {
            case false:
                order.Status = OrderStatus.PROCESSING;
                foreach (var koi in koifishes)
                {
                    koi.Status = KoiFishStatus.SOLD;
                    koi.DateModified = DateTime.Now;
                    // trả tiền cho chủ cá
                    if (koi.UserId != null)
                    {
                        var fishOwner = await _userManager.FindByIdAsync(koi.UserId.ToString());
                        fishOwner.Wallet += koi.Price - (koi.Price * CostConstant.COMMISSION_CHARGE);

                        var currentConsignment = await _consignmentRepository.FindAsync(consignment => consignment.UserID == koi.UserId 
                            && consignment.ConsignmentType == ConsignmentType.ONLINE
                            && consignment.Status == ConsignmentStatus.APPROVED
                            && consignment.EndDate >= DateTime.UtcNow
                            && consignment.ConsignmentItems.Any(item => item.KoiFishId == koi.Id));
                        currentConsignment.EndDate = DateTime.UtcNow;
                        currentConsignment.Status = ConsignmentStatus.COMPLETED;
                        await _consignmentRepository.UpdateAsync(currentConsignment);
                        await _userManager.UpdateAsync(fishOwner);
                    }
                    // Set owner mới cho cá
                    koi.UserId = customer.Id;
                    // Này chạy happy case chứ kh có UnitOfWork thì sẽ xảy ra chuyện con được con mất
                    await _koiFishRepository.UpdateAsync(koi);
                }
                await _emailService.SendEmailVerificationAsync(customer.Email, koifishes, order);
                break;
            case true:
                // khởi tạo consignment mới
                Consignment consignment = new Consignment
                {
                    ConsignmentType = ConsignmentType.OFFLINE,
                    Status = ConsignmentStatus.APPROVED,
                    DateCreated = DateTime.Now,
                    StartDate = DateTime.UtcNow,
                    CreatedBy = customer.UserName,
                    EndDate = DateTime.UtcNow.AddDays(30), // 30 ngày miễn phí
                    Price = 0,
                    UserID = customer.Id,
                };
                await _consignmentRepository.AddAsync(consignment);
                List<ConsignmentItem> consignmentItems = new List<ConsignmentItem>();

                foreach (var koi in koifishes)
                {
                    // Trả tiền cho chủ cá + hoàn tất consignment online của họ
                    if (koi.UserId.HasValue)
                    {
                        var fishOwner = await _userManager.FindByIdAsync(koi.UserId.ToString());
                        fishOwner.Wallet += koi.Price - (koi.Price * CostConstant.COMMISSION_CHARGE);

                        var currentConsignment = await _consignmentRepository.FindAsync(x =>
                            x.UserID == koi.UserId.Value
                            && x.ConsignmentType == ConsignmentType.ONLINE
                            && x.Status == ConsignmentStatus.APPROVED
                            && x.EndDate >= DateTime.UtcNow
                            && x.ConsignmentItems!.Any(item => item.KoiFishId == koi.Id), cancellationToken);
                        currentConsignment.EndDate = DateTime.UtcNow;
                        currentConsignment.Status = ConsignmentStatus.COMPLETED;
                        await _consignmentRepository.UpdateAsync(currentConsignment);
                        await _userManager.UpdateAsync(fishOwner);
                    }
                 
                    koi.Status = KoiFishStatus.RESERVED;
                    koi.DateModified = DateTime.Now;
                    koi.UserId = customer.Id;
                    ConsignmentItem consignmentItem = new ConsignmentItem
                    {
                        ConsignmentId = consignment.Id,
                        DateCreated = DateTime.Now,
                        CreatedBy = customer.UserName,
                        KoiFishId = koi.Id,
                        Price = 0,
                    };  
                    await _consignmentItemRepository.AddAsync(consignmentItem);
                    await _koiFishRepository.UpdateAsync(koi);
                    consignmentItems.Add(consignmentItem);
                }
                consignment.ConsignmentItems = consignmentItems;
                order.Status = OrderStatus.HOLDING;
                await _consignmentRepository.UpdateAsync(consignment);
                break;
        }
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
        var order = await _orderRepository.FindAsync(x => x.Id == orderStatusUpdateDto.Id && x.isDeleted == false, cancellationToken);
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
        foreach (var order in result)
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

    public async Task<Dictionary<int, OrderRevenueDto>> GetRevenueOfYear(int year, CancellationToken cancellationToken)
    {
        Dictionary<int, OrderRevenueDto> result = new Dictionary<int, OrderRevenueDto>();
        decimal revenueFromKoiShop = 0;
        decimal revenueFromCommission = 0;
        int totalCommissionOrders = 0;
        int completedOrders = 0;
        for (int i = 1; i <= 13; i++)
        {
            result.Add(i, new OrderRevenueDto());
        }
        // status khác pending vs cancelled -> Order hoàn thành gòi -> cộng vô revenue dc
        var orderList = await _orderRepository.FindAllAsync(x => x.isDeleted == false 
            && x.DateModified.Year == year 
            && (x.Status != OrderStatus.PENDING && x.Status != OrderStatus.CANCELLED),
            query => query.Include(order => order.OrderItems), cancellationToken); 

        // làm nhưng mà kh chác đúng kh nựa =))))
        foreach (var order in orderList)
        {
            if (order.OrderItems != null)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    if (orderItem.Type == OrderItemType.CONSIGNMENT_ONLINE && orderItem.isDeleted == false)
                    {
                        decimal commission = orderItem.Price * CostConstant.COMMISSION_CHARGE;
                        if (order.OrderItems.Count > 10)
                        {
                            commission *= (1 - CostConstant.WHOLESALE_DISCOUNT);
                        }
                        result[order.DateModified.Month].RevenueFromCommission += commission;
                        result[order.DateModified.Month].TotalCommissionOrders++;

                        revenueFromCommission += commission;
                        totalCommissionOrders++;
                    }
                    else if (orderItem.Type == OrderItemType.SHOP_OWNER && orderItem.isDeleted == false)
                    {
                        decimal revenue = orderItem.Price;
                        if (order.OrderItems.Count > 10)
                        {
                            revenue *= (1 - CostConstant.WHOLESALE_DISCOUNT);
                        }
                        result[order.DateModified.Month].RevenueFromKoiShop += revenue;

                        revenueFromKoiShop += revenue;
                    }
                }
            }
            result[order.DateModified.Month].CompletedOrders++;
            completedOrders++;
        }
        result[13].RevenueFromCommission = revenueFromCommission;
        result[13].TotalCommissionOrders = totalCommissionOrders;
        result[13].CompletedOrders = completedOrders;
        result[13].RevenueFromKoiShop = revenueFromKoiShop;
        return result;

    }

    public async Task<IEnumerable<OrderFavCustomer>> GetTotalOrderByUser()
    {
        var orders = await _orderRepository.GetAllAsync();
        
        var favCustomers = orders
            .Where(o => o.UserId.HasValue) 
            .GroupBy(o => o.UserId.Value)  
            .Select(group => new OrderFavCustomer
            {
                UserId = group.Key,
                TotalOrders = group.Count(),
            })
            .OrderByDescending(o => o.TotalOrders) 
            .ToList();

        return favCustomers;
    }

}
