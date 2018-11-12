using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyTurn.Web.Models
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(DbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(RoleNames.ROLE_ADMINISTRATOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMINISTRATOR));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_VENDOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_VENDOR));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_USER))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_USER));
            }

            string userName = "admin@admin.com";
            string password = "[password]";

            ApplicationUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                IdentityResult userResult = userManager.Create(user, password);
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, RoleNames.ROLE_ADMINISTRATOR);
                }
            }
        }
    }
}