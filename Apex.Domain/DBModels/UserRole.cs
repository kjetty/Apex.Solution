namespace Apex.Domain.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    public partial class UserRole
    {
        public int UserRoleId { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public bool ActiveFlag { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
