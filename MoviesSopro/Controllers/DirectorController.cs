using Microsoft.AspNetCore.Mvc;
using Movie.Entities;
using Movie.Services.Interfaces;
using MoviesSopro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesSopro.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorServices _directorServices;

        public DirectorController(IDirectorServices directorServices)
        {
            _directorServices = directorServices;
        }
        public IActionResult Index()
        {
            var directors = _directorServices.GetAllDirectors();
            return View(directors);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Director director)
        {
            //var director = new Director();
            //director.Id = model.DirectorId;
            //director.Name = model.Name;
            //director.Country = model.Country;
            //director.DateBirth = model.DateBirth;

            _directorServices.Add(director);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var director = _directorServices.GetAuthorById(id);
            return View(director);
        }
        [HttpPost]
        public IActionResult Edit(Director director)
        {
            _directorServices.Edit(director);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var director = _directorServices.GetAuthorById(id);
            return View(director);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var director = _directorServices.GetAuthorById(id);
            return View(director);
        }
        [HttpPost]
        public IActionResult Delete(Director director)
        {
            _directorServices.Delete(director.Id);
            return RedirectToAction(nameof(Index));
        }

    }
}
