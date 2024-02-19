using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CDNCRUD.Models;

namespace CDNCRUD.Controllers
{
    public class UsersController : Controller
    {
        private CDN_CRUD_OperarationEntities2 db = new CDN_CRUD_OperarationEntities2();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        // GET: Users/Create
        public ActionResult Create()
        {
            // Retrieve the list of skill sets from the database
            var skillSetList = GetSkillSetListFromDatabase();

            // Retrieve the list of Hobby from the database
            var hobbyList = GethobbyListFromDatabase();

            // Pass the skill set list to the view
            ViewBag.SkillSetList = skillSetList;
            // Pass the skill set list to the view
            ViewBag.HobbyList = hobbyList;
            return View();
        }

        // Method to get the list of skill sets from the database
        private List<SelectListItem> GetSkillSetListFromDatabase()
        {
            // Here you would typically query your database to retrieve the skill sets
            // For demonstration purposes, I'm creating a sample list here
            var skillSetList = new List<SelectListItem>
        {
        new SelectListItem { Value = "Java", Text = "Java" },
        new SelectListItem { Value = "Python", Text = "Python" },
        new SelectListItem { Value = "C#", Text = "C#" },
        new SelectListItem { Value = "JavaScript", Text = "JavaScript" },
        new SelectListItem { Value = "SQL", Text = "SQL" },
        new SelectListItem { Value = "Ruby", Text = "Ruby" },
        new SelectListItem { Value = "Swift", Text = "Swift" },
        new SelectListItem { Value = "PHP", Text = "PHP" },
        new SelectListItem { Value = "C++", Text = "C++" },
        new SelectListItem { Value = "AngularJS", Text = "AngularJS" }
       // Add more items as needed
        };

            return skillSetList;
        }
        // Method to get the list of hobby from the database
        private List<SelectListItem> GethobbyListFromDatabase()
        {
            // Here you would typically query your database to retrieve the hobby
            // For demonstration purposes, I'm creating a sample list here
            var hobbyList = new List<SelectListItem>
        {
       new SelectListItem { Value = "Reading", Text = "Reading" },
       new SelectListItem { Value = "Cooking", Text = "Cooking" },
       new SelectListItem { Value = "Gardening", Text = "Gardening" },
       new SelectListItem { Value = "Playing Sports", Text = "Playing Sports" },
       new SelectListItem { Value = "Traveling", Text = "Traveling" },
       new SelectListItem { Value = "Painting", Text = "Painting" },
       new SelectListItem { Value = "Photography", Text = "Photography" },
       new SelectListItem { Value = "Writing", Text = "Writing" },
       new SelectListItem { Value = "Drawing", Text = "Drawing" },
       // Add more items as needed
        };

            return hobbyList;
        }
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user, string[] SkillSets, string[] Hobby)
        {
            // Populate the SkillSets property with the selected skill sets
            user.SkillSets = string.Join(",", SkillSets);

            // Populate the Hobby property with the selected hobby
            user.Hobby = string.Join(",", Hobby);

            if (ModelState.IsValid)
            {
                // Explicitly exclude the Id property from the insert operation
                var newUser = new User
                {
                  
                    Username = user.Username,
                    Mail = user.Mail,
                    PhoneNumber = user.PhoneNumber,
                    SkillSets = user.SkillSets,
                    Hobby = user.Hobby
                };
               
                db.Users.Add(newUser);
                db.SaveChanges();
                var ID = newUser.ID;
                return RedirectToAction("Index");
            }
            // If the model state is not valid, return the view with the user model
            // so that the selected skill sets are retained in the form
            ViewBag.SelectedSkills = SkillSets; // Pass selected skills back to the view
            ViewBag.SelectedHobby = Hobby; // Pass selected skills back to the view
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            // Retrieve the list of skill sets from the database
            var skillSetList = GetSkillSetListFromDatabase();

            // Retrieve the list of hobby from the database
            var hobbyList = GethobbyListFromDatabase();

            // Split the user's skill sets into an array
            var selectedSkills = user.SkillSets != null ? user.SkillSets.Split(',') : new string[0];

            // Split the user's hobby into an array
            var selectedHobby = user.Hobby != null ? user.Hobby.Split(',') : new string[0];

            // Pass the skill set list and selected skills to the view
            ViewBag.SkillSetList = skillSetList;
            ViewBag.SelectedSkills = selectedSkills;

            // Pass the hobby list and selected skills to the view
            ViewBag.HobbyList = hobbyList;
            ViewBag.SelectedHobby = selectedHobby;

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind(Include = "ID,Username,Mail,PhoneNumber")] User user, string[] SkillSets, string[] Hobby)
        {
            if (id != user.ID)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                // Combine the selected skill sets into a comma-separated string
                user.SkillSets = SkillSets != null ? string.Join(",", SkillSets) : null;

                // Combine the selected hobby into a comma-separated string
                user.Hobby = Hobby != null ? string.Join(",", Hobby) : null;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // If model state is not valid, re-populate the ViewBag and return to the view
            var skillSetList = GetSkillSetListFromDatabase();
            ViewBag.SkillSetList = skillSetList;
            ViewBag.SelectedSkills = SkillSets ?? new string[0];

            // If model state is not valid, re-populate the ViewBag and return to the view
            var hobbyList = GethobbyListFromDatabase();
            ViewBag.HobbyList = hobbyList;
            ViewBag.SelectedHobby = Hobby ?? new string[0];

            //// Retrieve the list of hobby from the database
            //var hobbyList = GethobbyListFromDatabase();
            //ViewBag.HobbyList = hobbyList;

            //// Ensure that selected hobby values are properly assigned
            //ViewBag.SelectedHobby = Hobby ?? user.Hobby?.Split(',') ?? new string[0];

            return View(user);
        }


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
