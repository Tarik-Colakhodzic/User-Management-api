using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Requests.UserPermissions
{
    public class UserPermissionsInsertRequest
    {
        public int UserId { get; set; }
        public int PermissionId { get; set; }
    }
}
