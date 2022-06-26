using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Concrete
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Movie> GetMovieAsync(int id) => await Context.Movies.Include(x => x.Category).Include(x => x.Comments).ThenInclude(x => x.User).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<Movie>> GetAllMoviesAsync() => await Context.Movies.Include(x => x.Category).Include(x => x.Comments).ThenInclude(x => x.User).ToListAsync();

        public async Task<bool> SaveMovieAsync(Movie movie)
        {
            if (movie == null)
                return false;

            try
            {
                Context.Entry(movie).State = movie.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var movie = await GetMovieAsync(id);
            if (movie == null)
                return true;

            Context.Movies.Remove(movie);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
