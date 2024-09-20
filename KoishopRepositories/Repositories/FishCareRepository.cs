using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopRepositories.Repositories;

public class FishCareRepository : GenericRepository<FishCare>, IFishCareRepository
{
    public FishCareRepository(KoishopContext context) : base(context)
    {
    }
}
