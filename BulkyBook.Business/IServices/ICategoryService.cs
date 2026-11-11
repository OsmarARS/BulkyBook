using BulkyBook.Models;

namespace BulkyBook.Business.IServices;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);
    Task<Category> CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);

    Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryId = null);
}