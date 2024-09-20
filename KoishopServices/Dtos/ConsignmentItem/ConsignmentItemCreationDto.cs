using DTOs.KoiFish;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ConsignmentItem;

public class ConsignmentItemCreationDto
{
    public int? ConsignmentId { get; set; }
    public int? KoiFishId { get; set; }
    public decimal Price { get; set; }
}
