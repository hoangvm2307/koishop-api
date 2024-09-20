﻿using KoishopBusinessObjects.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects;

public class Consignment : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ConsignmentType { get; set; } 
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public virtual ICollection<ConsignmentItem>? ConsignmentItems { get; set; }

}
