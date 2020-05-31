using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;

namespace Version_1._0.ViewModel.ControlVm
{
    class DayScheduleVm : INotifyPropertyChanged
    {
        public delegate void ApplyLessonsDelegate (DayScheduleVm dayScheduleVm);
        private ObservableCollection<LessonVm> lessonList = null;
        private DateTime date;
        private DaySchedule daySchedule;

        public event ApplyLessonsDelegate LessonsApplier;

        public List<Lesson> Lessons
        {
            get
            {
                List<Lesson> res = new List<Lesson>();
                foreach (var cur in lessonList)
                {
                    res.Add(cur.Lesson);
                }

                return res;
            }
        }

        private void AddLesson(object parameter)
        {
            Lesson lesson = new Lesson();
            lesson.Time = date.Date;
            lesson.Time = lesson.Time.AddHours(1.0);
            lesson.Info = "New lesson";
            lessonList.Add(new LessonVm(lesson));
        }

        private void DeleteLessons(object parameter)
        {
            List<int> indicies = new List<int>(daySchedule.SelectedIndicies);
            indicies.Sort();
            indicies.Reverse();
            foreach (var curIndex in indicies)
            {
                lessonList.RemoveAt(curIndex);
            }
        }

        private void ApplyLessons(object parameter)
        {
            LessonsApplier?.Invoke(this);
        }

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
            set
            {
                lessonList = value;
                OnPropertyChanged();
            }
        }

        public DayScheduleVm(DayOfWeek dayOfWeek, DaySchedule schedule)
        {
            this.daySchedule = schedule;
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
            AddLessonCommand = new RelayCommand(AddLesson);
            DeleteLessonsCommand = new RelayCommand(DeleteLessons);
            ApplyLessonsCommand = new RelayCommand(ApplyLessons);
        }

        public ICommand AddLessonCommand { get; private set; }
        public ICommand DeleteLessonsCommand { get; private set; }
        public ICommand ApplyLessonsCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
