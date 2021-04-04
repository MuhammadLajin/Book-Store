using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Day4BookStore.Models;

namespace Day4BookStore.Controllers
{
    public class userController : Controller
    {
        // GET: user
        Model1 db = new Model1();
        public ActionResult Index()
        {

            if (Session["userid"] != null)
                return View(db.Catalogs.ToList());
            else
                return RedirectToAction("login");
        }
        public ActionResult BookView()
        {
            ViewBag.auth = db.Authors.ToList();
            ViewBag.cata = db.Catalogs.ToList();
            return View(db.Books.ToList());
        }
        public ActionResult AuthorView()
        {
            ViewBag.books = db.Books.ToList();
            return View(db.Authors.ToList());
        }

        public ActionResult User()
        {
            ViewBag.books = db.Books.ToList();
            return View(db.Users.ToList());
        }
        public ActionResult CreateUser()
        {
            ViewBag.books = db.Books.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(User s)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(s);
                db.SaveChanges();
                return RedirectToAction("login");
            }
            else
            {
                
                return View();
            }

        }

        public ActionResult edit(int id)
        {
            User s = db.Users.Where(n => n.user_id == id).SingleOrDefault();

            //ViewBag.books = new SelectList(db.Books.ToList().Where(n => n.user_id == id) );

            return View(s);

        }
        [HttpPost]
        public ActionResult edit(User u)
        {
            //db.Entry(u).State = System.Data.Entity.EntityState.Modified;

            //us is return null here ??????????????????????????????????????????????????????
            User us = db.Users.Where(n => n.user_id == u.user_id).SingleOrDefault();
            us.user_name = u.user_name;
            us.user_age = u.user_age;
            us.user_email = u.user_email;
            us.confirm_password = us.user_password;


            db.SaveChanges();


            return RedirectToAction("User");
        }

        public ActionResult login()
        {
            if (Request.Cookies["fullstack"] != null)
            {
                Session["userid"] = Request.Cookies["fullstack"].Values["id"];
                return RedirectToAction("profile");
            }
            return View();
        }

        [HttpPost]
        public ActionResult login(User u, string remeberme)
        {
            User us = db.Users.Where(n => n.user_email == u.user_email && n.user_password == u.user_password).FirstOrDefault();
            if (us != null)
            {
                //login
                Session.Add("userid", us.user_id);
                if (remeberme == "true")
                {
                    HttpCookie co = new HttpCookie("fullstack");
                    co.Values.Add("id", us.user_id.ToString());
                    co.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(co);


                }

                return RedirectToAction("profile");
            }
            else
            {
                //notlogin
                ViewBag.status = "invalid email or password";

                return View();
            }
        }


        public ActionResult profile()
        {
            int id = int.Parse(Session["userid"].ToString());
            User u = db.Users.Where(n => n.user_id == id).FirstOrDefault();


            return View(u);
        }

        public ActionResult logout()
        {
            Session["userid"] = null;
            HttpCookie c = new HttpCookie("fullstack");
            c.Expires = DateTime.Now.AddDays(-15);
            Response.Cookies.Add(c);

            return RedirectToAction("login");
        }

        public ActionResult changePass()
        {

            return View();
        }

        [HttpPost]
        public ActionResult changePass(User u, string newPass)
        {
            //pass is by reference
            int id  = db.Users.Where(n => n.user_email == u.user_email).SingleOrDefault().user_id;
            User us = db.Users.Where(n => n.user_id == id).SingleOrDefault();
            if (us != null)
            {   
                us.user_password = newPass;
                us.confirm_password = us.user_password;

                db.SaveChanges();


            }
            return RedirectToAction("login");
        }

        
        //part Two >> AJAx
        public ActionResult AjaxPartial()
        {
            ViewBag.authors = new SelectList(db.Authors.ToList(), "auth_id", "auth_name");
            return View();
        }

        public ActionResult bookAuthor(int? id)
        {
            if (id != null)
            {
                List<Book> b = db.Books.Where(n => n.auth_id == id).ToList();
                return PartialView(b);
            }
            else
            {
                return PartialView();
            }
        }
    }
}