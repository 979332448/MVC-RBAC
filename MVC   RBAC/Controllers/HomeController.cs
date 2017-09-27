using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC___RBAC.Filters;
using MVC___RBAC.Models;

namespace MVC___RBAC.Controllers
{
    [AutharArrtibute(Type=AuthorType.Identity)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Header()
        {
            //获取登录用户
            var user = Session["er"] as User;
            //获取用户角色 转为List类型
            var roles = Session["roles"] as List<Role>;

            var role = Session["role"] as Role;
            //下拉框的选择列表
            List<SelectListItem> rolelist = new List<SelectListItem>();
            //便利roles用户角色Text显示值，Value实际值
            foreach (var item in roles)
            {
                rolelist.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = item.Id == role.Id });
            }
            ViewBag.roles = rolelist;
            return PartialView(user);
        }
        /// <summary>
        /// 负责查询所选角色的所有模块
        /// 把所有角色渲染出来
        /// </summary>
        /// <returns></returns>
        public ActionResult Nav(int roleid=0)
        {
            //获取用户角色的模块
            var roleModules = Session["roleModules"] as Dictionary<int, ICollection<Module>>;
            //获取默认角色
            var role = Session["role"] as Role;
            //根据参数roleid，获取某一个用户角色的模块
            var roles = Session["roles"] as List<Role>;
            List<Module> modules;
            if (roleModules.ContainsKey(roleid))
            {
                modules = roleModules[roleid].ToList();
                //切换当前角色
                Session["role"] = roles.FirstOrDefault(r => r.Id == roleid);
            }
            else
            {
                modules = roleModules[role.Id].ToList();
            }

            //var modules = roleModules[roleid];
            ////如果模块表为Null，说明传递的roleid有误
            //if (modules == null)
            //{
            //    //使用Session里的id
            //    modules = roleModules[role.Id];
            //}
            return PartialView(modules);
        }

        //注销
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}