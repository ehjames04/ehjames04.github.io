using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ElizabethJames_CodeExample.DAL;
using ElizabethJames_CodeExample.Models;

namespace ElizabethJames_CodeExample.Controllers
{
    public class ComputerGamesController : Controller
    {
        private ComputerGamesContext db = new ComputerGamesContext();

        // GET: ComputerGames
        public ActionResult Index()
        {
            return View(db.ComputerGames.ToList());
        }

        public JsonResult IndexJson()
        {
            return Json(db.ComputerGames.ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: ComputerGames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerGame computerGame = db.ComputerGames.Find(id);
            if (computerGame == null)
            {
                return HttpNotFound();
            }
            return View(computerGame);
        }

        // GET: ComputerGames/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ComputerGames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ReleaseDate,Rating")] ComputerGame computerGame)
        {
            if (ModelState.IsValid)
            {
                db.ComputerGames.Add(computerGame);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(computerGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateJson([Bind(Include = "Id,Name,Description,ReleaseDate,Rating")] ComputerGame computerGame)
        {
            if (ModelState.IsValid)
            {
                db.ComputerGames.Add(computerGame);
                db.SaveChanges();
                return Json(computerGame.Id);
            }

            return Json(0);
        }

        // GET: ComputerGames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerGame computerGame = db.ComputerGames.Find(id);
            if (computerGame == null)
            {
                return HttpNotFound();
            }
            return View(computerGame);
        }

        // POST: ComputerGames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ReleaseDate,Rating")] ComputerGame computerGame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computerGame).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(computerGame);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditJson([Bind(Include = "Id,Name,Description,ReleaseDate,Rating")] ComputerGame computerGame)
        {
            if (ModelState.IsValid)
            {
                db.Entry(computerGame).State = EntityState.Modified;
                db.SaveChanges();
                return Json(computerGame.Id);
            }

            return Json(false);
        }

        // GET: ComputerGames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComputerGame computerGame = db.ComputerGames.Find(id);
            if (computerGame == null)
            {
                return HttpNotFound();
            }
            return View(computerGame);
        }

        // POST: ComputerGames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComputerGame computerGame = db.ComputerGames.Find(id);
            db.ComputerGames.Remove(computerGame);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteJson")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteJson(int id)
        {
            ComputerGame computerGame = db.ComputerGames.Find(id);
            db.ComputerGames.Remove(computerGame);
            db.SaveChanges();
            return Json(computerGame);
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
