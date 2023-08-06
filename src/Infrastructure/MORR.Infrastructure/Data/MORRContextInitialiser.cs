using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MORR.Domain.Entities;

namespace MORR.Infrastructure.Data
{
    public class MORRContextInitialiser
    {
        private readonly ILogger<MORRContextInitialiser> _logger;
        private readonly MORRContext _context;

        public MORRContextInitialiser(ILogger<MORRContextInitialiser> logger, MORRContext context)
        {
            this._logger = logger;
            this._context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await SeedUsersAndRolesAsync();
                await SeedProductAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        private async Task SeedUsersAndRolesAsync()
        {
            if(!_context.Roles.Any())
            {
                var adminRole = new Role()
                {

                    Name = "Admin"
                };

                var managerRole = new Role()
                {

                    Name = "Manager"
                };

                _context.Roles.Add(adminRole);
                _context.Roles.Add(managerRole);


                if (!_context.Users.Any())
                {
                    var admin = new User()
                    {

                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "admin@morr.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass@123"),
                        IsActive = true,
                        CreatedDate = DateTime.Now,


                    };

                    var manager = new User()
                    {

                        FirstName = "Manager",
                        LastName = "Manager",
                        UserName = "manager@morr.com",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("pass@123"),
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                    };

                    _context.Users.Add(admin);
                    _context.Users.Add(manager);

                    if (!_context.UserRoles.Any())
                    {
                        var adminUserRole = new UserRole()
                        {
                            Role = adminRole,
                            User = admin,
                            IsActive = true,

                        };

                        var managerUserRole = new UserRole()
                        {
                            Role = managerRole,
                            User = manager,
                            IsActive = true,

                        };

                        _context.UserRoles.Add(adminUserRole);
                        _context.UserRoles.Add(managerUserRole);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedProductAsync()
        {
            if (!_context.Categories.Any())
            {
                var category1 = new Category()
                {
                    Name = "Electronics"
                };

                var category2 = new Category()
                {
                    Name = "Clothing & Fashion"
                };

                var category3 = new Category()
                {
                    Name = "Home & Kitchen"
                };

                var category4 = new Category()
                {
                    Name = "Health & Beauty"
                };

                var category5 = new Category()
                {
                    Name = "Sports & Outdoors"
                };

                var category6 = new Category()
                {
                    Name = "Toys & Games"
                };

                var category7 = new Category()
                {
                    Name = "Automotive"
                };

                var category8 = new Category()
                {
                    Name = "Food & Beverages"
                };

                var category9 = new Category()
                {
                    Name = "Pets"
                };

                _context.Categories.Add(category1);
                _context.Categories.Add(category2);
                _context.Categories.Add(category3);
                _context.Categories.Add(category4);
                _context.Categories.Add(category5);
                _context.Categories.Add(category6);
                _context.Categories.Add(category7);
                _context.Categories.Add(category8);
                _context.Categories.Add(category9);
            }

            await _context.SaveChangesAsync();
        }
    }
}
