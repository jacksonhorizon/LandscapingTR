using LandscapingTR.Core.Entities.Lookups;
using LandscapingTR.Core.Interfaces;
namespace LandscapingTR.Infrastructure.Data.Repositories
{
    public class LookupRepository : BaseRepository<BaseLookupEntity, int?>, ILookupRepository
    {
        public LookupRepository(LandscapingTRDbContext dbContext) : base(dbContext)
        {
        }
    }
}
