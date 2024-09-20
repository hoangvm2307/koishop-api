using DTOs.KoiFish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.FishCare;

public class FishCareUpdateDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? CareInstructions { get; set; }
    public string? Status { get; set; }
    public int CareId { get; set; }
    public int? KoiFishId { get; set; }
    public int? UserId { get; set; }
}
