﻿using DTOs.KoiFish;
using DTOs.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.OrderItem;

public class OrderItemDto
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int? OrderId { get; set; }
    public int? KoiFishId { get; set; }
    public string KoiFishName { get; set; }
}
