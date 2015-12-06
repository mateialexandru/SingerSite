using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SingerSite.Models;

namespace SingerSite.Controllers
{
    public class NewsPostController : Controller
    {
        private NewsPostDBContext _db = new NewsPostDBContext();

        private UpdateCollection _updateCollection = new UpdateCollection(new SongDBContext());

        // GET: /NewsPost/
        public ActionResult Index()
        {
            NewsIndexViewModel vm = new NewsIndexViewModel();
            vm.UpdateModels = _updateCollection.getLatestModels();
            vm.NewsPostModels = _db.NewsPosts.ToList();
            return View(vm);
        }

        // GET: /NewsPost/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPostModel newspostmodel = _db.NewsPosts.Find(id);
            if (newspostmodel == null)
            {
                return HttpNotFound();
            }
            return View(newspostmodel);
        }

        // GET: /NewsPost/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /NewsPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include="ID,Content")] NewsPostModel newspostmodel)
        {
            if (ModelState.IsValid)
            {
                newspostmodel.PostDate = DateTime.Now;
                _db.NewsPosts.Add(newspostmodel);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newspostmodel);
        }

        // GET: /NewsPost/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPostModel newspostmodel = _db.NewsPosts.Find(id);
            if (newspostmodel == null)
            {
                return HttpNotFound();
            }
            return View(newspostmodel);
        }

        // POST: /NewsPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include="ID,PostDate,Content")] NewsPostModel newspostmodel)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(newspostmodel).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newspostmodel);
        }

        // GET: /NewsPost/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsPostModel newspostmodel = _db.NewsPosts.Find(id);
            if (newspostmodel == null)
            {
                return HttpNotFound();
            }
            return View(newspostmodel);
        }

        // POST: /NewsPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsPostModel newspostmodel = _db.NewsPosts.Find(id);
            _db.NewsPosts.Remove(newspostmodel);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
