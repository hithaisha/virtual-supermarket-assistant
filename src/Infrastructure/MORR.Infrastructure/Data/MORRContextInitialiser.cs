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
    }
}
