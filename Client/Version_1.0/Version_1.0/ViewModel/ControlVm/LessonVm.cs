using System;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class LessonVm
    {
        private Lesson lesson;

        public LessonVm(Lesson lesson)
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
