using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookManagement.Core.Models;
using BookManagement.Core.Infrastructure;

namespace BookManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookManagementDb db = new BookManagementDb();
        public ActionResult Index(int categoryId = 0)
        {
            // Get all book
            if(categoryId == 0)
            {
                ViewBag.Category = "All";
                return View(db.Books.ToList());
            }
            
            // Get book by id category
            Category ca = db.Categories.Where(c => c.Id == categoryId).ToList()[0];
            ViewBag.Category = ca.Name;

            List<Book> list = new List<Book>();
            list = db.Books.Where(b => b.Category.Id == categoryId).ToList();
            return View(list);
        }

        public ActionResult DetailBook(int bookId = 0)
        {
            Book book = db.Books.Where(b => b.Id == bookId).ToList()[0];

            return View(book);
        }

        // List categories
        public PartialViewResult _CategoriesView()
        {
            return PartialView(db.Categories.ToList());
        }

        // Login
        public PartialViewResult _LoginView()
        {
            return PartialView();
        }
    }
}
