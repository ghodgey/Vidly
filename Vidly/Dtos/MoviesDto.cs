using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MoviesDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // these are called data annotations
        public int Id { get; set; }

        [Required] public string Name { get; set; }

        //public Genres Genres { get; set; }

        [Display(Name = "Genre")] public int GenresId { get; set; }

        [Display(Name = "Release Date")] public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name = "Number in stock")]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

    }
}