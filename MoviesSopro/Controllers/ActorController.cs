using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movie.Entities;
using Movie.Entities.Logger;
using Movie.Services.Interfaces;
using MoviesSopro.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MoviesSopro.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorServices _actorService;
        private readonly ILogger<ActorController> _logger;

        public ActorController(IActorServices actorService, ILogger<ActorController> logger)
        {
            _actorService = actorService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var actors = _actorService.GetAllActors();
            return View(actors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                //var actor = new Actor();
                //actor.Name = model.Name;
                //actor.Country = model.Country;
                //actor.Language = model.Language;
                //actor.ShortDescription = model.ShortDescription;
                //actor.Popularity = model.Popularity;
                //actor.Gender = model.Gender;
                //actor.DateBirth = model.DateBirth;

                _actorService.Add(actor);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _actorService.GetActorById(id);
            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor)
        {
            _actorService.Edit(actor);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var actor = _actorService.GetActorById(id);
            return View(actor);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var actor = _actorService.GetActorById(id);
            return View(actor);
        }
        [HttpPost]
        public IActionResult Delete(Actor actor)
        {
            _actorService.Delete(actor.Id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult UploadActorPhoto()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "ActorPhotos");
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
