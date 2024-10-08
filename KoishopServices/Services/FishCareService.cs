﻿using AutoMapper;
using DTOs.FishCare;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class FishCareService : IFishCareService
{
    private readonly IMapper _mapper;
    private readonly IFishCareRepository _fishCareRepository;

    public FishCareService(IMapper mapper, IFishCareRepository fishCareRepository)
    {
        this._mapper = mapper;
        this._fishCareRepository = fishCareRepository;
    }
    public async Task AddFishCare(FishCareCreationDto fishCareCreationDto)
    {
        //TODO: Add validation before create and mapping
        var fishCare = _mapper.Map<FishCare>(fishCareCreationDto);
        await _fishCareRepository.AddAsync(fishCare);
    }

    public async Task<FishCareDto> GetFishCareById(int id)
    {
        var fishCare = await _fishCareRepository.GetByIdAsync(id);
        if (fishCare == null)
            return null;
        return _mapper.Map<FishCareDto>(fishCare);
    }

    public async Task<IEnumerable<FishCareDto>> GetListFishCare()
    {
        var fishCares = await _fishCareRepository.GetAllAsync();
        var result = _mapper.Map<List<FishCareDto>>(fishCares);
        return result;
    }

    public async Task<bool> RemoveFishCare(int id)
    {
        var exist = await _fishCareRepository.GetByIdAsync(id);
        if (exist == null)
            return false;
        await _fishCareRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateFishCare(int id, FishCareUpdateDto fishCareUpdateDto)
    {
        var existingFishCare = await _fishCareRepository.GetByIdAsync(id);
        if (existingFishCare == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(fishCareUpdateDto, existingFishCare);
        await _fishCareRepository.UpdateAsync(existingFishCare);
        return true;
    }
}
