using CleanArchitecture.Application.Common.Extensions;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Data
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default roles
            if (!_context.Roles.Any())
            {
                var applicationRoles = new List<Role>()
                {
                  new Role { Name = Roles.Administrator },
                  new Role { Name = Roles.User }
                };

                await _context.Roles.AddRangeAsync(applicationRoles);
                await _context.SaveChangesAsync();
            }

            if (!_context.Users.Any())
            {

                var adminRole = await _context.Roles.Where(x => x.Name == Roles.Administrator).FirstOrDefaultAsync();
                var userRole = await _context.Roles.Where(x => x.Name == Roles.User).FirstOrDefaultAsync();
                var defaultPassword = "Password@1".WithBCrypt();

                var defaultUsers = new List<User>()
                {
                  new User {
                      FirstName = "Application", LastName = "Admin",
                      Email = "Admin@CleanArchitecture.com", Password = defaultPassword,
                      CreatedAtUtc = DateTime.UtcNow, RoleId = adminRole.Id
                  },
                  new User {
                      FirstName = "Application", LastName = "User",
                      Email = "User@CleanArchitecture.com", Password = defaultPassword,
                      CreatedAtUtc = DateTime.UtcNow, RoleId = userRole.Id
                  }
                };

                await _context.Users.AddRangeAsync(defaultUsers);
                await _context.SaveChangesAsync();
            }

            if (!_context.Notes.Any())
            {
                var user = await _context.Users.Where(x => x.Email == "Admin@CleanArchitecture.com").FirstOrDefaultAsync();

                var defaultNotes = new List<Note>()
                {
                  new Note {
                        Title = "Prepare Presentation",
                        Content = "Prepare for .NET Global Summit Presentation.",
                        CreatedById = user.Id,
                        CreatedAtUtc = DateTime.UtcNow
                  },
                  new Note {
                      Title = "Deliver Presentation",
                    Content = "Deliver the presentation in .NET Global Summit.",
                    CreatedById = user.Id,
                    CreatedAtUtc = DateTime.UtcNow
                  }
                };

                await _context.Notes.AddRangeAsync(defaultNotes);
                await _context.SaveChangesAsync();
            }

        }
    }
}
