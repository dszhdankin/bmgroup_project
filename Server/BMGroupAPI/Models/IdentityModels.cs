using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace BMGroupAPI.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }
    }

    public class BMGroupAPIContext : IdentityDbContext<ApplicationUser>
    {
        public BMGroupAPIContext()
            : base("name=BMGroupAPIContext", throwIfV1Schema: false)
        {
            Database.Initialize(false);
        }

        static BMGroupAPIContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<BMGroupAPIContext>());
        }

        public static BMGroupAPIContext Create()
        {
            return new BMGroupAPIContext();
        }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Organization> Organizations { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Class> Classes { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Ellective> Ellectives { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Lesson> Lessons { get; set; }
    }
}