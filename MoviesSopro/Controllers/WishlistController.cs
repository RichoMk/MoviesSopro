using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Entities;
using Movie.Services.Interfaces;
using MoviesSopro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoviesOnline.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IWishlistServices _wishlistService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMoviesServices _moviesServices;

        public WishlistController(IWishlistServices wishlistService, IHttpContextAccessor httpContextAccessor, IMoviesServices moviesServices)
        {
            _wishlistService = wishlistService;
            _httpContextAccessor = httpContextAccessor;
            _moviesServices = moviesServices;
        }

        public IActionResult Index()
        {
            List<Movies> AllMoviesListFromWishlistByLoggedInUser = new List<Movies>();

            var TotalPriceCount = 0.0;

            // get logged in user id
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var wishlists = _wishlistService.GetAllWishlistByUserId(userId);

            foreach (var item in wishlists)
            {
                var book = _moviesServices.GetMovieById(item.MovieId);
                if (book != null)
                {
                    AllMoviesListFromWishlistByLoggedInUser.Add(book);
                }
            }

            //TotalPriceCount = Math.Round(AllBookListFromWishlistByLoggedInUser.Sum(x => x.Price), 2);

            // init viewmodel
            var movieViewModel = new MoviesViewModel();
            movieViewModel.AllMoviesFromWishlistByLoggedInUser = AllMoviesListFromWishlistByLoggedInUser;
            movieViewModel.WishlistTotalPrice = TotalPriceCount;

            return View(movieViewModel);
        }

        public IActionResult Details(int id)
        {
            var movie = _moviesServices.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            var getMovie = _moviesServices.GetMovieById(Id);

            _wishlistService.DeleteByMovieId(Id);

            return new JsonResult(new { data = getMovie, url = Url.Action("Index", "Wishlist") });
        }

    }
}
