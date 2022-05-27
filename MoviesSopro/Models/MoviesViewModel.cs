using Movie.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSopro.Models
{
    public class MoviesViewModel
    {

        // Movies
        public Movies Movie { get; set; }

        public string Title { get; set; }
        public int[] ActorIds { get; set; }
        public string ActorNames { get; set; }
        public string DirectorName { get; set; }
        public int DirectorId { get; set; }
        public int[] CategoryIds { get; set; }
        public string Categories { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string WatchTime { get; set; }
        public string ReleaseDate { get; set; }
        public string CountryOfOrigin { get; set; }
        public double Rating { get; set; }
        public string ShortDescription { get; set; }
        public string TrailerURL { get; set; }
        public string PhotoURL { get; set; }

        // Actors

        //[StringLength(100)]
        public string Name { get; set; }

        //[StringLength(100)]
        public string ActorCountryDTO { get; set; }

        public string DateBirth { get; set; }

        //[StringLength(500)]
        public string ActorShortDescriptionDTO { get; set; }

        //[StringLength(50)]
        public string ActorLanguageDTO { get; set; }

        //[StringLength(50)]
        public string Gender { get; set; }

        public bool Popularity { get; set; }

        //CATEGORY

        [StringLength(50)]
        public string CategoryNameDTO { get; set; }

        //DIRECTOR

        [StringLength(100)]
        public string DirectorNameDTO { get; set; }

        [StringLength(100)]
        public string DirectorCountryDTO { get; set; }

        [StringLength(50)]
        public string Year { get; set; }

        //PUBLISHER

        [StringLength(100)]
        public string PublisherNameDTO { get; set; }

        [StringLength(100)]
        public string PublisherCountryDTO { get; set; }

        [StringLength(50)]
        public string PublisheYearDTO { get; set; }


        #region Wishlist Data
        public double WishlistTotalPrice { get; set; }

        #endregion

        #region Search
        public string SearchString { get; set; }
        #endregion

        #region Other Landing Page Data

        public IEnumerable<Movies> TopPopularMovies { get; set; }

        public IEnumerable<Movies> AllMovies { get; set; }

        public IEnumerable<Actor> AllActorsInMovie { get; set; }

        public IEnumerable<Movies> AllMoviesFromWishlistByLoggedInUser { get; set; }

        #endregion





    }
}
