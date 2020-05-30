using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BMGroupAPI.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }

        [Required]
        [ForeignKey("Class")]
        public int ClassId { get; set; }
 
        [IgnoreDataMember]                  // to prevent "Class" field from serialization
        public Class Class { get; set; }

        public string Info { get; set; }
        public DateTime Time { get; set; }
    }
}