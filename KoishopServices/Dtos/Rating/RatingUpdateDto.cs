using DTOs.KoiFish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Rating;

public class RatingUpdateDto
{
    public int Id { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }
    public int UserId { get; set; }
    public int KoiFishId { get; set; }
}
