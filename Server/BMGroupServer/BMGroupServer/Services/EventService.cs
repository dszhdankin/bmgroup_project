using BMGroupServer.Models;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

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

        public static Event GetEvent(int eventId)
        {
            using (var context = new SchoolServiceDBContext())
            {
                var evt = context.Events.Find(eventId);
                if (evt is null)
                    throw new ArgumentException($"Event {eventId} not found");
                return evt;
            }
        }

        public static async Task<Event> CreateFromJson(string jsonString)
        {
            var js = new JavaScriptSerializer();
            Event evt;
            try
            {
                evt = (Event)js.Deserialize(jsonString, typeof(Event));
            } catch (ArgumentException ex)
            {
                throw new PageNotFoundException("wrong JSON object format");
            }
            using (var context = new SchoolServiceDBContext())
            {
                context.Events.Add(evt);
                await context.SaveChangesAsync();
            }
            return evt;
        }
    }
}
