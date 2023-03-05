using Assignment.Data;
using Assignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Assignment.Controllers
{
    public class OrderController : Controller
    {
        //khai báo ApplicationDbcontext để truy xuất và thay dổi dữ liệu của bảng
        private ApplicationDbContext context;
        public OrderController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        [Authorize(Roles ="Customer,Admin")]
        public IActionResult Index()
        {
            var orders = context.Orders.Include(c => c.Book).ToList();
            return View(orders);
        }
        [Authorize(Roles ="Customer")]
        public IActionResult IndexForCurrent()
        {
            var orders = context.Orders
                .Include(c => c.Book)
                .Where(c => c.UserEmail == User.Identity.Name)
                .ToList();
            return View(orders);
        }
        [Authorize(Roles ="Admin,Customer")]
        public IActionResult Delete(int id)
        {
            var order = context.Orders.Find(id);
            var book = context.Books.Find(order.BookId);
            book.Quantity += order.OrderQuantity;
            context.Books.Update(book);
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("IndexForCurrent", "Order");
        }
        public IActionResult Delete1(int id)
        {
            var order = context.Orders.Find(id);
            var book = context.Books.Find(order.BookId);
            book.Quantity += order.OrderQuantity;
            context.Books.Update(book);
            context.Orders.Remove(order);
            context.SaveChanges();
            return RedirectToAction("Index","Order");
        }
        [Authorize(Roles ="Customer")]
        [HttpPost]
        public IActionResult Make(int id, int quantity)
        {
            var order = new Order();
            var book = context.Books.Find(id);
            order.Book = book;
            order.BookId= id;
            order.Title = book.Title;
            order.OrderQuantity = quantity;
            order.OrderPrice = book.Price * quantity;
            order.OrderDate = DateTime.Now;
            order.UserEmail = User.Identity.Name;
            //add Orders vào DB
            context.Orders.Add(order);
            //trừ quantity của book
            book.Quantity -= quantity;
            context.Books.Update(book);
            //lưu cập nhật vào Db
            context.SaveChanges();
            TempData["Success"] = "Order book successfully!";
            //redirect về trang book store
            return RedirectToAction("Store", "Book");
        }
        [HttpPost]
        public IActionResult Search(string email)
        {
            var orders = context.Orders.Where(c => c.UserEmail.Contains(email))
                                        .Include(c => c.Book).ToList();
            return View("Index", orders);
        }
    }
}
