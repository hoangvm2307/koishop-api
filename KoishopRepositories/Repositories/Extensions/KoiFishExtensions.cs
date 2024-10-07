using KoishopBusinessObjects;

namespace KoishopRepositories.Repositories.Extensions
{
    public static class KoiFishExtensions
    {
        public static IQueryable<KoiFish> Sort(this IQueryable<KoiFish> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return query;

            query = orderBy switch
            {
                "id" => query.OrderBy(x => x.Id),
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "age_asc" => query.OrderBy(p => p.Age),
                "age_desc" => query.OrderByDescending(p => p.Age),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
            };

            return query;
        }

        public static IQueryable<KoiFish> Search(this IQueryable<KoiFish> query, string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(c => c.Name.Trim().ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<KoiFish> Filter(this IQueryable<KoiFish> query, string searchTerm)
        {
            return query;
        }
    }
}
