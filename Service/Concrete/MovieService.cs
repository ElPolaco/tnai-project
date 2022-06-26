using AutoMapper;
using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using Service.DtoModel;

namespace Service.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository movieRepository;
        private readonly IMapper mapper;

        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            this.movieRepository = movieRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddNewMovieAsync(MovieInDto movieInDto)
        {
            var newMovie = mapper.Map<Movie>(movieInDto);
            bool info = await movieRepository.SaveMovieAsync(newMovie);
            return info;
        }

        public async Task<MovieOutDto> GetMovieAsync(int id)
        {
            var movie = await movieRepository.GetMovieAsync(id);
            var getMovie = mapper.Map<MovieOutDto>(movie);
            return getMovie;
        }

        public async Task<List<MovieOutDto>> SearchByKeyword(string keyword)
        {
            keyword = keyword.ToLowerInvariant();

            var movie = movieRepository.GetAllMoviesAsync().Result
                .Where(o => o.Name.ToLower().Contains(keyword) || o.Category.Name.ToLower().Equals(keyword)).ToList();
            return mapper.Map<List<MovieOutDto>>(movie);
        }

        public async Task<List<MovieOutDto>> GetAllMoviesAsync()
        {
            var movies = await movieRepository.GetAllMoviesAsync();
            return mapper.Map<List<MovieOutDto>>(movies);
        }

        public async Task<bool> UpdateMovieAsync(int id, MovieInDto movieInDto)
        {
            var existingMovie = await movieRepository.GetMovieAsync(id);
            if (existingMovie == null)
                return false;
            var updatedMovie = mapper.Map(movieInDto, existingMovie);

            bool info = await movieRepository.SaveMovieAsync(updatedMovie);
            return info;
        }

        public async Task<bool> DeleteMovieAsync(int id)
        {
            var category = await movieRepository.GetMovieAsync(id);
            bool info = await movieRepository.DeleteMovieAsync(id);
            return info;
        }
    }
}
