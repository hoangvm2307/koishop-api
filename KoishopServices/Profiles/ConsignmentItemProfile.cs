using AutoMapper;
using DTOs.ConsignmentItem;
using KoishopBusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Profiles;

public class ConsignmentItemProfile : Profile
{
    public ConsignmentItemProfile()
    {
        CreateMap<ConsignmentItemDto, ConsignmentItem>().ReverseMap();
        CreateMap<ConsignmentItemCreationDto, ConsignmentItem>();
        CreateMap<ConsignmentItemUpdateDto, ConsignmentItem>();
    }
}
