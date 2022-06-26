using Model.Entities;

namespace Repository.Abstract
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentAsync(int id);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<bool> SaveCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
