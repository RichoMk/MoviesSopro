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
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        public IActionResult Index()
        {
            var categories = _categoryServices.GetAllCategories();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            //var category = new Category
            //{
            //    Id = model.CategoryId,
            //    Name = model.Name
            //};
            _categoryServices.Add(category);
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryServices.GetCategoryById(id);

            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _categoryServices.Edit(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var category = _categoryServices.GetCategoryById(id);
            return View(category);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _categoryServices.GetCategoryById(id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
           _categoryServices.Delete(category.ID);

            return RedirectToAction(nameof(Index));
        }
    }
}
