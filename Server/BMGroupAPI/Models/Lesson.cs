using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public int ClassId { get; set; }
        public string Info { get; set; }
        public DateTime Time { get; set; }
    }
}