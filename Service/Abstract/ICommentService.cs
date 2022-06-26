using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface ICommentService
    {
        Task<CommentOutDto> GetCommentAsync(int id);
        Task<bool> AddNewCommentAsync(CommentInDto commentInDto);
        Task<bool> UpdateCommentAsync(int id, CommentInDto commentInDto);
        Task<bool> DeleteCommentAsync(int id);
    }
}
