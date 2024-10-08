using AutoMapper;

namespace KoishopRepositories.Repositories.RequestHelpers;

public class PagedListConverter<TSource, TDestination> : ITypeConverter<IPagedList<TSource>, IPagedList<TDestination>>
{
    public IPagedList<TDestination> Convert(IPagedList<TSource> source, IPagedList<TDestination> destination, ResolutionContext context)
    {
        var dtos = context.Mapper.Map<List<TDestination >>(source.ToList());
        return new PagedList<TDestination >(dtos, source.MetaData.TotalCount, source.MetaData.CurrentPage, source.MetaData.PageSize);
    }
}