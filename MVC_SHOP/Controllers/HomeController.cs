using System;
using System.Linq;
using System.Web.Mvc;
using MVC_SHOP.Models;

namespace MVC_SHOP.Controllers
{
    public class HomeController : Controller
    {
        private LearnContext db = new LearnContext();
        public ActionResult Index()
        {
            ViewBag.Cat1 = db.News.Where(c => c.Catrgories.Title == "Education").Take(4);
            ViewBag.Cat2 = db.News.Where(c => c.Catrgories.Title == "Public").Take(4);
            ViewBag.Cat3 = db.News.Where(c => c.Catrgories.Title == "Promotion").Take(4);
            ViewBag.Cat4 = db.News.Where(c => c.Catrgories.Title == "Society").Take(4);
            ViewBag.isHome = true;
            return View();
        }
    }
}