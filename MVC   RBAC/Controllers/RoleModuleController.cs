using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVC___RBAC.Models;
using MVC___RBAC.VeirMdels;

namespace MVC___RBAC.Controllers
{
    public class RoleModuleController : Controller
    {
        Model1 db = new Model1();
        // GET: RoleModule
        public ActionResult Index()
        {
            //查询每一个角色对应的模块
            var result = db.Roles.Include(r => r.Modules);
            return View(result);
        }

        public ActionResult Creadte()
        {
            //所有角色下拉列表
            ViewBag.RoleOptiuns = from r in db.Roles
                                  select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };

            //获取所有模块的下拉列表
            ViewBag.ModulOptiuns = from m in db.Modules
                                  select new SelectListItem { Text = m.Name, Value = m.Id.ToString() };
            return View();
        }

        public ActionResult Edit(RoleModels roleModule)
        {
            //所有模块的下拉列表
            ViewBag.ModuleOptiuns = from m in db.Modules
                                  select new SelectListItem { Text = m.Name, Value = m.Id.ToString() };
            //角色名称
            roleModule.RoleName = db.Roles.FirstOrDefault(r => r.Id == roleModule.RoleId).Name;
            //模块名称
            roleModule.ModuleName = db.Modules.FirstOrDefault(m => m.Id == roleModule.ModuleId).Name;
            return View(roleModule);
        }

        public ActionResult Delete(RoleModels roleModule)
        {
            //先把要删除的权限角色找出来
            var role = db.Roles.FirstOrDefault(r => r.Id == roleModule.RoleId);
            //查找删除的权限模块找出来
            var model = db.Modules.FirstOrDefault(m => m.Id == roleModule.ModuleId);
            //把这个要删除的权限模块，从角色的模块集合中移除
            role.Modules.Remove(model);
            //判断是否保存到数据库中
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }

        public ActionResult Insert(RoleModels roleModule)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //先把要添加的权限角色找出来
            var role = db.Roles.FirstOrDefault(r => r.Id == roleModule.RoleId);
            //查找添加的权限模块找出来
            var model = db.Modules.FirstOrDefault(m => m.Id == roleModule.ModuleId);
            //构建一个要添加的权限模块
            //var modul = new Module { Id = roleModule.ModuleId };
            //伪装成从数据库查出来的
            //db.Modules.Attach(modul);


            //把这个要添加的权限模块添加到角色模块集合中
            role.Modules.Add(model);
            //判断是否保存到数据库中
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult Update(RoleModels roleModule)
        {
            //先把要修改的权限角色找出来
            var role = db.Roles.FirstOrDefault(r => r.Id == roleModule.RoleId);

            //构建一个要添加的权限模块
            var model = new Module { Id = roleModule.ModuleId };
            //构建一个要修改的权限模块
            var UpdateModule = new Module { Id = roleModule.UpdateModuleId };

            //伪装成从数据库查出来的
            db.Modules.Attach(model);
            db.Modules.Attach(UpdateModule);

            //把要修改的角色模块删掉
            role.Modules.Remove(model);
            //把要更新的model添加到role的Modules
            role.Modules.Add(UpdateModule);
            //判断是否保存到数据库中
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
    }
}