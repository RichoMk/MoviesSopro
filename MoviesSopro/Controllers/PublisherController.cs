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
    public class PublisherController : Controller
    {
        private readonly IPublisherServices _publisherServices;

        public PublisherController(IPublisherServices publisherServices)
        {
            _publisherServices = publisherServices;
        }
        public IActionResult Index()
        {
            var publisher = _publisherServices.GetAllPublishers();
            return View(publisher);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(MoviesViewModel model)
        {
            var publisher = new Publisher
            {
                Name = model.Name,
                Year = model.Year,
                Id = model.PublisherId,
                Country = model.Country
            };
            _publisherServices.Add(publisher);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
           var publisher =  _publisherServices.GetPublisherById(id);
            return View(publisher);
        }
        [HttpPost]
        public IActionResult Edit(Publisher publisher)
        {
            _publisherServices.Edit(publisher);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var publisher = _publisherServices.GetPublisherById(id);
            return View(publisher);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var publisher = _publisherServices.GetPublisherById(id);
            return View(publisher);
        }
        [HttpPost]
        public IActionResult Delete(Publisher publisher)
        {
            _publisherServices.Delete(publisher.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
