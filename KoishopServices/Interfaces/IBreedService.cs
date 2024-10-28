using DTOs.Breed;

namespace KoishopServices.Interfaces
{
    public interface IBreedService
    {
        Task<IEnumerable<BreedDto>> GetListBreed();
        Task<BreedDto> GetBreedById(int id);
        Task AddBreed(BreedCreationDto breedCreationDto);
        Task<bool> UpdateBreed(int id, BreedUpdateDto breedUpdateDto);
        Task<bool> RemoveBreed(int id);
    }
}
