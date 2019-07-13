namespace Apex.Domain.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            UserRoles = new HashSet<UserRole>();

            this.ActiveFlag = true;
            this.ModifiedBy = 0;
            this.ModifiedDate = DateTime.Now;
        }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(100)]
        public string Address { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string City { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string Province { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string Zip { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string HomePhone { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string OfficePhone { get; set; }

        [DataMember(EmitDefaultValue = false)]
        [StringLength(50)]
        public string Mobile { get; set; }

        [DataMember]
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string LoginId { get; set; }

        [DataMember]
        [Required]
        [StringLength(25)]
        public string LoginPassword { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool ActiveFlag { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int ModifiedBy { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime ModifiedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
