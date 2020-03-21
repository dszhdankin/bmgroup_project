using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Version_1._0.Model;

namespace Version_1._0.ViewModel
{
    class SingleLessonVM
    {
        private Lesson lesson;

        public SingleLessonVM(Lesson lesson)
        {
            this.lesson = lesson;
        }

        public string Date
        {
            get
            {
                DateTime time = lesson.Time;
                return (time.Hour / 10).ToString() + time.Hour % 10 + ":"
                       + time.Minute / 10 + time.Minute % 10;
            }
        }

        public string Name
        {
            get => lesson.Info;
        }

        public string Cabinet
        {
            get => "None";
        }

    }
}
