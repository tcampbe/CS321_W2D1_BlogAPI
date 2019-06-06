using Microsoft.AspNetCore.Mvc;
using CS321_W2D1_BlogAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace CS321_W2D1_BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private int _nextId = 2;

        // BAD! For illustration purposes only. Any changes made to the array will be lost on next request.
        private List<Post> _posts = new List<Post>
            {
                new Post() { Id = 1, Title = "Post1", Body = "blah blah blah" },
                new Post() { Id = 2, Title = "Post2", Body = "blah blah blah" },
            };

        // GET api/posts
        [HttpGet]
        public IActionResult Get()
        {
            // return OK 200 status and list of posts
            return Ok(_posts);
        }

        // GET api/posts/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // look up post by id
            var post = _posts.FirstOrDefault(p => p.Id == id);
            // if not found, return NotFound (404)
            if (post == null) return NotFound();
            // otherwise return post (200)
            return Ok(post);
        }

        // POST api/posts
        [HttpPost]
        public IActionResult Post([FromBody] Post newPost)
        {
            // BAD! for illustration only. Not a safe way to generate an id in a real system.
            newPost.Id = ++_nextId;
            _posts.Add(newPost);
            return CreatedAtAction("Get", new { Id = newPost.Id }, newPost);
        }

        // PUT api/posts/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Post updatedPost)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            _posts.Remove(post);
            _posts.Add(updatedPost);
            return Ok(updatedPost);
        }

        // DELETE api/posts/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var post = _posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return NotFound();
            _posts.Remove(post);
            return NoContent();
        }
    }
}
