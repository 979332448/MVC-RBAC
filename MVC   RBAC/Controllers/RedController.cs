using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC___RBAC.Filters;
using MVC___RBAC.Models;

namespace MVC___RBAC.Controllers
{
    [AutharArrtibute(Type = AuthorType.None)]
    public class RedController : Controller
    {
        Model1 db = new Model1();
        // GET: Red
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Reg(User user)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { cod = 400 });
            }

            //获取要增加角色的实体
            var role = db.Roles.FirstOrDefault(r => r.Id == 3);
            //给用户增加角色
            user.Roles.Add(role);
            //把注册用户添加到用户表里
            db.Users.Add(user);
            //叫用户保存到数据库中
            db.SaveChanges();
            return Json(new { cod =200 });
        }

    }
}