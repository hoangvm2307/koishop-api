using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopBusinessObjects;

public class Breed : BaseEntity
{
    public string? BreedName { get; set; }
    public string? ScreeningRatio { get; set; }
    public string? Personality { get; set; }
    public virtual ICollection<KoiFish>? KoiFish { get; set; }
}
