using Microsoft.EntityFrameworkCore;
using News_Test.Context;
using News_Test.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News_Test.Services
{
    public class NewsRepository : INewsRepository<News>
    {
        private readonly MyDbContext _context;
        public NewsRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(News entity)
        {
            _context.News.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var news = _context.News.FirstOrDefault(x => x.Id == id);

            _context.News.Remove(news);
            return await _context.SaveChangesAsync();
        }

        public async Task<News> Get(int id)
        {
            return await _context.News.Include(n => n.Categories).FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<News>> GetAll()
        {
            return await _context.News.Include(n => n.Categories).ToListAsync();
        }

        public async Task<int> Update(News entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
        public bool Exists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        public async Task<IEnumerable<News>> GetByCategory(int categoryId)
        {
            var News = from news in _context.News
                        from category in news.Categories
                        where categoryId == category.Id
                        select news;


            return await News.Include(n => n.Categories).ToListAsync();
        }

        public async Task<IEnumerable<News>> GetByDate(DateTime startDate, DateTime finishDate)
        {
            var news = await _context.News.Include(n => n.Categories).Where(n => n.Date >= startDate && n.Date <= finishDate).ToListAsync();
            return news;
        }

        public async Task<IEnumerable<News>> GetByText(string text)
        {
            var news = _context.News.Include(n => n.Categories).Where(n => n.Text.ToLower().Contains(text.ToLower()) ||
                                                                     n.Title.ToLower().Contains(text.ToLower()));
            return await news.ToListAsync();
        }
    }
}
