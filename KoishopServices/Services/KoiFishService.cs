using AutoMapper;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class KoiFishService : IKoiFishService
{
    private readonly IMapper _mapper;
    private readonly IKoiFishRepository _koifishRepository;

    public KoiFishService(IMapper mapper, IKoiFishRepository koifishRepository)
    {
        this._mapper = mapper;
        this._koifishRepository = koifishRepository;
    }
    public async Task AddKoiFish(KoiFishCreationDto koifishCreationDto)
    {
        //TODO: Add validation before create and mapping
        var koifish = _mapper.Map<KoiFish>(koifishCreationDto);
        await _koifishRepository.AddAsync(koifish);
    }

    public async Task<KoiFishDto> GetKoiFishById(int id)
    {
        var koifish = await _koifishRepository.GetByIdAsync(id);
        if (koifish == null)
            return null;
        return _mapper.Map<KoiFishDto>(koifish);
    }

    public async Task<IEnumerable<KoiFishDto>> GetListKoiFish()
    {
        var koifishs = await _koifishRepository.GetAllAsync();
        var result = _mapper.Map<List<KoiFishDto>>(koifishs);
        return result;
    }

    public async Task<bool> RemoveKoiFish(int id)
    {
        var exist = await _koifishRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _koifishRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateKoiFish(int id, KoiFishUpdateDto koifishUpdateDto)
    {
        var existingKoiFish = await _koifishRepository.GetByIdAsync(id);
        if (existingKoiFish == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(koifishUpdateDto, existingKoiFish);
        await _koifishRepository.UpdateAsync(existingKoiFish);
        return true;
    }
}
