using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GymTrainingDiary.Data;
using GymTrainingDiary.Data.Model;
using GymTrainingDiary.Web.Models;
using GymTrainigDiary.StaticData;
using System.Data.Entity.Migrations;

namespace GymTrainingDiary.Web.Controllers
{
    [System.Runtime.InteropServices.Guid("32660B14-9828-415F-8FEF-4B2CC03CC60C")]
    public class UsersController : Controller
    {
        //private GymTrainingDiaryContext db = new GymTrainingDiaryContext();

        // GET: Users
        public ActionResult Index()
        {
            var users = DataAccessMethods.GetUsers();
            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = DataAccessMethods.GetUserById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            var model = new CreateUserModel();
            this.PopulateSelectLists(model);

            return View(model);
        }


        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,AccountTypeId,FirstName,SecondName,DisplayName,Login,PasswordHash,LastLoginDate")] User user)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Users.Add(user);
        //        db.SaveChanges();
        //        return RedirectToAction("Details", new { user.Id });
        //    }

        //    ViewBag.AccountTypeId = new SelectList(db.CommonTypes.Where(x => x.Domain == AccountTypes.Domain), "Id", "Domain", user.AccountTypeId);
        //    return View(user);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserModel model)
        {
            ActionResult result = null;

            var user = model.NewUser;

            if (ModelState.IsValid)
            {
                DataAccessMethods.AddOrUpdateUser(model.NewUser);
                result = RedirectToAction("Details", new { user.Id });
            }
            else
            {
                ////перезаповнення списків, оскільки він буде NULL при POST
                this.PopulateSelectLists(model);
                result = View(model);
            }

            return result;
        }

        private void PopulateSelectLists(CreateUserModel model)
        {
            model.UserAccountTypes = new SelectList(DataAccessMethods.GetAccountTypes(), "Id", "Name");
        }



        // GET: Users/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new CreateUserModel();
            model.NewUser = DataAccessMethods.GetUserById(id);
            this.PopulateSelectLists(model);


            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateUserModel model)
        {
            ActionResult result = null;

            var user = model.NewUser;

            if (ModelState.IsValid)
            {
                DataAccessMethods.AddOrUpdateUser(model.NewUser);
                result = RedirectToAction("Index");
            }
            else
            {
                ////перезаповнення списків, оскільки він буде NULL при POST
                this.PopulateSelectLists(model);
                result = View(model);
            }

            return result;
        }

        // GET: Users/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = DataAccessMethods.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            DataAccessMethods.DeleteUser(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
