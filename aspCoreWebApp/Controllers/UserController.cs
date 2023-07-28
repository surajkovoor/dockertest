using aspCoreWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aspCoreWebApp.Repository;


namespace aspCoreWebApp.Controllers
{
    public class UserController : Controller
    {
        Repository.StudentDataAccessLayer studentDataAccessLayer = null;
        public UserController()
        {
            studentDataAccessLayer = new Repository.StudentDataAccessLayer();
        }
        // GET: UserController
        public ActionResult Index()
        {
            Repository.StudentDataAccessLayer lstUser = new Repository.StudentDataAccessLayer();
            return View(lstUser.GetAllUser());
        }

        // GET: UserController/Details/5
        public ActionResult Details(int PersonId)
        {
            User usr = studentDataAccessLayer.GetUserData(PersonId);
            return View(usr);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User usr)
        {
          
            try
            {
                studentDataAccessLayer.AddUser(usr);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int? PersonId)
        {
            User usr = studentDataAccessLayer.GetUserData(PersonId);
            return View(usr);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User usr)
        {
            try
            {
                studentDataAccessLayer.UpdateUser(usr);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        //public ActionResult Delete(int PersonId)
        //{
        //    User usr = studentDataAccessLayer.GetUserData(PersonId);
        //    return View(usr);
        //}

        // POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int PersonId)
        {
            try
            {
                studentDataAccessLayer.DeleteUser(PersonId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}
