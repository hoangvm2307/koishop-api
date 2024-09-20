using DTOs.FishCare;
using DTOs.KoiFish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Interfaces;

public interface IKoiFishService
{
    Task<IEnumerable<KoiFishDto>> GetListKoiFish();
    Task<KoiFishDto> GetKoiFishById(int id);
    Task AddKoiFish(KoiFishCreationDto koiFishCreationDto);
    Task<bool> UpdateKoiFish(int id, KoiFishUpdateDto koiFishUpdateDto);
    Task<bool> RemoveKoiFish(int id);
}
