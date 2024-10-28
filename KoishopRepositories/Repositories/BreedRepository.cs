using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;

namespace KoishopRepositories.Repositories;

public class BreedRepository : GenericRepository<Breed>, IBreedRepository
{
    public BreedRepository(KoishopContext context) : base(context)
    {
    }
}
