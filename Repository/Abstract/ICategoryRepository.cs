using Model.Entities;

namespace Repository.Abstract
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int id);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<bool> SaveCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
