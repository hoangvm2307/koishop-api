﻿using System.ComponentModel.DataAnnotations.Schema;

namespace KoishopBusinessObjects;

public class FishCare : BaseEntity
{
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; } 
    public string? CareInstructions { get; set; }
    public string? Status { get; set; }
    public int CareId { get; set; }

    [ForeignKey(nameof(KoiFish))]
    public int? KoiFishId { get; set; }
    public virtual KoiFish? KoiFish { get; set; }

    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }
    public virtual User? User { get; set; }
}
