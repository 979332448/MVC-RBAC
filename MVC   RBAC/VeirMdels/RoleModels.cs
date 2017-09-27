using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC___RBAC.VeirMdels
{
    public class RoleModels
    {
        public int RoleId { get; set; }

        public int ModuleId { get; set; }

        public int UpdateModuleId { get; set; }

        public string RoleName { get; set; }

        public string ModuleName { get; set; }
    }
}