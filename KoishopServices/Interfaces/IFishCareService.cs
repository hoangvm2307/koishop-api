using DTOs.Consignment;
using DTOs.FishCare;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces;

public interface IFishCareService
{
    Task<IEnumerable<FishCareDto>> GetListFishCare();
    Task<FishCareDto> GetFishCareById(int id);
    Task AddFishCare(FishCareCreationDto fishCareCreationDto);
    Task<bool> UpdateFishCare(int id, FishCareUpdateDto fishCareUpdateDto);
    Task<bool> RemoveFishCare(int id);
}
