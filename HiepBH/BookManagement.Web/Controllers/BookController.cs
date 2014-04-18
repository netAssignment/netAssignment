using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookManagement.Core.Models;
using BookManagement.Core.Infrastructure;
using System.IO;

namespace BookManagement.Web.Controllers
{
    public class BookController : Controller
    {
        private BookManagementDb db = new BookManagementDb();

        //
        // GET: /Book/

        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        //
        // GET: /Book/Details/5

        public ActionResult Details(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // GET: /Book/Create

        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }

        //
        // POST: /Book/Create

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Book book, HttpPostedFileBase PathImage)
        {
            if(ModelState.IsValid)
            {
                if ((PathImage != null && PathImage.ContentLength > 0))
                {
                    var fileName = Path.GetFileName(PathImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                    PathImage.SaveAs(path);

                    book.PathImage = "Images/" + fileName;
                }

                book.Category = db.Categories.Where(c => c.Name == book.Category.Name).ToList()[0];
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
      
        //
        // GET: /Book/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        //
        // GET: /Book/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //
        // POST: /Book/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}