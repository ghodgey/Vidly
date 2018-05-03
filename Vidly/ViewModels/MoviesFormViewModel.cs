using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MoviesFormViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // these are called data annotations
        [Required]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public int? GenresId { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }


        [Display(Name = "Number in stock")]
        [Range(1, 20)]
        [Required]
        public int? NumberInStock { get; set; }

        public IEnumerable<Genres> Genres { get; set; }

        public string Title => Id != 0 ? "Edit Movie" : "New Movie";

        public MoviesFormViewModel()
        {
            Id = 0;
        }

        public MoviesFormViewModel(Movies movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenresId = movie.GenresId;
        }
    }
}