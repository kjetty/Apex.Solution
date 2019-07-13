using Apex.Domain.DBModels;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Apex.DAL
{
	public partial class ApexDBContext : DbContext
    {
        public ApexDBContext()
            : base("name=ApexDBContext")
        {
			//disable initializer :: will not create the database
			Database.SetInitializer<ApexDBContext>(null);
			Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			throw new UnintentionalCodeFirstException();
			//blog.oneunicorn.com/2012/02/26/dont-use-code-first-by-mistake/


			//		  modelBuilder.Entity<Role>()
			//             .HasMany(e => e.UserRoles)
			//             .WithRequired(e => e.Role)
			//             .WillCascadeOnDelete(false);

			//         modelBuilder.Entity<User>()
			//             .HasMany(e => e.UserRoles)
			//             .WithRequired(e => e.User)
			//             .WillCascadeOnDelete(false);
		}
	}
}
