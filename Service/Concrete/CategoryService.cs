using AutoMapper;
using Model.Entities;
using Repository.Abstract;
using Service.Abstract;
using Service.DtoModel;

namespace Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public async Task<bool> AddNewCategoryAsync(CategoryInDto categoryInDto)
        {
            var newCategory = mapper.Map<Category>(categoryInDto);
            bool info = await categoryRepository.SaveCategoryAsync(newCategory);
            return info;
        }

        public async Task<CategoryOutDto> GetCategoryAsync(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            var getCategory = mapper.Map<CategoryOutDto>(category);
            return getCategory;
        }

        public async Task<List<CategoryOutDto>> GetAllCategoriesAsync()
        {
            var categories = await categoryRepository.GetAllCategoriesAsync();
            return mapper.Map<List<CategoryOutDto>>(categories);
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryInDto categoryInDto)
        {
            var existingCategory = await categoryRepository.GetCategoryAsync(id);
            if(existingCategory == null)
                return false;
            var updatedCategory = mapper.Map(categoryInDto, existingCategory);

            bool info = await categoryRepository.SaveCategoryAsync(updatedCategory);
            return info;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            bool info  = await categoryRepository.DeleteCategoryAsync(id);
            return info;
        }
    }
}
