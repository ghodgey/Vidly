using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Glimpse.Mvc.AlternateType;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CreateNewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public CreateNewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost] //aplies with restful conventions
        public IHttpActionResult NewRentals(NewRentalDto newRental)
        {

            if (!ModelState.IsValid)
                return BadRequest(); //helper method from IHttpActionresult


            try
            {
                if (newRental.MovieId.Count == 0)
                {
                    return BadRequest("No Movie Ids have been given");
                }

                var customer = _context.Customers.SingleOrDefault(c => newRental.CustomerId == c.Id);

                if (customer == null)
                {
                    return BadRequest("CustomerId is not valid");
                }

                var movies = _context.Movies.Where(m => newRental.MovieId.Contains(m.Id)).ToList();

                if (movies.Count != newRental.MovieId.Count)
                    return BadRequest("One or more movie Ids are invalid");


                foreach (var movie in movies)
                {
                    if (movie.NumberAvailable == 0)
                    {
                        return BadRequest("Movie is not available");
                    }
                    movie.NumberAvailable--;
                    var rental = new Rentals
                    {
                        Movies = movie,
                        Customer = customer,
                        DateRented = DateTime.Now
                    };
                    _context.Rentals.Add(rental);
                    
                }

                _context.SaveChanges();
                //return Created(new Uri(Request.RequestUri + "/" + rentals.Id), newRental);//URI unified resource identifier = /api/customers/10
                //When you use created you provide the URL to the newly created resource. Since we are now creating multiple new records we
                //dont use created
                return Ok();


            }

            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);

            }
        }

    }
}
