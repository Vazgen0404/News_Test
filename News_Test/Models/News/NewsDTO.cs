using News_Test.Models.Categories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News_Test.Models.News
{
    public class NewsDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MinLength(50)]
        public string Text { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new();
    }
}
