using Microsoft.Extensions.Configuration;
using Model;

namespace Repository.Concrete
{
    public class BaseRepository
    {
        protected AppDbContext Context;

        public BaseRepository(IConfiguration configuration)
        {
            Context = new AppDbContext(configuration);
        }
    }
}
