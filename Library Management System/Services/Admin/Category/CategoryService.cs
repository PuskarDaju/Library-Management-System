using Library_Management_System.Data;
using Library_Management_System.DTOs.Category;
using Microsoft.EntityFrameworkCore;


namespace Library_Management_System.Services.Admin.Category;

public class CategoryService(ApplicationDbContext dbContext) : ICategoryService
{
    private readonly ApplicationDbContext _dbContext=dbContext;
   
    
    public async Task<bool> CreateCategoryAsync(CreateCategory dto)
    {
        Models.Category category = new Models.Category
        {
            CategoryName = dto.CategoryName,
        };
        await _dbContext.Category.AddAsync(category);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Models.Category?> GetCategoryByIdAsync(int categoryId)
    {
        Models.Category? category = await _dbContext.Category.FindAsync(categoryId);
        return category ?? null;
    }
    public async Task<bool> GetCategoryByNameAsync(string categoryName)
    {
        return await _dbContext.Category
            .AnyAsync(c => c.CategoryName == categoryName);
    }

    public async Task<bool> DeleteCategoryByIdAsync(int categoryId)
    {
        var category =await GetCategoryByIdAsync(categoryId);
        if (category == null) return false;
        _dbContext.Remove(category);
        await _dbContext.SaveChangesAsync();
        return true;

    }

    public async Task<bool> UpdateCategoryByIdAsync(UpdateCategory dto)
    {
        var category = await GetCategoryByIdAsync(dto.CategoryId);
        if(category==null) return false;
        category.CategoryName = dto.CategoryName;
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<Models.Category>> GetAllCategoriesAsync()
    {
       return await  _dbContext.Category.ToListAsync();
    }
}