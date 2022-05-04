using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace News_Test.Models.Categories
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
