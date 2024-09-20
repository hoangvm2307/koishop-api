﻿using DTOs.KoiFish;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Breed;

public class BreedUpdateDto
{
    public int Id { get; set; }
    public string? BreedName { get; set; }
    public string? ScreeningRatio { get; set; }
    public string? Personality { get; set; }
}