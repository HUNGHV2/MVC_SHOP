using System.Linq;
using System.Web.Mvc;
using MVC_SHOP.Models;
namespace MVC_SHOP.Controllers
{
    public class NewsViewController : Controller
    {
        // GET: NewsView
        private LearnContext db = new LearnContext();
        public ActionResult Index()
        {
            var news = db.News.Where(c => c.Catrgories.Title == "Education").Take(4);
            return View(news);
        }
    }
}