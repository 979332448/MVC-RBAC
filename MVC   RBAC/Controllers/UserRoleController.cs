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
    public class UserRoleController : Controller
    {
        Model1 db = new Model1();
        // GET: UserRole
        public ActionResult Index()
        {

            //查询每一个用户对应的角色
            var user = db.Users.Include(u =>u.Roles);
            return View(user);
        }

        public ActionResult Creadte()
        {
            //所有角色下拉列表
            ViewBag.UserOptiuns = from u in db.Users
                                  select new SelectListItem { Text = u.Username, Value = u.Id.ToString() };

            //获取所有模块的下拉列表
            ViewBag.RoleOptiuns = from r in db.Roles
                                   select new SelectListItem { Text = r.Name, Value = r.Id.ToString() };
            return View();
        }

        public ActionResult Insert(UserRole roleModule)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //先把要添加的权限用户找出来
            var user = db.Users.FirstOrDefault(r => r.Id == roleModule.UserId);
            //查找添加的权限角色找出来
            var role = db.Roles.FirstOrDefault(m => m.Id == roleModule.RoleId);
            //构建一个要添加的权限模块
            //var modul = new Module { Id = roleModule.ModuleId };
            //伪装成从数据库查出来的
            //db.Modules.Attach(modul);


            //把这个要添加的权限模块添加到角色模块集合中
            //role.Modules.Add(model);
            user.Roles.Add(role);
            //判断是否保存到数据库中
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

        public ActionResult Delete(UserRole roleModule)
        {
            //先把要删除的权限用户找出来
            var user = db.Users.FirstOrDefault(r => r.Id == roleModule.UserId);
            //查找删除的权限模块找出来
            var role = db.Roles.FirstOrDefault(m => m.Id == roleModule.RoleId);
            //把这个要删除的权限模块，从角色的模块集合中移除
            user.Roles.Remove(role);
            //判断是否保存到数据库中
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }

            return Json(new { code = 200 });
        }


        public ActionResult Edit(UserRole roleModule)
        {
            //所有模块的下拉列表
            ViewBag.RoleOptiuns = from m in db.Roles
                                    select new SelectListItem { Text = m.Name, Value = m.Id.ToString() };
            //用户名称
            roleModule.UserName = db.Users.FirstOrDefault(r => r.Id == roleModule.UserId).Username;
            //角色名称
            roleModule.RoleName = db.Roles.FirstOrDefault(m => m.Id == roleModule.RoleId).Name;
            return View(roleModule);
        }

        public ActionResult Update(UserRole roleModule)
        {
            //先把要修改的权限用户找出来
            var user = db.Users.FirstOrDefault(r => r.Id == roleModule.UserId);

            //查找修改的权限角色找出来
            var role = new Role { Id = roleModule.RoleId };
            var UpdateRole = new Role { Id = roleModule.UpdateRoleId };

            //伪装成从数据库查出来的
            db.Roles.Attach(role);
            db.Roles.Attach(UpdateRole);

            //把要修改的用户角色删掉
            user.Roles.Remove(role);
            //把要更新的role添加到user的roles
            user.Roles.Add(UpdateRole);
            //判断是否保存到数据库中
            if (db.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }
    }
}