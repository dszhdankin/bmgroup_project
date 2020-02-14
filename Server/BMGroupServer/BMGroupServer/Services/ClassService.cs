using BMGroupServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BMGroupServer.Services
{
    public static class ClassService
    {
        public static List<Class> GetClasses(int schoolId)
        {
            using (var context = new SchoolServiceDBContext())
            {
                var school = context.Schools.Find(schoolId);
                if (school is null)
                    throw new ArgumentException($"School {schoolId} not found");
                return school.Classes.ToList();
            }
        }

        public static Class GetClass(int classId)
        {
            using (var context = new SchoolServiceDBContext())
            {
                var cls = context.Classes.Find(classId);
                if (cls is null)
                    throw new ArgumentException($"Class {classId} not found");
                return cls;
            }
        }

        public static async Task<Class> CreateFromJson(string jsonString)
        {
            var js = new JavaScriptSerializer();
            var cls = (Class)js.Deserialize(jsonString, typeof(Class));
            using (var context = new SchoolServiceDBContext())
            {
                context.Classes.Add(cls);
                await context.SaveChangesAsync();
            }
            return cls;
        }
    }
}
