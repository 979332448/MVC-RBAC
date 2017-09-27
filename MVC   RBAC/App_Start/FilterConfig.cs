using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC___RBAC.Filters;

namespace MVC___RBAC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFIlter(GlobalFilterCollection filtere)
        {
            filtere.Add(new AutharArrtibute() );
        }
    }
}