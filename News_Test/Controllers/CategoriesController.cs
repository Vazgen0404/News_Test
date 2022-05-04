using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News_Test.Models.Categories;
using News_Test.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> _categoriesContext;
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IRepository<Category> repository, ILogger<CategoriesController> logger, IMapper mapper)
        {
            _categoriesContext = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoriesContext.GetAll();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoriesContext.Get(id);

            if (category == null)
            {
                _logger.LogError("Category Not Found");
                return NotFound();
            }
            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Post([FromBody] CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoriesContext.Add(category);

            _logger.LogInformation("Category created");
            return CreatedAtAction("Get", new { id = category.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                _logger.LogError("Ids dont match ");

                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDto);
            try
            {
                await _categoriesContext.Update(category);
                _logger.LogInformation("Category updated");

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Exists(id))
                {
                    _logger.LogError("Id does not exist");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoriesContext.Get(id);
            if (category == null)
            {
                _logger.LogError("Id does not exist");
                return NotFound();
            }

            await _categoriesContext.Delete(id);
            _logger.LogInformation("Category deleted");

            return NoContent();
        }
        private bool Exists(int id)
        {
            return _categoriesContext.Exists(id);
        }

    }
}
