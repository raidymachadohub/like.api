using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Like.Domain.Entities;
using Like.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Like.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private static Cont _CONT = new Cont();
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlog()
        {
            var result = await _blogService.GetAll();
            return result.OrderByDescending(x => x.Id).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetPostsId(int id)
        {
            var blog = await _blogService.GetById(id);

            if (blog == null)
            {
                return NotFound();
            }
            return blog;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {

                await _blogService.Put(id, dto);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog dto)
        {
            await _blogService.Post(dto);
            return CreatedAtAction("GetBlog", new { id = dto.Id }, dto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Blog>> DeletePost(int id)
        {
            var blog = await _blogService.Delete(id);
            if (blog == null)
            {
                return NotFound();
            }
            return blog;
        }

        [HttpPut("/api/like/{id}")]
        public async Task<ActionResult<Blog>> LikeBlog(int id)
        {
            //SendQueueRbMQ consumer = new SendQueueRbMQ();

            //var json = new
            //{
            //    Id = id.ToString(),
            //    Type = "Like"
            //};
            //consumer.Send(JsonConvert.SerializeObject(json));

            _blogService.Like(id);
            return Ok(await _blogService.GetById(id));
        }


        [HttpPut("/api/unlike/{id}")]
        public async Task<ActionResult<Blog>> UnLikeBlog(int id)
        {
            //SendQueueRbMQ consumer = new SendQueueRbMQ();

            //var json = new
            //{
            //    Id = id.ToString(),
            //    Type = "UnLike"
            //};
            //consumer.Send(JsonConvert.SerializeObject(json));

            _blogService.UnLike(id);
            return Ok(await _blogService.GetById(id));
        }
    }
}
