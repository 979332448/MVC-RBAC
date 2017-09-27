using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using MVC___RBAC.Models;
using System.Web.Mvc;

namespace MVC___RBAC.Controllers
{
    public class UserController : Controller
    {
        Model1 db = new Model1();
        // GET: User
        public ActionResult Index()
        {
            return View(db.Users);
        }

        public ActionResult Creadte()
        {
            return View();
        }

        public ActionResult Add(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //添加或更新尸体
            db.Users.AddOrUpdate(user);
            db.SaveChanges();
            return Json(new { code = 200 });
        }

        public ActionResult Delete(int id)
        {
            //实例化一个Module把要删除的ID初始化
            User user = new User();
            user.Id = id;
            //将要删除的实体，附加到数据上下文
            db.Users.Attach(user);
            //删除实体
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var user = db.Users.FirstOrDefault(m => m.Id == id);
            if (user == null) return Content("未找到");
            return View(user);
        }

    }
}