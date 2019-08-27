using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TitchyCottage.Security.Model;

namespace TitchyCottage.Security.DbContext
{
    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
             //Database.SetInitializer<AuthContext>(new DropCreateDatabaseIfModelChanges<AuthContext>());
        }

       //public DbSet<UserModel> User {get;set;}
    }
}
