using FOF.Domain.Common;

namespace MORR.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
   
}
