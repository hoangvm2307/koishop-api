using AutoMapper;
using DTOs.Consignment;
using KoishopBusinessObjects;
using KoishopServices.Dtos.Consignment;

namespace KoishopServices.Profiles;

public class ConsignmentProfile : Profile
{
    public ConsignmentProfile()
    {
        CreateMap<ConsignmentDto, Consignment>().ReverseMap();
        CreateMap<ConsignmentCreationDto, Consignment>();
        CreateMap<ConsignmentUpdateDto, Consignment>();
        CreateMap<ConsignmentStatusUpdateDto, Consignment>();
    }
}
