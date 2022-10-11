using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests.User
{
    public class UserUpdateRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public bool Status { get; set; }
    }
}