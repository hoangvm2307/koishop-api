using KoishopBusinessObjects;
using KoishopRepositories.DatabaseContext;
using KoishopRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoishopRepositories.Repositories;

public class ConsignmentItemRepository : GenericRepository<ConsignmentItem>, IConsignmentItemRepository
{
    private readonly KoishopContext _context;

    public ConsignmentItemRepository(KoishopContext context) : base(context)
    {
        _context = context;
    }
}
