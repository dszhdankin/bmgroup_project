using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class LessonVm : INotifyPropertyChanged
    {
        private Lesson lesson;

        public Lesson Lesson
        {
            get => lesson;
        }

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
            set
            {
                string str = value;
                try
                {
                    int hour = int.Parse(str.Substring(0, 2));
                    int minute = int.Parse(str.Substring(3, 2));
                    lesson.Time = new DateTime(lesson.Time.Year, lesson.Time.Month, lesson.Time.Day, hour, minute, 0);
                    OnPropertyChanged();
                }
                catch (Exception e)
                {

                }
            }
        }

        public string Name
        {
            get => lesson.Info;
            set
            {
                lesson.Info = value;
                OnPropertyChanged();
            }
        }

        public string Cabinet
        {
            get => "None";
            set => OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
