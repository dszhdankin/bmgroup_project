using BMGroupServer.Models;
using System.Data.Entity;


namespace BMGroupServer.Services
{
    public class SchoolServiceDBContext : DbContext
    {
        static SchoolServiceDBContext()
        {
            // TODO: setup migrations
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SchoolServiceDBContext>());    // for the development time. TODO: change before production
        }
        public SchoolServiceDBContext() : base("SchoolServiceDB") // TODO: refactor with a connection string: base("name=DbConnectionString")
        {
            Database.Initialize(false);
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}

// TODO: template Service<T> class
