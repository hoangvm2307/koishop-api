using AutoMapper;
using DTOs.Consignment;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Profiles;

public class ConsignmentProfile : Profile
{
    public ConsignmentProfile()
    {
        CreateMap<ConsignmentDto, Consignment>().ReverseMap();
        CreateMap<ConsignmentCreationDto, Consignment>();
        CreateMap<ConsignmentUpdateDto, Consignment>();
    }
}
