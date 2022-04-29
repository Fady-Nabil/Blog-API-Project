using BlogAPI.Data;
using BlogAPI.Models.Dto;
using BlogAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogDBContext DBContext;
        public PostsController(BlogDBContext dBContext)
        {
            DBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await DBContext.Posts.ToListAsync();
            return Ok(posts);
        }
        
        [HttpGet]
        [Route("{Id:int}")]
        [ActionName("GetPostById")]
        public async Task<IActionResult> GetPostById(int Id)
        {
            var post = await DBContext.Posts.FirstOrDefaultAsync(x => x.Id == Id);
            if(post != null)
            {
                return Ok(post);
            }
            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> AddPost(AddPostRequest AddPostRequest)
        {
            var post = new Post()
            {
                Title = AddPostRequest.Title,
                Body = AddPostRequest.Body,
                CreationDate = AddPostRequest.CreationDate
            };
            await DBContext.Posts.AddAsync(post);
            await DBContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostById), new { Id = post.Id }, post);
        }
        
        [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdatePost([FromRoute] int Id, UpdatePostRequest UpdatePostRequest)
        {
            var ExistingPost = await DBContext.Posts.FindAsync(Id);
            if (ExistingPost != null)
            {
                ExistingPost.Title = UpdatePostRequest.Title;
                ExistingPost.Body = UpdatePostRequest.Body;
                ExistingPost.CreationDate = UpdatePostRequest.CreationDate;
                await DBContext.SaveChangesAsync();
                return Ok(ExistingPost);
            }
            return NotFound();
        }
        
        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeletePost(int Id)
        {
            var ExistingPost = await DBContext.Posts.FindAsync(Id);
            if (ExistingPost != null)
            {
                DBContext.Remove(ExistingPost);
                await DBContext.SaveChangesAsync();
                return Ok(ExistingPost);
            }
            return NotFound();
        }
    }
}
