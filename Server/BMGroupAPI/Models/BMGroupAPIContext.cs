using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class BMGroupAPIContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BMGroupAPIContext() : base("name=BMGroupAPIContext")
        {
            Database.Initialize(false);
        }

        static BMGroupAPIContext()
        {  
            Database.SetInitializer(new CreateDatabaseIfNotExists<BMGroupAPIContext>());   
        }
        

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Organization> Organizations { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Class> Classes { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Ellective> Ellectives { get; set; }

        public System.Data.Entity.DbSet<BMGroupAPI.Models.Lesson> Lessons { get; set; }
    }
}
