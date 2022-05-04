using News_Test.Models.Categories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace News_Test.Models.News
{
    public class News
    {
        public int Id { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MinLength(50)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public List<Category> Categories { get; set; } = new();
    }
}
