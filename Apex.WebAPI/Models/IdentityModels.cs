using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Apex.WebAPI.Models
{
	// You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
		public ApplicationDbContext()
			: base("name = ApexDbConnectionString", throwIfV1Schema: false)
		{
			//disable initializer :: will not create the database
			//to creat the owin database, comment out the following line and run the application, once the database tables are created, uncomment the line
			//System.Data.Entity.Database.SetInitializer<ApplicationDbContext>(null);
		}
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}