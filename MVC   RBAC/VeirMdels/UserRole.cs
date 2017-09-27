using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC___RBAC.VeirMdels
{
    public class UserRole
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int UpdateRoleId { get; set; }

        public string UserName{ get; set; }

        public string RoleName { get; set; }
    }
}