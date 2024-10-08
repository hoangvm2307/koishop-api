﻿using KoishopBusinessObjects;
using KoishopRepositories.Repositories.RequestHelpers;

namespace KoishopRepositories.Interfaces;

public interface IKoiFishRepository : IGenericRepository<KoiFish>
{
    Task<IQueryable<KoiFish>> GetKoiFishs(KoiFishParams koiFishParams);
    Task<KoiFish> GetKoiFishDetail(int id);
    decimal GetMaxPrices();
    decimal GetMinPrices();
    int GetMaxAge();
    int GetMinAges();
    Task<List<string>> GetDistinctOriginsAsync();
    List<string> GetDistinctSizes();
    Task<List<string>> GetDistinctGendersAsync();
    Task<List<string>> GetDistinctTypesAsync();
    Task<List<string>> GetDistinctStatusAsync();
    Task<List<string>> GetDistinctBreedAsync();
}
