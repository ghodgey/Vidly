using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Rentals
    {
        public int Id { get; set; }

        [Required]
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }

        [Required]
        public Movies Movies { get; set; }
        public int MoviesId { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }
        
    }
}