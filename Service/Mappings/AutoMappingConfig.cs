using AutoMapper;
using Model.Entities;
using Service.DtoModel;

namespace Service.Mappings
{
    public class AutoMappingConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                #region Category

                cfg.CreateMap<CategoryInDto, Category>();
                cfg.CreateMap<Category, CategoryOutDto>();

                #endregion

                #region Movie

                cfg.CreateMap<MovieInDto, Movie>();
                cfg.CreateMap<Movie, MovieOutDto>()
                    .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.Category.Name));

                #endregion

                #region Comment

                cfg.CreateMap<CommentInDto, Comment>();
                cfg.CreateMap<Comment, CommentOutDto>()
                    .ForMember(dest => dest.UserName, act => act.MapFrom(src => src.User.UserName));

                #endregion

            }).CreateMapper();
    }
}
