using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly MovieDbContext _movieDbContext;
         public  MovieController(MovieDbContext movieDbContext) {
          _movieDbContext = movieDbContext;
}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if(_movieDbContext.Movies == null)
            {
                return NotFound();
            }
            return await _movieDbContext.Movies.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _movieDbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return  movie;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _movieDbContext.Movies.Add(movie);
            _movieDbContext.SaveChanges();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.id }, movie);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> PutMovie(int id , Movie movie) {
        if(id != movie.id)
            {
                return BadRequest();
            }
            _movieDbContext.Entry(movie).State = EntityState.Modified;
            try
            {
                await _movieDbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!MovieExist(id))
                {
                    return NotFound();
                }
                else {
                    throw;
                }
            }
            return NoContent();
        }
        private bool MovieExist(long id) { 
        return (_movieDbContext.Movies?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int id)
        {
            if (_movieDbContext.Movies == null)
            {
                return NotFound();
            }
            var movie = await _movieDbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
             _movieDbContext.Movies.Remove(movie);
            await _movieDbContext.SaveChangesAsync();
            return NoContent();
        }      
        }
}
