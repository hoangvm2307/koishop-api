using System.ComponentModel;

namespace KoishopRepositories.Repositories.RequestHelpers;

public class PaginationParams
{
    private const int maxPageSize = 50;
    private const int defaultPageNumber = 1;
    private const int defaultPageSize = 6;
    private int _pageSize = defaultPageSize;

    [DefaultValue(defaultPageNumber)]
    public int PageNumber { get; set; } = defaultPageNumber;
    [DefaultValue(defaultPageSize)]
    public int PageSize
    { 
        get => _pageSize; 
        set => _pageSize = value > maxPageSize ? maxPageSize : value; 
    }
}
