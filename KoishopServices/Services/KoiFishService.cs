using AutoMapper;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopRepositories.Interfaces;
using KoishopServices.Common.Exceptions;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace KoishopServices.Services;

public class KoiFishService : IKoiFishService
{
    private readonly IMapper _mapper;
    private readonly IKoiFishRepository _koifishRepository;
    private readonly UserManager<User> _userManager;

    public KoiFishService(IMapper mapper
        , IKoiFishRepository koifishRepository
        , UserManager<User> userManager)
    {
        this._mapper = mapper;
        _userManager = userManager;
        this._koifishRepository = koifishRepository;
    }
    public async Task AddKoiFish(KoiFishCreationDto koifishCreationDto)
    {
        var user = await _userManager.FindByIdAsync(koifishCreationDto.UserId.Value.ToString());
        if (user == null)
        {
            throw new NotFoundException(ExceptionConstants.USER_NOT_EXIST);
        }   

        // VALIDATE INPUT CONST
        if (!new[] { KoiFishGender.MALE, KoiFishGender.FEMALE, KoiFishGender.UNKNOWN }.Contains(koifishCreationDto.Gender))
        {
            throw new ValidationException(ExceptionConstants.INVALID_KOIFISH_GENDER);
        }
        if (!new[] { KoiFishStatus.AVAILABLE, KoiFishStatus.SOLD, KoiFishStatus.RESERVED }.Contains(koifishCreationDto.Status))
        {
            throw new ValidationException(ExceptionConstants.INVALID_KOIFISH_TYPE);
        }
        if (!new[] { KoiFishType.PUREIMPORTED, KoiFishType.HYBRIDF1, KoiFishType.PUREVIETNAMESE }.Contains(koifishCreationDto.Type))
        {
            throw new ValidationException(ExceptionConstants.INVALID_KOIFISH_TYPE);
        }

        // VALIDATE PRICE
        if (koifishCreationDto.Price < 0 || koifishCreationDto.ListPrice < 0)
        {
            throw new ValidationException(ExceptionConstants.INVALID_PRICE);
        }
        var koifish = _mapper.Map<KoiFish>(koifishCreationDto);
        koifish.UserId = user.Id;
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
