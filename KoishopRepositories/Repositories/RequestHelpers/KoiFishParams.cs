using System.ComponentModel;

namespace KoishopRepositories.Repositories.RequestHelpers;

public class KoiFishParams : PaginationParams
{
    [DefaultValue("id")]
    public string? OrderBy { get; set; }
    public string? SearchTerm { get; set; } = string.Empty;

    // Filter Params
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public decimal? MinSize { get; set; }
    public decimal? MaxSize { get; set; }
    public string? Origin { get; set; }
    public List<string>? Genders { get; set; }
    public List<string>? Types { get; set; }
    public List<string>? Status { get; set; }
    public List<string>? BreedName { get; set; }
}
