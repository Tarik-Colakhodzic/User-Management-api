using Application.Common.Models;
using System.Collections.Generic;

namespace Application.Models
{
    public class UserModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<UserPermissionsModel> UserPermissions { get; set; }
    }
}