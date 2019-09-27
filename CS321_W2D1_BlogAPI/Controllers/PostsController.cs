using Microsoft.AspNetCore.Mvc;
using CS321_W2D1_BlogAPI.Models;
using System.Collections.Generic;
using System.Linq;
using CS321_W2D1_BlogAPI.Services;

namespace CS321_W2D1_BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        // Constructor
        // IPostService is automatically injected by the ASP.NET framework, if you've
        // configured it properly in Startup.ConfigureServices()
        public PostsController(IPostService postService) /* DONE: add a parameter of type IPostService */
        {
            _postService = postService;             //DONE: keep a reference to the service so we can use it in methods below
        }

        // get all posts
        // GET api/posts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postService.GetAll());
            // DONE: return OK 200 status and list of posts
        }

        // get specific post by id
        // GET api/posts/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // look up post by id
            var post = _postService.Get(id);
            // TODO: use _postsService to get post by id

            // if not found, return 404 NotFound 
            if (post == null) return NotFound();

            // otherwise return 200 OK and the Post
            return Ok(post);
        }

        // create a new post
        // POST api/posts
        [HttpPost]
        public IActionResult Post([FromBody] Post newPost)
        {
            // add the new post
            _postService.Add(newPost);
            // TODO: use _postService to add newPost

            // return a 201 Created status. This will also add a "location" header
            // with the URI of the new post. E.g., /api/posts/99, if the new is 99
            return CreatedAtAction("Get", new { Id = newPost.Id }, newPost);
        }

        // update an existing post
        // PUT api/posts/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Post updatedPost)
        {
            Post post = _postService.Get(id);
            // TODO: use _postService to update post. store returned Post in the post variable.
            if (post == null) return NotFound();
            return Ok(_postService.Update(updatedPost));
        }

        // delete an existing post
        // DELETE api/posts/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _postService.Get(id);
            // TODO: use _postService to get post by id
            if (post == null) return NotFound();
            // TODO: use _postService to update post
            _postService.Remove(post);
            return NoContent();
        }
    }
}
