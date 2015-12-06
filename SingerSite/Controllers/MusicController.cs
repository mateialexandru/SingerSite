using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SingerSite.Models;

namespace SingerSite.Controllers
{
    [RequireHttps]
    public class MusicController : Controller
    {
        private SongDBContext db = new SongDBContext();

        // GET: /Music/
        public ActionResult Index()
        {
            var FilePaths = db.FilePaths.ToList();
            return View(db.Songs.ToList());
        }

        // GET: /Music/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongViewModel songviewmodel = db.Songs.Find(id);
            if (songviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(songviewmodel);
        }

        // GET: /Music/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Music/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Title,Link,YouTube,Description")] SongViewModel songviewmodel, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    FilePath oFilePath = new FilePath
                    {
                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(upload.FileName),
                        FileType = FileType.mp4,
                    };
                    songviewmodel.FilePath = oFilePath;
                    string strServerDirectory = Server.MapPath("~/uploads/music");
                    if (!Directory.Exists(strServerDirectory))
                    {
                        Directory.CreateDirectory(strServerDirectory);
                    }
                    upload.SaveAs(Path.Combine(strServerDirectory, oFilePath.FileName)); 
                }

                songviewmodel.PostDate = DateTime.Now;
                db.Songs.Add(songviewmodel);
                if(songviewmodel.FilePath != null) db.FilePaths.Add(songviewmodel.FilePath);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(songviewmodel);
        }

        // GET: /Music/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongViewModel songviewmodel = db.Songs.Find(id);
            if (songviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(songviewmodel);
        }

        // POST: /Music/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "ID,Title,Link,YouTube,Description")] SongViewModel songviewmodel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(songviewmodel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(songviewmodel);
        }

        // GET: /Music/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongViewModel songviewmodel = db.Songs.Find(id);
            if (songviewmodel == null)
            {
                return HttpNotFound();
            }
            return View(songviewmodel);
        }

        // POST: /Music/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            SongViewModel songviewmodel = db.Songs.Find(id);
            db.Songs.Remove(songviewmodel);
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
