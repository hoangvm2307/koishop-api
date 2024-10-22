using DTOs.KoiFish;
using KoishopRepositories.Repositories.RequestHelpers;

namespace KoishopServices.Interfaces;

public interface IKoiFishService
{
    Task<IPagedList<KoiFishDto>> GetListKoiFish(KoiFishParams koiFishParams);
    Task<KoiFishDto> GetKoiFishById(int id);
    Task<List<KoiFishDto>> GetRelatedKoiFishBy(int id);
    Task<FilterKoiFishParamDto> GetFilterParam();
    Task<List<KoiFishDto>> GetKoiFishByIds(List<int> ids);
    Task AddKoiFish(KoiFishCreationDto koiFishCreationDto);
    Task<bool> UpdateKoiFish(int id, KoiFishUpdateDto koiFishUpdateDto);
    Task<bool> RemoveKoiFish(int id);
}
