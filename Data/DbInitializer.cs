using mabuddy.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace mabuddy.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> um,
            RoleManager<IdentityRole> rm, bool development)
        {
            var adminRole = new IdentityRole("Admin");
            var helperRole = new IdentityRole("Helper");
            var studentRole = new IdentityRole("Student");

            var admin = new ApplicationUser
                {UserName = "admin@uia.no", Email = "admin@uia.no", Nickname = "Admin", Age = 22, Level = 3};

            var admin2 = new ApplicationUser
                {UserName = "admin2@uia.no", Email = "admin2@uia.no", Nickname = "Admin2", Age = 22, Level = 3};

            var user = new ApplicationUser
                {UserName = "helper@uia.no", Email = "helper@uia.no", Nickname = "Helper", Age = 22, Level = 2};


            if (!development)
            {
                context.Database.Migrate();


                rm.CreateAsync(adminRole).Wait();
                rm.CreateAsync(helperRole).Wait();
                rm.CreateAsync(studentRole).Wait();

                um.CreateAsync(admin, "Password1.").Wait();
                um.AddToRoleAsync(admin, "Admin").Wait();


                um.CreateAsync(admin2, "Password1.").Wait();
                um.AddToRoleAsync(admin2, "Admin").Wait();


                um.CreateAsync(user, "Password1.").Wait();
                um.AddToRoleAsync(user, "Helper").Wait();

                return;
            }

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            rm.CreateAsync(adminRole).Wait();
            rm.CreateAsync(helperRole).Wait();
            rm.CreateAsync(studentRole).Wait();


            um.CreateAsync(admin, "Password1.").Wait();
            um.AddToRoleAsync(admin, "Admin").Wait();

            um.CreateAsync(admin2, "Password1.").Wait();
            um.AddToRoleAsync(admin2, "Admin").Wait();


            um.CreateAsync(user, "Password1.").Wait();
            um.AddToRoleAsync(user, "Helper").Wait();


            context.SaveChanges();
        }
    }
}