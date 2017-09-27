using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC___RBAC.Models;
using MVC___RBAC.Filters;
namespace MVC___RBAC.Controllers
{
    [AutharArrtibute(Type = AuthorType.None)]
    public class LoginController : Controller
    {
        Model1 db = new Model1();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(User user)
        {
            //模型绑定验证
            if (!ModelState.IsValid)
            {
                return Json(new { cod = 400 });
            }
             //查找用户
            var er = db.Users.FirstOrDefault(u => u.Username==user.Username && u.Password == user.Password);
            //如果没找到，就返回404
            if (er == null) return Json(new { cod = 404 });

            //设置session作为身份验证的标识
            Session["er"] = er;

            //查找所有角色的所有模块,转换成字典类型,key是角色Id，value是角色所有的模块
            var roleModules = er.Roles.ToDictionary(r=>r.Id,r => r.Modules);
            //存入Session
            Session["roleModules"] = roleModules;

            //查找用户的所有角色
            var roles = er.Roles.ToList();
            Session["roles"] = roles;

            //设置默认角色为角色表里的第一个
            Session["role"] = roles[0];

            return Json(new { cod = 200 });
        }
    }
}