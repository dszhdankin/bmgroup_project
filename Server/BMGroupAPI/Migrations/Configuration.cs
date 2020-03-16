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
            Event[] events = {
                    new Event() {
                        Description = "Additional lesson of math",
                        Title = "Only for bad students",
                        StartTime = new DateTime(2020,6,7),
                        Photo = null
                    },
                    new Event() {
                        Description = "Additional lesson of language",
                        Title = "Only for good students",
                        StartTime = new DateTime(2020,10,5),
                        Photo = null
                    }
            };

            Ellective[] ellectives = {
                new Ellective() {
                    Info = "QWERTY",
                    Time = new DateTime(2020,10, 10, 10, 10, 10),
                    Title = "Very Important Ellective"
                },
                new Ellective() {
                    Info = "ASDFGH",
                    Time = new DateTime(2020,10, 10, 10, 10, 10),
                    Title = "Not very Important Ellective"
                },
                new Ellective() {
                    Info = "ZXCVBN",
                    Time = new DateTime(2020,10, 10, 10, 10, 10),
                    Title = "Usless Ellective"
                }
            };

            Lesson[] lessons = {
                new Lesson() {
                    ClassId = 1,
                    Info = "Math",
                    Time = new DateTime(2020, 1,12,9, 00, 00)
                },
                new Lesson() {
                    ClassId = 1,
                    Info = "Language",
                    Time = new DateTime(2020, 1, 12, 10, 30, 00)
                },
                new Lesson() {
                    ClassId = 1,
                    Info = "Prigramming",
                    Time = new DateTime(2020, 1, 12, 12, 00 , 00)
                },
                new Lesson() {
                    ClassId = 1,
                    Info = "Programming",
                    Time = new DateTime(2020, 1, 12, 13, 30, 00)
                },
                new Lesson() {
                    ClassId = 1,
                    Info = "Art",
                    Time = new DateTime(2020, 1, 12, 15, 00, 00)
                }
            };

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
