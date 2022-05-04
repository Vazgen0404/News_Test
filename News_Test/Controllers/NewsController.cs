using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using News_Test.Models.News;
using News_Test.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository<News> _newsContext;
        private readonly ILogger<NewsController> _logger;
        private readonly IMapper _mapper;

        public NewsController(INewsRepository<News> repository, ILogger<NewsController> logger, IMapper mapper)
        {
            _newsContext = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> Get()
        {
            var news = await _newsContext.GetAll();
            var newsDto = _mapper.Map<IEnumerable<NewsDTO>>(news);

            return Ok(newsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsDTO>> Get(int id)
        {
            var news = await _newsContext.Get(id);

            if (news == null)
            {
                _logger.LogError("News Not Found");
                return NotFound();
            }
            var newsDto = _mapper.Map<NewsDTO>(news);

            return Ok(newsDto);
        }

        [HttpPost]
        public async Task<ActionResult<NewsDTO>> Post([FromBody] NewsDTO newsDto)
        {
            var news = _mapper.Map<News>(newsDto);
            news.Date = DateTime.Now;

            await _newsContext.Add(news);

            _logger.LogInformation("News created");
            return CreatedAtAction("Get", new { id = news.Id }, newsDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] NewsDTO newsDto)
        {
            if (id != newsDto.Id)
            {
                _logger.LogError("Ids dont match ");

                return BadRequest();
            }

            var news = _mapper.Map<News>(newsDto);
            news.Date = DateTime.Now;

            try
            {
                await _newsContext.Update(news);
                _logger.LogInformation("News updated");

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
            var news = await _newsContext.Get(id);
            if (news == null)
            {
                _logger.LogError("Id does not exist");
                return NotFound();
            }

            await _newsContext.Delete(id);
            _logger.LogInformation("News deleted");

            return NoContent();
        }
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetByCategory(int categoryId)
        {
            var news = await _newsContext.GetByCategory(categoryId);
            
            var newsDto = _mapper.Map<IEnumerable<NewsDTO>>(news);

            return Ok(newsDto);
        }
        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetByDate([FromQuery] DateTime startDate, [FromQuery] DateTime finishDate)
        {
            var news = await _newsContext.GetByDate(startDate,finishDate);

            var newsDto = _mapper.Map<IEnumerable<NewsDTO>>(news);

            return Ok(newsDto);
        }
        [HttpGet("text")]
        public async Task<ActionResult<IEnumerable<NewsDTO>>> GetByText(string text)
        {
            var news = await _newsContext.GetByText(text);

            var newsDto = _mapper.Map<IEnumerable<NewsDTO>>(news);

            return Ok(newsDto);
        }
        private bool Exists(int id)
        {
            return _newsContext.Exists(id);
        }
    }
}
