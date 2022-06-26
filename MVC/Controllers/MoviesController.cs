using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Service.Abstract;
using Service.DtoModel;
using Service.SerachAndPage;

namespace WebMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService movieService;
        private readonly ICategoryService categoryService;

        public MoviesController(IMovieService movieService, ICategoryService categoryService)
        {
            this.movieService = movieService;
            this.categoryService = categoryService;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string keyWord = "", int page=1)
        {
            List<MovieOutDto> movies;

            if(keyWord != "" && keyWord != null)
            {
                movies = await movieService.SearchByKeyword(keyWord);
            }
            else
            {
                movies = await movieService.GetAllMoviesAsync();
            }
                
            //const int pageSize = 20;
            const int pageSize = 2;
            if (page < 1) page = 1;

            var pager = new Pager(movies.Count(), page, pageSize) { KeyWord = keyWord};

            var skip = (page - 1) * pageSize;

            var data = movies.Skip(skip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;

            return View(data);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id, int page = 1)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await movieService.GetMovieAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            const int pageSize = 4;
            if (page < 1) page = 1;

            int i = movie.Comments.Count;

            var pager = new Pager(i, page, pageSize);

            var skip = (page - 1) * pageSize;

            ViewBag.AllComments = movie.Comments;

            var commentsSort = movie.Comments.OrderByDescending(x => x.LastModified);
            var comments = commentsSort.Skip(skip).Take(pager.PageSize).ToList();

            movie.Comments = comments;

            this.ViewBag.Pager = pager;

            return View(movie);
        }

        // GET: Categories/Create
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieInDto movie)
        {
            if (movie != null)
            {
                await movieService.AddNewMovieAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Categories/Edit/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await movieService.GetMovieAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            var categories = await categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieInDto movie)
        {
            if (movie != null)
            {
                try
                {
                    bool info = await movieService.UpdateMovieAsync(id, movie);
                    if (!info)
                        return BadRequest();
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Categories/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await movieService.GetMovieAsync(id.Value);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await movieService.GetMovieAsync(id);
            if (movie != null)
            {
                await movieService.DeleteMovieAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
