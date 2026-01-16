using Microsoft.AspNetCore.Mvc;
using SergeevaMaria_Lab6_V2_1.Data;
using SergeevaMaria_Lab6_V2_1.Models;
using System.Linq;

namespace SergeevaMaria_Lab6_V2_1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICardRepository repo;

        public CategoryController(ICardRepository repository)
        {
            repo = repository;
        }

        public IActionResult All()
        {
            var categories = repo.GetCategories();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            var category = repo.GetCategory(id);
            return View(category);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                var result = repo.AddCategory(category);

                return RedirectToAction("All");
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = repo.GetCategory(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateCategory(category);
                return RedirectToAction("All");
            }
            return View(category);
        }

        public IActionResult Remove(int id)
        {
            var category = repo.GetCategory(id);
            if (category != null)
            {
                repo.RemoveCategory(category);
            }
            return RedirectToAction("All");
        }
    }
}
