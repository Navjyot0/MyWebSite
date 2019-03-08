using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.IO;

namespace WebApp.Controllers
{
    [Authorize]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> Index()
        {
            var news = db.News.Include(n => n.ContentDetail);
            return View(await news.ToListAsync());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        public ActionResult Create()
        {
            ViewBag.ContentID = new SelectList(db.ContentDetails, "ContentID", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.ContentDetail.Upload.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(news.ContentDetail.Upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/News/Images"), fileName);
                    news.ContentDetail.Upload.SaveAs(path);
                    news.ContentDetail.Image = "Uploads/News/Images/" + fileName;
                }
                //ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                news.ContentDetail.CreatedDate = DateTime.Now;
                news.ContentDetail.UpdatedDate = DateTime.Now;
                
                //news.ContentDetail.CreatedBy.Id = user.Id;
                //news.ContentDetail.UpdatedBy.Id = user.Id;
                db.News.Add(news);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ContentID = new SelectList(db.ContentDetails, "ContentID", "Title", news.ContentID);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContentID = news.ContentID;
            return View(news);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(News news)
        {
            if (ModelState.IsValid)
            {
                if (news.ContentDetail.Upload!=null && news.ContentDetail.Upload.ContentLength > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(news.ContentDetail.Upload.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads/News/Images"), fileName);
                    news.ContentDetail.Upload.SaveAs(path);
                    news.ContentDetail.Image = "Uploads/News/Images/" + fileName;
                }
                ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                news.ContentDetail.UpdatedDate = DateTime.Now;
                //news.ContentDetail.UpdatedBy = user;
                db.Entry(news).State = EntityState.Modified;
                db.Entry(news.ContentDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            News news = await db.News.FindAsync(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthFilter(Roles = "AppAdmin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            News news = await db.News.FindAsync(id);
            db.News.Remove(news);
            await db.SaveChangesAsync();
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
