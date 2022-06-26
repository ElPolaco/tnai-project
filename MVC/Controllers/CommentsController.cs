using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.DtoModel;
using System.Globalization;
using System.Security.Claims;

namespace WebMVC.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IMovieService movieService;

        public CommentsController(ICommentService commentService, IMovieService movieService)
        {
            this.commentService = commentService;
            this.movieService = movieService;
        }


        // GET: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? movieId, string? content, string? rating)
        {
            if (movieId == null || rating == null)
            {
                return NotFound();
            }
            if (content == null) content = "";
            CommentInDto dto = new CommentInDto();

            dto.UserId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            dto.MovieId = (int)movieId;
            dto.Content = content;
            dto.Rating = Decimal.Parse(rating, CultureInfo.InvariantCulture);
            await commentService.AddNewCommentAsync(dto);
            return Redirect("/Movies/Details/" + dto.MovieId);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await commentService.GetCommentAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CommentInDto comment)
        {
            if (comment != null)
            {
                try
                {
                    bool info = await commentService.UpdateCommentAsync(id, comment);
                    if (!info)
                        return BadRequest();
                }
                catch (Exception)
                {
                    throw;
                }
                return Redirect("/Movies/Details/" + comment.MovieId);
            }
            return View(comment);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await commentService.GetCommentAsync(id.Value);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await commentService.GetCommentAsync(id);
            if (comment != null)
            {
                await commentService.DeleteCommentAsync(id);
            }

            return Redirect("/Movies/Details/" + comment.MovieId);
        }
    }
}
