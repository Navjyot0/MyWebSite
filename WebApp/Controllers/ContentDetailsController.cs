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

namespace WebApp.Controllers
{
    public class ContentDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContentDetails
        public async Task<ActionResult> Index()
        {
            return View(await db.ContentDetails.ToListAsync());
        }

        // GET: ContentDetails/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentDetail contentDetail = await db.ContentDetails.FindAsync(id);
            if (contentDetail == null)
            {
                return HttpNotFound();
            }
            return View(contentDetail);
        }

        // GET: ContentDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ContentID,Title,Description,Keywords,Author,PublishDate,Image,URL,CreatedDate,UpdatedDate")] ContentDetail contentDetail)
        {
            if (ModelState.IsValid)
            {
                db.ContentDetails.Add(contentDetail);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(contentDetail);
        }

        // GET: ContentDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentDetail contentDetail = await db.ContentDetails.FindAsync(id);
            if (contentDetail == null)
            {
                return HttpNotFound();
            }
            return View(contentDetail);
        }

        // POST: ContentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ContentID,Title,Description,Keywords,Author,PublishDate,Image,URL,CreatedDate,UpdatedDate")] ContentDetail contentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contentDetail).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contentDetail);
        }

        // GET: ContentDetails/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContentDetail contentDetail = await db.ContentDetails.FindAsync(id);
            if (contentDetail == null)
            {
                return HttpNotFound();
            }
            return View(contentDetail);
        }

        // POST: ContentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContentDetail contentDetail = await db.ContentDetails.FindAsync(id);
            db.ContentDetails.Remove(contentDetail);
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
