using BMGroupServer.Models;
using System.Data.Entity;


namespace BMGroupServer.Services
{
    public class SchoolServiceDBContext : DbContext
    {
        public SchoolServiceDBContext() : base("SchoolServiceDB") // TODO: refactor with a connection string: base("name=DbConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SchoolServiceDBContext>());    // for the development time. TODO: change before production
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
