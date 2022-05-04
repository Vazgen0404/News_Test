using AutoMapper;
using News_Test.Models.Categories;
using News_Test.Models.News;

namespace News_Test.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<News, NewsDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
