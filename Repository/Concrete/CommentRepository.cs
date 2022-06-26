using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Repository.Abstract;

namespace Repository.Concrete
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Comment> GetCommentAsync(int id) => await Context.Comments.Include(x => x.Movie).Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<Comment>> GetAllCommentsAsync() => await Context.Comments.Include(x => x.Movie).Include(x => x.User).ToListAsync();

        public async Task<bool> SaveCommentAsync(Comment comment)
        {
            if (comment == null)
                return false;

            try
            {
                Context.Entry(comment).State = comment.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await GetCommentAsync(id);
            if (comment == null)
                return true;

            Context.Comments.Remove(comment);

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
