using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using MVC_SHOP.Common;
using MVC_SHOP.Models;
using PagedList;

namespace MVC_SHOP.Controllers
{
    public class FeedBacksController : Controller
    {
        private LearnContext db = new LearnContext();
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name" : "";
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

            var feedback = from s in db.FeedBacks
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                feedback = feedback.Where(s => s.name.Contains(searchString)
                                         || s.Email.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    feedback = feedback.OrderByDescending(s => s.name);
                    break;
                case "CreatedDate_desc":
                    feedback = feedback.OrderBy(s => s.Creared);
                    break;
                case "CreatedDate":
                    feedback = feedback.OrderByDescending(s => s.Creared);
                    break;
                default:  // Name ascending 
                    feedback = feedback.OrderBy(s => s.name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(feedback.ToPagedList(pageNumber, pageSize));
        }
        public void ExportData()
        {
            
           var lstFeedBack = db.FeedBacks.ToList();

            WebGrid webGrid = new WebGrid(lstFeedBack, canPage: true, rowsPerPage: 10);
            string gridData = webGrid.GetHtml(
                columns: webGrid.Columns(
                webGrid.Column(columnName: "name", header: "Name"),
                webGrid.Column(columnName: "Email", header: "Email"),
                webGrid.Column(columnName: "Content", header: "Content"),
                webGrid.Column(columnName: "PhoneNumber", header: "PhoneNumber"),
                webGrid.Column(columnName: "Creared", header: "Creared")
            )).ToString();

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=FeedBack-Details.xls");
            Response.ContentType = "applicatiom/excel";
            Response.Write(gridData);
            Response.End();
        }
        // GET: FeedBacks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Default");
            }
            FeedBacks feedBacks = db.FeedBacks.Find(id);
            if (feedBacks == null)
            {
                return HttpNotFound();
            }
            return View(feedBacks);
        }

        // GET: FeedBacks/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FeedBacks feedBacks)
        {
            feedBacks.ModifieddBy = Notice.SystemAddmin;
            feedBacks.Creared = DateTime.Now;
            feedBacks.Modified = DateTime.Now;
            feedBacks.CreatedBy = Notice.SystemAddmin;
            if (ModelState.IsValid)
            {
                db.FeedBacks.Add(feedBacks);
                db.SaveChanges();
                return RedirectToAction(Notice.Index);
            }

            return View(feedBacks);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNew(FeedBacks feedBacks)
        {
            feedBacks.ModifieddBy = Notice.SystemAddmin;
            feedBacks.Creared = DateTime.Now;
            feedBacks.Modified = DateTime.Now;
            feedBacks.CreatedBy = Notice.SystemAddmin;
            string content = "You have notification from Bright Day center \n" +
                             "Member " + feedBacks.name + " has mail is: \n" + feedBacks.Email
                             +" Response content: \n" + feedBacks.Content + "\n at " + feedBacks.Creared;
            if (ModelState.IsValid)
            {
                db.FeedBacks.Add(feedBacks);
                db.SaveChanges();
                var sendMail = new SendMail.EmailService();
                sendMail.Send(Notice.MailXuan, Notice.Subject1, content);
                return RedirectToAction(Notice.Index, Notice.Home);
            }
            return  RedirectToAction(Notice.Index, Notice.Home);
        }

        // GET: FeedBacks/Edit/5
        public ActionResult Edit(int? id)
        { 
                if (id == null)
                {
                    return RedirectToAction("Error", "Default");
                }
                FeedBacks feedBacks = db.FeedBacks.Find(id);
                if (feedBacks == null)
                {
                    return HttpNotFound();
                }
                return View(feedBacks);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( FeedBacks feedBacks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedBacks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(Notice.Index);
            }
            return View(feedBacks);
        }

        // GET: FeedBacks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Default");
            }
            FeedBacks feedBacks = db.FeedBacks.Find(id);
            if (feedBacks == null)
            {
                return HttpNotFound();
            }
            return View(feedBacks);
        }

        // POST: FeedBacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FeedBacks feedBacks = db.FeedBacks.Find(id);
            if (feedBacks != null) db.FeedBacks.Remove(feedBacks);
            db.SaveChanges();
            return RedirectToAction(Notice.Index);
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
