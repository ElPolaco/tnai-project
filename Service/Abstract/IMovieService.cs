using Service.DtoModel;

namespace Service.Abstract
{
    public interface IMovieService
    {
        Task<bool> AddNewMovieAsync(MovieInDto movieInDto);
        Task<MovieOutDto> GetMovieAsync(int id);
        Task<List<MovieOutDto>> GetAllMoviesAsync();
        Task<bool> UpdateMovieAsync(int id, MovieInDto movieInDto);
        Task<bool> DeleteMovieAsync(int id);
        Task<List<MovieOutDto>> SearchByKeyword(string keyword);
    }
}
