using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using Movie.Entities;
using Movie.Entities.Logger;
using Movie.Services.Interfaces;
using MoviesSopro.Models;

namespace MoviesSopro.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesServices _moviesServices;
        private readonly IActorServices _actorServices;
        private readonly IDirectorServices _directorServices;
        private readonly IPublisherServices _publisherServices;
        private readonly ICategoryServices _categoryServices;
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieActorService _movieActorService;
        private readonly IMovieCategoryService _movieCategoryService;


        public MoviesController(IMoviesServices moviesServices,
            IActorServices actorServices,
            IDirectorServices directorServices,
            IPublisherServices publisherServices,
            ICategoryServices categoryServices,
            ILogger<MoviesController> logger,
            IMovieActorService movieActorService,
            IMovieCategoryService movieCategoryService)
        {
            _moviesServices = moviesServices;
            _actorServices = actorServices;
            _directorServices = directorServices;
            _publisherServices = publisherServices;
            _categoryServices = categoryServices;
            _logger = logger;
            _movieActorService = movieActorService;
            _movieCategoryService = movieCategoryService;
        }

        public IActionResult Index()
        {
            var AllMovies = _moviesServices.GetAllMovies();
            _logger.LogInformation(LoggerMessageDisplay.MoviesListed);
            return View(AllMovies);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var categories = _categoryServices.GetAllCategories();
            var publishers = _publisherServices.GetAllPublishers();
            var directors = _directorServices.GetAllDirectors();
            var actors = _actorServices.GetAllActors();

            var (Categories, Publishers, Directors, Actors) = _moviesServices.FillDropdowns(categories, publishers, directors, actors);

            ViewBag.CategoriesList = Categories;
            ViewBag.PublishersList = Publishers;
            ViewBag.DirectorsList = Directors;
            ViewBag.ActorList = Actors;

            return View();
        }
        [HttpPost]
        public IActionResult Create(MoviesViewModel model)
        {

            if (ModelState.IsValid)
            {
                var movie = new Movies
                {
                    Title = model.Title,
                    Language = model.Language,
                    PublisherName = model.PublisherName,
                    Rating = model.Rating,
                    ReleaseDate = model.ReleaseDate,
                    WatchTime = model.WatchTime,
                    ShortDescription = model.ShortDescription,
                    DirectorId = model.DirectorId,
                    DirectorName = model.DirectorName,
                    CountryOfOrigin = model.CountryOfOrigin,
                    Country = model.Country,
                    PublisherId = model.PublisherId,
                    TrailerURL = model.TrailerURL,
                    ActorNames = _actorServices.GetAllActorNames(model.ActorIds),
                    Categories = _categoryServices.GetAllCategories(model.CategoryIds)
                };

                _moviesServices.Add(movie);

                List<MovieActor> movieActors = new List<MovieActor>();
                List<MovieCategory> movieCategories = new List<MovieCategory>();

                foreach (var actorId in model.ActorIds)
                {
                    var actor = _actorServices.GetActorById(actorId);
                    movieActors.Add(new MovieActor { Actor = actor, Movie = movie });
                }
                _movieActorService.AddMovieActorsList(movieActors);

                foreach (var categoryId in model.CategoryIds)
                {
                    var category = _categoryServices.GetCategoryById(categoryId);
                    movieCategories.Add(new MovieCategory { Category = category, Movie = movie });
                }
                _movieCategoryService.AddMovieCategoriesList(movieCategories);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _moviesServices.GetMovieById(id);

            var categories = _categoryServices.GetAllCategories();
            var publishers = _publisherServices.GetAllPublishers();
            var directors = _directorServices.GetAllDirectors();
            var actors = _actorServices.GetAllActors();

            var (Categories, Publishers, Directors, Actors) = _moviesServices.FillDropdowns(categories, publishers, directors, actors);

            ViewBag.GenreList = Categories;
            ViewBag.ProducerList = Publishers;
            ViewBag.DirectorList = Directors;
            ViewBag.ActorList = Actors;

            return View(movie);
        }
        [HttpPost]
        public IActionResult Edit(int id, Movies movie)
        {
            _moviesServices.Edit(movie);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _moviesServices.GetMovieById(id);
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _moviesServices.GetMovieById(id);
            _moviesServices.Delete(movie.Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = _moviesServices.GetMovieById(id);
            var allActors = _actorServices.GetAllActors();

            var actorsInMovie = movie.ActorNames.ToLower().Split(",");

            List<Actor> actors = new List<Actor>();

            foreach (var actor in allActors)
            {
                foreach (var item in actorsInMovie)
                {
                    var actorNames = actor.Name.ToLower();

                    if (actorNames == item)
                    {
                        actors.Add(actor);
                    }
                }
            }

            var moviemodel = new MoviesViewModel()
            {
                Movie = movie,
                AllActorsInMovie = actors
            };

            return View(moviemodel);
        }

        [HttpPost]
        public IActionResult UploadPhoto()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "Photos");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);


                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = fileName;

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    _logger.LogInformation(LoggerMessageDisplay.PhotoUploaded);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggerMessageDisplay.PhotoUploadedError + " | " + ex);
                return StatusCode(500, "Internal Server Error" + ex);
            }

        }
    }
}
