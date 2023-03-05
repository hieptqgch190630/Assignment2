using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Assignment.Controllers
{
    public class AuthorController : Controller
    {
        private ApplicationDbContext context;
        public AuthorController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View(context.Authors.ToList());
        }
        [Authorize(Roles ="StoreOwner")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles ="StoreOwner")]
        [HttpPost]
        public IActionResult Add(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Authors.Add(author);
                context.SaveChanges();
                TempData["Message"] = "Add author successfull!";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [Authorize(Roles = "StoreOwner")]
        public IActionResult Delete(int id)
        {
            var author = context.Authors.Find(id);
            context.Authors.Remove(author);
            context.SaveChanges();
            TempData["Message"] = "Delete author successfull!";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detail(int id)
        {
            var author = context.Authors
                .Include(c => c.Book)
                .FirstOrDefault(c => c.Id == id);
            return View(author);
        }
        [Authorize(Roles = "StoreOwner")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = context.Authors.Find(id);
            return View(author);
        }
        [Authorize(Roles = "StoreOwner")]
        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                context.Authors.Update(author);
                context.SaveChanges();
                TempData["Message"] = "Edit author successfull!";
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }
    }
}
