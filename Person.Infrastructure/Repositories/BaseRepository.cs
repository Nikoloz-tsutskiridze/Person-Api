using Person.Infrastructure.Data;

namespace Person.Infrastructure.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _appDbContext;
        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
