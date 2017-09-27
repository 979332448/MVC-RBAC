using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC___RBAC.Models;

namespace MVC___RBAC.Controllers
{
    public class ModuleController : Controller
    {
        Model1 db = new Model1();
        // GET: Module
        public ActionResult Index()
        {
            return View(db.Modules);
        }

        public ActionResult Creadte()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var moduel = db.Modules.FirstOrDefault(m => m.Id == id);
            if (moduel == null) return Content("未找到");
            return View(moduel);
        }

        public ActionResult Add(Module module)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //添加或更新实体
            db.Modules.AddOrUpdate(module);
            db.SaveChanges();
            return Json(new { code = 200 });
        }

        public ActionResult Delete(int id)
        {
            //实例化一个Module把要删除的ID初始化
            Module model = new Module();
            model.Id = id;
            //将要删除的实体，附加到数据上下文
            db.Modules.Attach(model);
            //删除实体
            db.Modules.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");
           
        }
    }

}