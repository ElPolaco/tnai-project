using AutoMapper;
using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
        }

        public async Task<CommentOutDto> GetCommentAsync(int id)
        {
            var comment = await commentRepository.GetCommentAsync(id);
            var getComment = mapper.Map<CommentOutDto>(comment);
            return getComment;
        }

        public async Task<bool> AddNewCommentAsync(CommentInDto commentInDto)
        {
            var newComment = mapper.Map<Comment>(commentInDto);
            newComment.LastModified = DateTime.Now;
            bool info = await commentRepository.SaveCommentAsync(newComment);
            return info;
        }

        public async Task<bool> UpdateCommentAsync(int id, CommentInDto commentInDto)
        {
            var existingComment = await commentRepository.GetCommentAsync(id);
            if (existingComment == null)
                return false;
            var updatedComment = mapper.Map(commentInDto, existingComment);
            updatedComment.LastModified = DateTime.Now;

            bool info = await commentRepository.SaveCommentAsync(updatedComment);
            return info;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await commentRepository.GetCommentAsync(id);
            bool info = await commentRepository.DeleteCommentAsync(id);
            return info;
        }
    }
}
