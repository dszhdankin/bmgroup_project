using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class DayScheduleVm : INotifyPropertyChanged
    {
        private ObservableCollection<LessonVm> lessonList = null;
        private DateTime date;

        public string DayOfWeek { get; private set; }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<LessonVm> LessonList
        {
            get => lessonList;
        }

        public DayScheduleVm(DayOfWeek dayOfWeek)
        {
            lessonList = new ObservableCollection<LessonVm>();
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
