using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC___RBAC.Models;

namespace MVC___RBAC.Filters
{
    /// <summary>
    /// 授权过滤器认证类型枚举
    /// None 无限制不认证
    /// Identity 仅身份验证
    /// AuthorIze  授权验证
    /// </summary>
    public enum AuthorType {None,Identity,AuthorIze }
    public class AutharArrtibute : FilterAttribute, IAuthorizationFilter
    {
        //默认授权认证
        public AuthorType Type = AuthorType.AuthorIze;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //1.无限制
            if (Type == AuthorType.None) return;

            //2.身份认证
            if (filterContext.HttpContext.Session["er"] == null)
            {
                RedirectToLoin(filterContext);
                return;
            }

            //如果请求的控制器为Identity则直接返回
            if (Type == AuthorType.Identity) return;

            //授权认证
            //获取当前请求控制器
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //获取当前用户登陆角色
            var role = filterContext.HttpContext.Session["role"] as Role;
            //判断用户角色是否为空
            if (role == null)
            {
                RedirectToLoin(filterContext);
                return;
            }
            //查找角色模块里的控制器是否存在请求的控制器
            var modle = role.Modules.FirstOrDefault(m => m.Controller == controller);
            //不存在返回登陆页面
            if (modle == null)
            {
                RedirectToLoin(filterContext);
            }
        }

        //返回登陆方法
        public void RedirectToLoin(AuthorizationContext filterContext)
        {
            //实例化一个UrlHelper对象
            var url = new UrlHelper(filterContext.RequestContext);
            //设置返回结果为重定向到登陆页
            filterContext.Result = new RedirectResult(url.Action("Index", "Login"));
        }
    }
}