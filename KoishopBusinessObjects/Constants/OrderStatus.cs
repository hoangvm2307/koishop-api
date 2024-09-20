using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects.Constants;

public static class OrderStatus
{
    public const string PENDING = "Pending";
    public const string PROCESSING = "Processing";
    public const string SHIPPED = "Shipped";
    public const string DELIVERED = "Delivered";
    public const string CANCELLED = "Cancelled";
}
