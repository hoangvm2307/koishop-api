﻿using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;

namespace KoishopRepositories.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    private readonly KoishopContext _context;

    public OrderRepository(KoishopContext context) : base(context)
    {
        _context = context;
    }

    //GET ORDER DETAILS EXAMPLE
    //public async Task<Order> GetOrderDetails(int Id)
    //{
    //    return await _context.Orders
    //        .Include(c => c.OrderItems)
    //        .FirstOrDefaultAsync()
    //}
}
