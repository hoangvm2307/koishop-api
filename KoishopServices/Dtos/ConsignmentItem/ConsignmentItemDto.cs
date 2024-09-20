using DTOs.Consignment;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ConsignmentItem;

public class ConsignmentItemDto
{
    public int Id { get; set; }
    public int? ConsignmentId { get; set; }
    public virtual ConsignmentDto? Consignment { get; set; }
    public int? KoiFishId { get; set; }
    public virtual KoiFishDto? KoiFish { get; set; }
    public decimal Price { get; set; }
}
