using DTOs.AccountDtos;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using KoishopServices.Common.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Rating;

public class RatingDto
{
    public int Id { get; set; }
    public int RatingValue { get; set; }
    public string? Comment { get; set; }

    public int UserId { get; set; }
    public virtual UserDto? UserDto { get; set; }
    public int KoiFishId { get; set; }
    public virtual KoiFishDto? KoiFishDto { get; set;}
}
