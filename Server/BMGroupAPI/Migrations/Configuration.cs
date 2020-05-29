namespace BMGroupAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BMGroupAPI.Models.BMGroupAPIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BMGroupAPI.Models.BMGroupAPIContext context)
        {

            DateTime startDay = DateTime.Today;

            Event[] events = {
                    new Event() {
                        Description = "Additional lesson of math",
                        Title = "Only for bad students",
                        StartTime = startDay.AddDays(3).AddHours(19),
                        Photo = null,
                    },
                    new Event() {
                        Description = "Additional lesson of language",
                        Title = "Only for good students",
                        StartTime = startDay.AddDays(5).AddHours(17),
                        Photo = null
                    }
            };

            Ellective[] ellectives = {
                new Ellective() {
                    Info = "QWERTY",
                    Time = startDay.AddDays(1).AddHours(10),
                    Title = "Very Important Ellective",
                },
                new Ellective() {
                    Info = "ASDFGH",
                    Time = startDay.AddDays(4).AddHours(14),
                    Title = "Not very Important Ellective",
                },
                new Ellective() {
                    Info = "ZXCVBN",
                    Time = startDay.AddDays(10).AddHours(19),
                    Title = "Useless Ellective",
                }
            };


            const int DAYS = 20;
            Lesson[] lessons = new Lesson[5 * DAYS];

            for (int day = 0; day < DAYS; ++day)
            {
                lessons[day * 5] = new Lesson()
                {
                    ClassId = 1,
                    Info = "Math",
                    Time = startDay.AddDays(day).AddHours(9),
                };
                lessons[day * 5 + 1] = new Lesson()
                {
                    ClassId = 1,
                    Info = "English",
                    Time = startDay.AddDays(day).AddHours(10).AddMinutes(30),
                };
                lessons[day * 5 + 2] = new Lesson()
                {
                    ClassId = 1,
                    Info = "Programming",
                    Time = startDay.AddDays(day).AddHours(12),
                };
                lessons[day * 5 + 3] = new Lesson()
                {
                    ClassId = 1,
                    Info = "Programming",
                    Time = startDay.AddDays(day).AddHours(13).AddMinutes(30),
                };
                lessons[day * 5 + 4] = new Lesson()
                {
                    ClassId = 1,
                    Info = "Art",
                    Time = startDay.AddDays(day).AddHours(15),
                };
            }
            /*
            int day = 0;
            Lesson[] lessons = new Lesson[1];
            lessons[0] = new Lesson()
            {
                ClassId = 1,
                Info = "Math",
                Time = new DateTime(2020, 3 + day, 23, 9, 0, 0)
            };
            */
            Class[] classes = {
                    new Class() {
                        Title = "11A"
                    },
                    new Class() {
                        Title = "10A"
                    },
                    new Class() {
                        Title = "11B"
                    }
                };

            Employee[] employees = {
                    new Employee () {
                        Info = "Teacher of math",
                        Name = "Ivan Ivanov Ivanovich",
                    },
                    new Employee () {
                        Info = "Teacher of language",
                        Name = "AAA BBB CCC",
                    }
            };

            Organization org1 = new Organization() {
                Description = "School1"
            };

            //if (context.Organizations.Count() != 0)
             //   return;
            
            context.Organizations.AddOrUpdate(org1);
            org1.Classes = classes;
            org1.Employees = employees;
            org1.Events = events;
            context.Lessons.AddOrUpdate(lessons);
            context.Ellectives.AddOrUpdate(ellectives);
            context.SaveChanges();
            // context.Employees.AddOrUpdate(employees);
            //context.Events.AddOrUpdate(events);
            
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
