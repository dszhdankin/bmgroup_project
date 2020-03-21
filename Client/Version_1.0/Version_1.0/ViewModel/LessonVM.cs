using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel
{
    class LessonVM : INotifyPropertyChanged
    {
        private ObservableCollection<SingleLessonVM> lessonList = null;

        public string DayOfWeek { get; private set; }

        public ObservableCollection<SingleLessonVM> LessonList
        {
            get => lessonList;
        }

        public LessonVM(DayOfWeek dayOfWeek, ObservableCollection<Lesson> allLessons)
        {
            var query = from lesson in allLessons
                where lesson.Time.DayOfWeek == dayOfWeek
                select lesson;
            lessonList = new ObservableCollection<SingleLessonVM>();
            foreach (var lesson in query)
            {
                lessonList.Add(new SingleLessonVM(lesson));
            }
            switch (dayOfWeek)
            {
                case System.DayOfWeek.Monday: DayOfWeek = "Понедельник";
                    break;
                case System.DayOfWeek.Tuesday:
                    DayOfWeek = "Вторник";
                    break;
                case System.DayOfWeek.Wednesday:
                    DayOfWeek = "Среда";
                    break;
                case System.DayOfWeek.Thursday:
                    DayOfWeek = "Четверг";
                    break;
                case System.DayOfWeek.Friday:
                    DayOfWeek = "Пятница";
                    break;
                case System.DayOfWeek.Saturday:
                    DayOfWeek = "Суббота";
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
