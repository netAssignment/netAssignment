using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookManagement.Core.Models;
using BookManagement.Core.Infrastructure;
using System.Web.Security;

namespace BookManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private BookManagementDb db = new BookManagementDb();

        //
        // GET: /Account/

        [Authorize]
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        //
        // GET: /Account/Details/5
        [Authorize]
        public ActionResult Details(string id = null)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        //
        // GET: /Account/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Account/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
        }

        //
        // GET: /Account/Edit/5
        [Authorize]
        public ActionResult Edit(string id = null)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        //
        // POST: /Account/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        //
        // GET: /Account/Delete/5
        [Authorize]
        public ActionResult Delete(string id = null)
        {
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        //
        // POST: /Account/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        // Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account account)
        {
            if (ModelState.IsValid)
            {
                List<Account> l = db.Accounts.Where(a => a.UserName == account.UserName).ToList();

                if (l.Count > 0)
                {
                    if (l[0].Password == account.Password)
                    {
                        FormsAuthentication.SetAuthCookie(account.UserName, true);
                        return RedirectToAction("Index", "Home");

                    }
                }
            }

            ModelState.AddModelError("", "Invalid username or password!");
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}