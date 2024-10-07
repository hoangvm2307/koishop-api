using AutoMapper;
using DTOs.KoiFishDtos;
using KoishopBusinessObjects;

namespace KoishopRepositories.Repositories.RequestHelpers;
public class PagedListConverter : ITypeConverter<IPagedList<KoiFish>, IPagedList<KoiFishDto>>
{
    public IPagedList<KoiFishDto> Convert(IPagedList<KoiFish> source, IPagedList<KoiFishDto> destination, ResolutionContext context)
    {
        var koiFishDtos = context.Mapper.Map<List<KoiFishDto>>(source.ToList());
        return new PagedList<KoiFishDto>(koiFishDtos, source.MetaData.TotalCount, source.MetaData.CurrentPage, source.MetaData.PageSize);
    }
}
