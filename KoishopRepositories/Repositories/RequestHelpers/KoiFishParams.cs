using System.ComponentModel;

namespace KoishopRepositories.Repositories.RequestHelpers;

public class KoiFishParams : PaginationParams
{
    [DefaultValue("id")]
    public string? OrderBy { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;
}
