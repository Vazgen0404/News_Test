using Microsoft.EntityFrameworkCore;
using News_Test.Context;
using News_Test.Models.Categories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Test.Services
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly MyDbContext _context;
        public CategoryRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Category entity)
        {
            _context.Categories.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            _context.Categories.Remove(category);
            return await _context.SaveChangesAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.Include(c => c.News).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _context.Categories.Include(c => c.News).ToListAsync();
        }

        public async Task<int> Update(Category entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public bool Exists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }
    }
}
