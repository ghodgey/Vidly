using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;


namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genres)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query) );

            var moviesInDb = moviesQuery
                .ToList()
                .Select(Mapper.Map<Movies, MoviesDto>);
            
            return Ok(moviesInDb);
        }

        // api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            return Ok(Mapper.Map<Movies, MoviesDto>(movieInDb));

        }
        
        // api/movies/id
        [HttpPut]
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie(int id, MoviesDto moviesDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map<MoviesDto, Movies>(moviesDto, movieInDb);

            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);

            _context.SaveChanges();

            return Ok();

        }

        [HttpPost]
        [System.Web.Mvc.Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie(MoviesDto moviesDto) //when the clients sends in a customer it sends it as a dto
        {
            if (!ModelState.IsValid)
                return BadRequest(); //helper method from IHttpActionresult

            var movies = Mapper.Map<MoviesDto, Movies>(moviesDto);//in create method we receive the Dto and now we want to map it
            //to the customer object - pass in the method the customerDto object

            _context.Movies.Add(movies);
            _context.SaveChanges();
            moviesDto.Id = movies.Id;

            return Created(new Uri(Request.RequestUri + "/" + movies.Id), moviesDto);//URI unified resource identifier = /api/customers/10
            //second argument is the actual object that was created
        }




















    }
}
