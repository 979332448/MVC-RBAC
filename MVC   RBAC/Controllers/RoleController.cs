using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Migrations;
using MVC___RBAC.Models;
namespace MVC___RBAC.Controllers
{
    public class RoleController : Controller
    {
        Model1 db = new Model1();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles);
        }

        public ActionResult Creadte()
        {
            return View();
        }

        public ActionResult Add(Role role)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //添加或更新实体
            db.Roles.AddOrUpdate(role);
            db.SaveChanges();
            return Json(new { code = 200 });
        }

        public ActionResult Delete(int id)
        {
            //实例化一个Role把要删除的ID初始化
            Role role = new Role();
            role.Id = id;
            //将要删除的实体，附加到数据上下文
            db.Roles.Attach(role);
            //删除实体
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Edit(int id)
        {
            var roles = db.Roles.FirstOrDefault(m => m.Id == id);
            if (roles == null) return Content("未找到");
            return View(roles);
        }

    }
}