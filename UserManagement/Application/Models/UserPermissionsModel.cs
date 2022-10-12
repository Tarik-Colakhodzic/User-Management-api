namespace Application.Models
{
    public class UserPermissionsModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int PermissionId { get; set; }
        public virtual PermissionModel Permission { get; set; }
    }
}