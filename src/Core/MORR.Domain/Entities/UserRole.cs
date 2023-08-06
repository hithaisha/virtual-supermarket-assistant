﻿namespace MORR.Domain.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
