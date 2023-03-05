using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace Assignment.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext context;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        [Authorize(Roles = "StoreOwner,Admin")]
        public IActionResult Index()
        {
            return View(context.Categories.ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(string name)
        {
            var category = new Category();
            if(ModelState.IsValid)
            {
                category.Name= name;
                context.Categories.Add(category);
                context.SaveChanges();
                TempData["Message"] = "Create catergory successfull!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            TempData["Message"] = "Delete catergory successfull!";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Detail(int id)
        {
            var category = context.Categories
                .Include(c => c.Book)
                .FirstOrDefault(c => c.Id == id);
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = context.Categories.Find(id);
            return View(category);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(ModelState.IsValid)
            {
                context.Categories.Update(category);
                context.SaveChanges();
                TempData["Message"] = "Edit catergory successfull!";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
    }
}
