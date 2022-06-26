using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface ICategoryService
    {
        Task<bool> AddNewCategoryAsync(CategoryInDto categoryInDto);
        Task<CategoryOutDto> GetCategoryAsync(int id);
        Task<List<CategoryOutDto>> GetAllCategoriesAsync();
        Task<bool> UpdateCategoryAsync(int id, CategoryInDto categoryInDto);
        Task<bool> DeleteCategoryAsync(int id);
    }
}
