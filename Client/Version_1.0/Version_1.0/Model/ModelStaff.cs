using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Version_1._0.Model
{
    public class Teacher
    {
        public Teacher(int id, string name, string surname, string patronymic, byte[] photo, string subjects)
        {
            TeacherId = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Photo = photo;
            Subjects = subjects;
        }

        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public byte[] Photo { get; set; }
        public string Subjects { get; set; }
    }

    public class ModelStaff
    {
    }
}
