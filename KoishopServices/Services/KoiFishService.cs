using AutoMapper;
using DTOs.KoiFish;
using KoishopBusinessObjects;
using KoishopBusinessObjects.Constants;
using KoishopRepositories.Interfaces;
using KoishopRepositories.Repositories.RequestHelpers;
using KoishopServices.Common.Exceptions;
using KoishopServices.Common.Interface;
using KoishopServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace KoishopServices.Services;

public class KoiFishService : IKoiFishService
{
    private readonly IMapper _mapper;
    private readonly IKoiFishRepository _koifishRepository;
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUserService _currentUserService;

    public KoiFishService(IMapper mapper
        , IKoiFishRepository koifishRepository
        , UserManager<User> userManager
        , ICurrentUserService currentUserService)
    {
        this._mapper = mapper;
        _userManager = userManager;
        this._koifishRepository = koifishRepository;
        _currentUserService = currentUserService;
    }
    public async Task AddKoiFish(KoiFishCreationDto koifishCreationDto)
    {
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
        koifish.CreatedBy = _currentUserService.UserId;
        await _koifishRepository.AddAsync(koifish);
    }

    public async Task AddKoiFishWithUser(KoiFishCreationDto koifishCreationDto, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
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
        koifish.CreatedBy = userId;

        await _koifishRepository.AddAsync(koifish);
    }

    public async Task<KoiFishDto> GetKoiFishById(int id)
    {
        var koifish = await _koifishRepository.GetKoiFishDetail(id);
        if (koifish == null)
            return null;
        return _mapper.Map<KoiFishDto>(koifish);
    }

    public async Task<IPagedList<KoiFishDto>> GetListKoiFish(KoiFishParams koiFishParams)
    {
        var query = await _koifishRepository.GetKoiFishs(koiFishParams);
        var koiFishEnity = await PagedList<KoiFish>.ToPagedList(query, koiFishParams.PageNumber, koiFishParams.PageSize);
        return _mapper.Map<IPagedList<KoiFishDto>>(koiFishEnity);
    }

    public async Task<List<KoiFishDto>> GetKoiFishByIds(List<int> ids)
    {
        var listFish = await _koifishRepository.GetKoiFishByIds(ids);
        return _mapper.Map<List<KoiFishDto>>(listFish);
    }

    public async Task<List<KoiFishDto>> GetRelatedKoiFishBy(int id)
    {
        var koiFish = await _koifishRepository.GetKoiFishDetail(id);
        if (koiFish == null)
            return null;

        var listFish = await _koifishRepository.GetRelatedKoiFishBy(koiFish);
        if (listFish.IsNullOrEmpty()) listFish = await _koifishRepository.GetRandomKoiFishExcludingCurrent(id);
        return _mapper.Map<List<KoiFishDto>>(listFish);
    }

    public async Task<FilterKoiFishParamDto> GetFilterParam()
    {
        return new FilterKoiFishParamDto
        {
            MaxPrice = _koifishRepository.GetMaxPrices(),
            MinPrice = _koifishRepository.GetMinPrices(),
            MaxAge = _koifishRepository.GetMaxAge(),
            MinAge = _koifishRepository.GetMinAges(),
            Origin = await _koifishRepository.GetDistinctOriginsAsync(),
            Sizes = _koifishRepository.GetDistinctSizes(),
            Genders = await _koifishRepository.GetDistinctGendersAsync(),
            Types = await _koifishRepository.GetDistinctTypesAsync(),
            Status = await _koifishRepository.GetDistinctStatusAsync(),
            BreedName = await _koifishRepository.GetDistinctBreedAsync()
        };
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
