using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie.Data;
using Movie.Entities;
using Movie.Services.Interfaces;
using MoviesSopro.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MoviesSopro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMoviesServices _moviesServices;
        private readonly IWishlistServices _wishlistService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;


        public HomeController(ILogger<HomeController> logger, IMoviesServices moviesServices, IWishlistServices wishlistService, IHttpContextAccessor httpContextAccessor, DataContext dataContext)
        {
            _logger = logger;
            _moviesServices = moviesServices;
            _wishlistService = wishlistService;
            _httpContextAccessor = httpContextAccessor;
            _context = dataContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var movies = _moviesServices.GetAllMovies();

            var movieViewModel = new MoviesViewModel()
            {
                AllMovies = movies
            };
            return View(movieViewModel);
        }
        [HttpPost]
        public IActionResult Index(MoviesViewModel model)
        {
            var searchedstring = model.SearchString;
            if (!String.IsNullOrEmpty(searchedstring))
            {
                string regex_whitespaces = "[ ]{2,}";
                searchedstring = Regex.Replace(searchedstring, regex_whitespaces, " ");
            }

            var seachedmovies = _moviesServices.SearchMovies(searchedstring);

            var movieViewModel = new MoviesViewModel()
            {
                AllMovies = seachedmovies,
                SearchString = model.SearchString
            };

            return View(movieViewModel);
        }

        [HttpPost]
        public JsonResult AddToWishlist(int id)
        {
            var getMovieById = _moviesServices.GetMovieById(id);

            var CheckIfExistsInWishlist = _wishlistService.GetWishlistByMovieId(id);

            if (CheckIfExistsInWishlist == null)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var movieId = getMovieById.Id;
                //var publisherId = getBookById.PublisherId;
                //var actorId = getBookById.ActorNames;
                var categoryId = getMovieById.Categories;

                // init wishlist obj
                var wishlist = new Wishlist
                {
                    UserId = userId,
                    MovieId = movieId,
                    //PublisherId = publisherId,
                    CategoryId = categoryId,
                    DateAdded = DateTime.Now
                };

                _wishlistService.Add(wishlist);

                return new JsonResult(new { data = wishlist });
            }
            else
            {
                return new JsonResult(new { data = "" });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
