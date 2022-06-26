using Model.Entities;

namespace Repository.Abstract
{
    public interface IMovieRepository
    {
        Task<Movie> GetMovieAsync(int id);
        Task<List<Movie>> GetAllMoviesAsync();
        Task<bool> SaveMovieAsync(Movie product);
        Task<bool> DeleteMovieAsync(int id);
    }
}
