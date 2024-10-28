using AutoMapper;

namespace KoishopServices.Common.Mapping
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
