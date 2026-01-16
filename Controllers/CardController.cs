using Microsoft.AspNetCore.Mvc;
using SergeevaMaria_Lab6_V2_1.Models;
using SergeevaMaria_Lab6_V2_1.Data;
using System.Linq;
using System.Text.Json;

namespace SergeevaMaria_Lab6_V2_1.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardRepository repo;

        public CardController(ICardRepository repository)
        {
            repo = repository;
        }

        public IActionResult All(int n = 0, string sort = null)
        {
            var cards = repo.GetAllCards();
            if (!string.IsNullOrEmpty(sort))
            {
                cards = cards.OrderBy(c => c.GetType().GetProperty(sort)?.GetValue(c)).ToList();
                ViewData["Sort"] = sort;
            }
            if (n > 0)
            {
                cards = cards.Take(n).ToList();
                ViewData["N"] = n;
            }
            return View(cards);
        }

        public IActionResult Details(int id)
        {
            var card = repo.GetAllCards().FirstOrDefault(c => c.Id == id);
            return View(card);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = repo.GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Card card)
        {
            if (ModelState.IsValid)
            {
                if (repo.AddCard(card))
                    return RedirectToAction("Stat");

                ModelState.AddModelError("", "Такая карточка уже существует");
            }

            ViewBag.Categories = repo.GetCategories();
            return View(card);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var card = repo.GetAllCards().FirstOrDefault(c => c.Id == id);
            ViewBag.Categories = repo.GetCategories();
            return View(card);
        }

        [HttpPost]
        public IActionResult Edit(Card card)
        {
            if (ModelState.IsValid)
            {
                repo.UpdateCard(card);
                return RedirectToAction("All");
            }

            ViewBag.Categories = repo.GetCategories();
            return View(card);
        }


        public IActionResult Remove(int id)
        {
            var card = repo.GetAllCards().FirstOrDefault(c => c.Id == id);
            if (card != null)
                repo.RemoveCard(card);
            return RedirectToAction("All");
        }

        public IActionResult Stat()
        {
            var cards = repo.GetAllCards();
            ViewData["Count"] = cards.Count();
            ViewData["Words"] = cards.Select(c => c.Word).Distinct().ToList();
            ViewData["Translations"] = cards.Select(c => c.Translation).Distinct().ToList();
            return View();
        }

        public IActionResult Export(int n = 0, string sort = null)
        {
            var cards = repo.GetAllCards();

            if (!string.IsNullOrEmpty(sort))
            {
                cards = cards.OrderBy(c => c.GetType().GetProperty(sort)?.GetValue(c)).ToList();
            }

            if (n > 0)
            {
                cards = cards.Take(n).ToList();
            }

            var export = cards.Select(c => new {
                c.Id,
                c.Word,
                c.Translation,
                Category = c.Category?.Title
            });

            var json = JsonSerializer.Serialize(export, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            return Content(json, "application/json");
        }


        [HttpGet]
        public IActionResult Test(int n = 0, int CategoryId = 0)
        {
            var cards = repo.GetAllCards()
                .Where(c => CategoryId == 0 || c.CategoryId == CategoryId)
                .OrderBy(c => Guid.NewGuid())
                .ToList();

            if (n > 0)
                cards = cards.Take(n).ToList();

            ViewBag.N = n;
            ViewBag.CategoryId = CategoryId;

            return View(cards);
        }


        [HttpPost]
        public IActionResult Test(List<int> cardIds, IFormCollection form)
        {
            int correct = 0;
            int total = cardIds.Count;

            var cards = repo.GetAllCards().Where(c => cardIds.Contains(c.Id)).ToList();

            foreach (var card in cards)
            {
                string userAnswer = form[$"answer_{card.Id}"];

                var allCorrectTranslations = repo.GetAllCards()
                    .Where(c => c.Word.ToLower() == card.Word.ToLower())
                    .Select(c => c.Translation.ToLower())
                    .ToList();

                if (allCorrectTranslations.Contains(userAnswer.ToLower()))
                {
                    correct++;
                }

            }

            TempData["Msg"] = $"Правильных ответов: {correct} из {total}";
            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult SelectTestOptions()
        {
            var categories = repo.GetCategories().ToList();
            int total = repo.GetAllCards().Count();
            var model = new SelectTestViewModel
            {
                Total = total,
                Categories = categories
            };
            return View(model);

        }

        [HttpGet]
        public IActionResult StartTest(int n, int CategoryId)
        {
            return RedirectToAction("Test", new { n, CategoryId });
        }

        [HttpGet]
        public IActionResult ExportOptions()
        {
            return View();
        }

    }
}
