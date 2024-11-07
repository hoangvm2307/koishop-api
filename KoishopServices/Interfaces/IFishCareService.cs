using DTOs.FishCare;

namespace KoishopServices.Interfaces;

public interface IFishCareService
{
    Task<IEnumerable<FishCareDto>> GetListFishCare();
    Task<FishCareDto> GetFishCareById(int id);
    Task AddFishCare(FishCareCreationDto fishCareCreationDto);
    Task<bool> UpdateFishCare(int id, FishCareUpdateDto fishCareUpdateDto);
    Task<bool> RemoveFishCare(int id);
    Task<List<FishCareDto>> GetFishCareByFishId(int fishId);
}
