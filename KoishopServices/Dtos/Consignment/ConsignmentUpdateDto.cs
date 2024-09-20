using DTOs.ConsignmentItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Consignment;

public class ConsignmentUpdateDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? ConsignmentType { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
}
