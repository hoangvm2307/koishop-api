using AutoMapper;
using DTOs.Breed;
using KoishopBusinessObjects;
using KoishopRepositories.Interfaces;
using KoishopServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopServices.Services;

public class BreedService : IBreedService
{
    private readonly IMapper _mapper;
    private readonly IBreedRepository _breedRepository;

    public BreedService(IMapper mapper, IBreedRepository breedRepository)
    {
        this._mapper = mapper;
        this._breedRepository = breedRepository;
    }
    public async Task AddBreed(BreedCreationDto breedCreationDto)
    {
        //TODO: Add validation before create and mapping
        var breed = _mapper.Map<Breed>(breedCreationDto);
        await _breedRepository.AddAsync(breed);
    }

    public async Task<BreedDto> GetBreedById(int id)
    {
        var breed = await _breedRepository.GetByIdAsync(id);
        if (breed == null)
            return null;
        return _mapper.Map<BreedDto>(breed);
    }

    public async Task<IEnumerable<BreedDto>> GetListBreed()
    {
        var breeds = await _breedRepository.GetAllAsync();
        var result = _mapper.Map<List<BreedDto>>(breeds);
        return result;
    }

    public async Task<bool> RemoveBreed(int id)
    {
        var exist = await _breedRepository.GetByIdAsync(id);
        if(exist == null)
            return false;
        await _breedRepository.DeleteAsync(exist);
        return true;
    }

    public async Task<bool> UpdateBreed(int id, BreedUpdateDto breedUpdateDto)
    {
        var existingBreed = await _breedRepository.GetByIdAsync(id);
        if (existingBreed == null)
            return false;

        //TODO: Add validation before Update and mapping
        _mapper.Map(breedUpdateDto, existingBreed);
        await _breedRepository.UpdateAsync(existingBreed);
        return true;
    }
}
