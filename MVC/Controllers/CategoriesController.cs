using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.DtoModel;
using Service.SerachAndPage;

namespace WebMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IMovieService movieService;

        public CategoriesController(ICategoryService categoryService,IMovieService movieService)
        {
            this.categoryService = categoryService;
            this.movieService = movieService;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var categories = await categoryService.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id, string keyWord = "", int page = 1)
        {

            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryService.GetCategoryAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            const int pageSize = 2;
            if (page < 1) page = 1;
            
            int i = 0;
            if (keyWord != null) {
                keyWord = keyWord.ToLower();
                i = category.Movies.Where(x => x.Name.ToLower().Contains(keyWord)).Count();
            }
            else
            {
                i = category.Movies.Count;
            }

            var pager = new Pager(i, page, pageSize) { KeyWord = keyWord };

            var skip = (page - 1) * pageSize;

            List<MovieOutDto> data;

            if (keyWord != "" && keyWord != null)
            {
                keyWord = keyWord.ToLower();
                var movies = category.Movies.Where(x => x.Name.ToLower().Contains(keyWord));
                data = movies.Skip(skip).Take(pager.PageSize).ToList();
            }
            else
            {
                data = category.Movies.Skip(skip).Take(pager.PageSize).ToList();
            }

            category.Movies = data;

            this.ViewBag.Pager = pager;

            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryInDto category)
        {
            if (category != null)
            {
                await categoryService.AddNewCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryService.GetCategoryAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,CategoryInDto category)
        {
            if (category != null)
            {
                try
                {
                    bool info = await categoryService.UpdateCategoryAsync(id,category);
                    if (!info)
                        return BadRequest();
                }
                catch (Exception)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryService.GetCategoryAsync(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            if (category != null)
            {
                await categoryService.DeleteCategoryAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
