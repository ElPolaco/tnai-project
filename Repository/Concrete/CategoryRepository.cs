using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Model.Entities;
using Repository.Abstract;


namespace Repository.Concrete
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Category> GetCategoryAsync(int id) => await Context.Categories.Include(x => x.Movies).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<List<Category>> GetAllCategoriesAsync() => await Context.Categories.Include(x => x.Movies).ToListAsync();

        public async Task<bool> SaveCategoryAsync(Category category)
        {
            if (category == null)
                return false;

            try
            {
                Context.Entry(category).State = category.Id == default(int) ? EntityState.Added : EntityState.Modified;

                await Context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);
            if (category == null)
                return true;

            Context.Categories.Remove(category);

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
