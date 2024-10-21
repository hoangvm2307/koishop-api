using KoishopBusinessObjects;
using KoishopRepositories.Migrations;
using KoishopRepositories.Repositories.RequestHelpers;

namespace KoishopRepositories.Repositories.Extensions
{
    public static class KoiFishExtensions
    {
        public static IQueryable<KoiFish> Sort(this IQueryable<KoiFish> query, string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy)) return query;

            query = orderBy.ToLower() switch
            {
                "id" => query.OrderBy(x => x.Id),
                "name_asc" => query.OrderBy(p => p.Name),
                "name_desc" => query.OrderByDescending(p => p.Name),
                "age_asc" => query.OrderBy(p => p.Age),
                "age_desc" => query.OrderByDescending(p => p.Age),
                "price_asc" => query.OrderBy(p => p.Price),
                "price_desc" => query.OrderByDescending(p => p.Price),
                _ => query,
            };

            return query;
        }

        public static IQueryable<KoiFish> Search(this IQueryable<KoiFish> query, string searchTerm)
        {
            if(string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return query.Where(c => c.Name.Trim().ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<KoiFish> Filter(this IQueryable<KoiFish> query, KoiFishParams koiFishParams)
        {
            if (koiFishParams.MinPrice.HasValue)
                query = query.Where(k => k.Price >= koiFishParams.MinPrice.Value);

            if (koiFishParams.MaxPrice.HasValue)
                query = query.Where(k => k.Price <= koiFishParams.MaxPrice.Value);

            if (koiFishParams.MinAge.HasValue)
                query = query.Where(k => k.Age >= koiFishParams.MinAge.Value);

            if (koiFishParams.MaxAge.HasValue)
                query = query.Where(k => k.Age <= koiFishParams.MaxAge.Value);

            // if (koiFishParams.MinSize.HasValue)
            //     query = query.Where(k => k.Size >= koiFishParams.MinSize.Value);

            // if (koiFishParams.MaxSize.HasValue)
            //     query = query.Where(k => k.Size <= koiFishParams.MaxSize.Value);

            if (!string.IsNullOrEmpty(koiFishParams.Origin))
                query = query.Where(k => k.Origin.ToLower() == koiFishParams.Origin.ToLower());


            if (koiFishParams.Sizes != null && koiFishParams.Sizes.Any())
            {
                var lowerCaseSizes = koiFishParams.Sizes.Select(s => s.ToLower()).ToList();
                var resultList = new List<IQueryable<KoiFish>>();

                foreach (var size in lowerCaseSizes)
                {
                    switch (size)
                    {
                        case "over_10":
                            resultList.Add(query.Where(k => k.Size > 10));
                            break;
                        case "6_10":
                            resultList.Add(query.Where(k => k.Size >= 6 && k.Size <= 10));
                            break;
                        case "8_12":
                            resultList.Add(query.Where(k => k.Size >= 6 && k.Size <= 10));
                            break;
                        case "under_8":
                            resultList.Add(query.Where(k => k.Size >= 6 && k.Size <= 10));
                            break;
                        case "under_6":
                            resultList.Add(query.Where(k => k.Size < 6));
                            break;
                        default:
                            break;
                    }
                }

                if (resultList.Any())
                    query = resultList.Aggregate((current, next) => current.Concat(next)).Distinct();
            }

            if (koiFishParams.Genders != null && koiFishParams.Genders.Any())
            {
                var lowerCaseGenders = koiFishParams.Genders.Select(g => g.ToLower()).ToList();
                query = query.Where(k => lowerCaseGenders.Contains(k.Gender.ToLower()));
            }

            if (koiFishParams.Types != null && koiFishParams.Types.Any())
            {
                var lowerCaseTypes = koiFishParams.Types.Select(g => g.ToLower()).ToList();
                query = query.Where(k => lowerCaseTypes.Contains(k.Type.ToLower()));
            }

            if (koiFishParams.Status != null && koiFishParams.Status.Any())
            {
                var lowerCaseStatus = koiFishParams.Status.Select(g => g.ToLower()).ToList();
                query = query.Where(k => lowerCaseStatus.Contains(k.Status.ToLower()));
            }

            if (koiFishParams.BreedId.HasValue)
                query = query.Where(k => k.BreedId == koiFishParams.BreedId.Value);

            return query;
        }
    }
}
