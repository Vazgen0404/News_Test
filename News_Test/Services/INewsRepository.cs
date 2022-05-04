using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Test.Services
{
    public interface INewsRepository<News> : IRepository<News> where News : class
    {
        Task<IEnumerable<News>> GetByCategory(int categoryId);
        Task<IEnumerable<News>> GetByDate(DateTime startDate, DateTime finishDate);
        Task<IEnumerable<News>> GetByText(string text);

    }
}
