using BMGroupServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMGroupServer.Services
{
    class EventService
    {
        public static List<Event> GetEvents(int schoolId)
        {
            using (var context = new SchoolServiceDBContext())
            {
                var school = context.Schools.Find(schoolId);
                if (school is null)
                    throw new ArgumentException($"School {schoolId} not found");
                return school.Events.ToList();
            }
        }
    }
}
