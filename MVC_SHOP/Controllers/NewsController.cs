using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_SHOP.Common;
using MVC_SHOP.Models;
using PagedList;

namespace MVC_SHOP.Controllers
{
    public class NewsController : Controller
    {
        private LearnContext db = new LearnContext();
        public ActionResult _PartialNews()
        {
            var news = db.News.Where(c => c.Catrgories.Title == "Education").Take(4);
            return View(news);
        }
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewBag.DateSortParm = sortOrder == "Created" ? "CreatedDate_desc" : "CreatedDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var news = from s in db.News
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                news = news.Where(s => s.Title.Contains(searchString)
                                         || s.Content.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    news = news.OrderByDescending(s => s.Title);
                    break;
                case "CreatedDate_desc":
                    news = news.OrderBy(s => s.Creared);
                    break;
                case "CreatedDate":
                    news = news.OrderByDescending(s => s.Creared);
                    break;
                default:  // Name ascending 
                    news = news.OrderBy(s => s.Title);
                    break;
            }
            
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(news.ToPagedList(pageNumber, pageSize));
        }
        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Default");
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create( News news, HttpPostedFileBase image)
        {
            news.ModifieddBy = Notice.SystemAddmin;
            news.Creared=DateTime.Now;
            news.CreatedBy = Notice.SystemAddmin;
            news.Modified=DateTime.Now;
            news.Image = Path.GetFileName(image.FileName);
            if (ModelState.IsValid)
            {
                db.News.Add(news);
                db.SaveChanges();
                image.SaveAs(Server.MapPath(Url.Content("~/NewsImage/" + news.Id.ToString() + news.Image)));
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Default");
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News news, HttpPostedFileBase image)
        {
            news.Modified = DateTime.Now;
            news.ModifieddBy = Notice.SystemAddmin;
            if (ModelState.IsValid)
            {

                    string fullPath = "~/NewsImage/" + news.Id.ToString() + news.Image;
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                if (image != null && image.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    news.Image = fileName;
                    if (fileName != null)
                    {
                        image.SaveAs(Server.MapPath(Url.Content("~/NewsImage/" + news.Id.ToString() + news.Image)));
                    }
                }
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Default");
            }
            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            if (news != null) db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
