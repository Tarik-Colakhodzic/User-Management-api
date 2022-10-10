using Application.Common.Models;

namespace Application.Models.Requests.User
{
    public class UserSearchRequest : BaseSearchModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
    }
}