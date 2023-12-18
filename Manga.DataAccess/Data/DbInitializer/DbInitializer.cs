using System;
using Manga.DataAccess.Data;
using Manga.Models;
using Manga.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging;

namespace Manga.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
     
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<DbInitializer> _logger;


        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db,
            ILogger<DbInitializer> logger

            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _db = db;
            _logger = logger;
        }

        public void Initialize()
        {
            //migrations if they are not applied

            try {
                if (_db.Database.GetPendingMigrations().Count()>0)
                {
                    _logger.LogInformation("Applying pending migrations...");
                    _db.Database.Migrate();
                    _logger.LogInformation("Migrations applied successfully.");
                }
                else
                {
                    _logger.LogInformation("No pending migrations.");
                }
            }
            catch(Exception ex) {
                _logger.LogError(ex, "Error applying migrations: {Message}", ex.Message);
            }

            //create roles if they are not created

            if (!_roleManager.RoleExistsAsync(Commun.Role_Customer).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Commun.Role_Company)).GetAwaiter().GetResult();



                //if roles are not created, then we will create admin user as well

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "b1912105551@sakarya.edu.tr",
                    Email = "b191210551@sakarya.edu.tr",
                    Name = "Alpha Bah",
                    PhoneNumber = "05310000000",
                    StreetAddress = "Serdivan",
                    State = "Turkey",
                    PostalCode = "54580",
                    City = "Sakarya"
                }, "aA@12345678").GetAwaiter().GetResult();

                ApplicationUser user = _db.ApplicationUsers
                    .FirstOrDefault(u => u.Email == "b191210551@sakarya.edu.tr");
                _userManager.AddToRoleAsync(user, Commun.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}

