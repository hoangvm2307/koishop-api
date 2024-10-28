using AutoMapper;
using DTOs.Consignment;
using KoishopBusinessObjects;

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
