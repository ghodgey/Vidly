using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            // var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //var movies = _context.Movies.Include(m => m.Genres).ToList();
            if (User.IsInRole("CanManageMovies"))
            {
                return View("List");
            }

            return View("ReadOnlyList");

        }

        [Route("movies/details/{moviesId}")]
        public ActionResult Details(int? moviesId)
        {
            var movies = _context.Movies.Include(m => m.Genres).SingleOrDefault(m => m.Id == moviesId);
            if (movies != null)
            {
                return View(movies);

            }
            if(!moviesId.HasValue)
             return HttpNotFound();
            return HttpNotFound();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            
            var movieFormView = new MoviesFormViewModel
            {
                Genres = genres
            };
            return View("MoviesForm",movieFormView);
            //implement adding a new movie view
            
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        [Route("movies/edit/{moviesId}")]
        public ActionResult Edit(int? moviesId)
        {
            var movie = _context.Movies.Single(m => m.Id == moviesId);

            var viewModel = new MoviesFormViewModel(movie)
            {

                Genres = _context.Genres
            };

            return View("MoviesForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movies movies)
        {
            if (!ModelState.IsValid)
            {
                var moviesForm = new MoviesFormViewModel(movies)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MoviesForm", moviesForm);
            }
            if (movies.Id == 0)
            {
                movies.DateAdded = DateTime.Now;
                _context.Movies.Add(movies);

            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movies.Id);
                movieInDb.Name = movies.Name;
                movieInDb.GenresId = movies.GenresId;
                movieInDb.NumberInStock = movies.NumberInStock;
                movieInDb.ReleaseDate = movies.ReleaseDate;


            }
                
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");


        }
    }
}