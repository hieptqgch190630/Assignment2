using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Assignment.Controllers
{
    public class RequestController : Controller
    {
        private ApplicationDbContext context;
        public RequestController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View(context.Requests.ToList());
        }
        [Authorize(Roles ="StoreOwner")]
        [HttpGet]
        public IActionResult MakeRequest()
        {
            return View();
        }
        [Authorize(Roles = "StoreOwner")]
        [HttpPost]
        public IActionResult MakeRequest(Request request)
        {
            if(ModelState.IsValid)
            {
                request.Status = false;
                context.Add(request);
                context.SaveChanges();
                return RedirectToAction(nameof(MakeRequest));
            }
            return View(request);
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var request = context.Requests.Find(id);
            context.Requests.Remove(request);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
