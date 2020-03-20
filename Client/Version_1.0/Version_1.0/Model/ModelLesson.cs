using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Version_1._0.Model
{
    public class Lesson : ModelItem
    {
        public int LessonId { get; set; }
        public int ClassId { get; set; }
        public string Info { get; set; }
        public DateTime Time { get; set; }

        public override string getWay()
        {
            return "Lessons/";
        }
    }
}