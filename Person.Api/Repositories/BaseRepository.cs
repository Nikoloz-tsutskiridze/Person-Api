using Person.Api.Data;

namespace BasePerson.Api.Repositories
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
