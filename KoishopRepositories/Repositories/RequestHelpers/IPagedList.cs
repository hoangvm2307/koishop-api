namespace KoishopRepositories.Repositories.RequestHelpers
{
    public interface IPagedList<T> : IList<T>
    {
        MetaData MetaData { get; set; }
    }
}
