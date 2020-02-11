using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMGroupServer.Models
{
    public class DbConn : DbContext
    {
        public DbConn() : base("SchoolServiceDB") // TODO: refactor with a connection string: base("name=DbConnectionString")
        {

        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
