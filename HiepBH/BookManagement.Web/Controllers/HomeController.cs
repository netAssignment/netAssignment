using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookManagement.Core.Models;
using BookManagement.Core.Infrastructure;
using System.Web.Security;

namespace BookManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookManagementDb db = new BookManagementDb();
        private const int _numBooks = 10;
        public ActionResult Index(int categoryId = 0)
        {
            // Get all book
            if(categoryId == 0)
            {
                ViewBag.Category = "All";

                var listBook = (from b in db.Books select b).OrderByDescending(book => book.Id).Take(_numBooks);
                return View(listBook.ToList());
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
            ViewBag.LinkBook = "http://localhost/book/Home/DetailBook?bookId=" + bookId.ToString();
            Book book = db.Books.Where(b => b.Id == bookId).ToList()[0];

            return View(book);
        }

        // List categories
        public PartialViewResult _CategoriesView()
        {
            return PartialView(db.Categories.ToList());
        }

        public PartialViewResult _SearchBook()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult SearchBook(Book b)
        {
            List<Book> list = new List<Book>();
            list = db.Books.Where(book => book.Title == b.Title).ToList();
            return View(list);
        }
        
       
    }
}
