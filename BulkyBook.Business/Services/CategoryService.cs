using BulkyBook.Business.IServices;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Business.Services;

public class CategoryService(ApplicationDbContext context) : ICategoryService
{
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await context.Categories.ToListAsync();
    }
    
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await context.Categories.FindAsync(id);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) throw new KeyNotFoundException($"Category {id} not found");
        
        context.Categories.Remove(category);
        await context.SaveChangesAsync();    
    }

    public async Task<bool> IsCategoryNameUniqueAsync(string name, int? categoryId = null)
    {
        if (categoryId.HasValue)
        {
            return !await context.Categories.AnyAsync(category => 
                category.Name.ToLower() == name.ToLower() && 
                category.Id != categoryId.Value);
        }
        else
        {
            return !await context.Categories.AnyAsync(category => 
                category.Name.ToLower() == name.ToLower());
        }
    }
}