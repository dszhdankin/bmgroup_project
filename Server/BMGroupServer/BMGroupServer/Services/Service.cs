using BMGroupServer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BMGroupServer.Services
{
    class Service<T> where T: Model
    {
        static PropertyInfo schoolInfo;
        static PropertyInfo contextInfo;

        static Service()
        {
            schoolInfo = (from s in typeof(School).GetProperties()
                         where typeof(ICollection<T>) == s.PropertyType
                         select s).First();
            
            contextInfo = (from s in typeof(SchoolServiceDBContext).GetProperties()
                           where typeof(DbSet<T>) == s.PropertyType
                           select s).First();
        }

        public static List<T> GetList(int schoolId)
        {
            using (var context = new SchoolServiceDBContext())
            {
                var school = context.Schools.Find(schoolId);
                if (school is null)
                    throw new ArgumentException($"School {schoolId} not found");
                var collection = (ICollection<T>)schoolInfo.GetValue(school);
                return collection.ToList();
            }
        }

        public static T Get(int id)
        {
            using (var context = new SchoolServiceDBContext())
            {
                var collection = (DbSet<T>)contextInfo.GetValue(context);
                T t = collection.Find(id);
                if (t is null)
                    throw new ArgumentException($"{typeof(T).Name} {id} not found");
                return t;
            }
        }

        public static async Task<T> CreateFromJson(string jsonString, int schoolId)
        {
            var js  = new JavaScriptSerializer();
            T t     = (T)js.Deserialize(jsonString, typeof(T));
            using (var context = new SchoolServiceDBContext())
            {
                var school = context.Schools.Find(schoolId);
                if (school is null)
                    throw new ArgumentException($"School {schoolId} not found");

                ((ICollection<T>)schoolInfo.GetValue(school)).Add(t);
                await context.SaveChangesAsync();
            }
            return t;
        }
    }
}
