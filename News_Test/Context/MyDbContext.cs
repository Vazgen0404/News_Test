using Microsoft.EntityFrameworkCore;
using News_Test.Models.Categories;
using News_Test.Models.News;

namespace News_Test.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Category> Categories { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
