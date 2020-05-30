using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;
using Version_1._0.View.Pages;
using Version_1._0.ViewModel.ControlVm;

namespace Version_1._0.ViewModel.PageVm
{
    class SchedulePageVm : INotifyPropertyChanged
    {
        private delegate void AsyncCaller();
        private delegate void UpdateClassesCaller(ObservableCollection<Class> classes);
        private delegate void UpdateSchedulesCaller(List<ObservableCollection<Lesson>> collections);
        private delegate void AsyncLessonsFetcher(int id, string cln, DateTime dt1, DateTime dt2, DateTime dt3, 
            DateTime dt4, DateTime dt5, DateTime dt6);

        private ModelGet<Class> classFetcher;
        private ModelGet<Lesson> lessonFetcher;
        private ObservableCollection<GroupButton> groupButtons;
        private SchedulePage schedulePage;
        private DayScheduleVm mondayVm, tuesdayVm, wednesdayVm, thursdayVm, fridayVm, saturdayVm;
        private ObservableCollection<Class> classes;
        private int curClassId = App.NO_ID;

        private Class FindClassById(int id)
        {
            if (classes.Count == 0)
                return null;
            Class result = null;
            int left = 0, right = classes.Count, m;
            while (left + 1 < right)
            {
                m = (left + right) >> 1;
                if (classes[m].ClassId > id)
                    right = m;
                else
                    left = m;
            }

            if (classes[left].ClassId == id)
                return classes[left];
            else
                return null;
        }

        private void FetchClasses()
        {
            ObservableCollection<Class> classes = new ObservableCollection<Class>();
            ObservableCollection<Class> serverClasses = classFetcher.get(App.SERVER_NAME);
            if (serverClasses != null)
            {
                foreach (var curClass in serverClasses)
                {
                    classes.Add(curClass);
                }
            }
            App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateClassesCaller(UpdateUiClasses), classes);
        }

        private void ChangeClass(object parameter)
        {
            GroupButton groupButton = (GroupButton) parameter;
            curClassId = groupButton.ClassId;
            CurClassName = "Property changed";
            AsyncLessonsFetcher fetcher = new AsyncLessonsFetcher(FetchServerLessons);
            fetcher.BeginInvoke(curClassId, CurClassName, mondayVm.Date, tuesdayVm.Date,
                wednesdayVm.Date, thursdayVm.Date, fridayVm.Date, saturdayVm.Date, null, null);
        }

        private void UpdateUiClasses(ObservableCollection<Class> classes)
        {
            this.classes = classes;
            GroupButtons.Clear();
            foreach (Class curClass in classes)
            {
                GroupButton curButton = new GroupButton(curClass.ClassId);
                curButton.DataContext = new GroupButtonVm(curClass, ChangeClassCommand);
                groupButtons.Add(curButton);
            }
            CurClassName = "Property changed";
            AsyncLessonsFetcher fetcher = new AsyncLessonsFetcher(FetchServerLessons);
            fetcher.BeginInvoke(curClassId, CurClassName, mondayVm.Date, tuesdayVm.Date,
                wednesdayVm.Date, thursdayVm.Date, fridayVm.Date, saturdayVm.Date, null, null);
        }

        private void UpdateUiDaySchedule(DayScheduleVm scheduleVm, ObservableCollection<Lesson> lessons)
        {
            scheduleVm.LessonList.Clear();
            foreach (Lesson curLesson in lessons)
            {
                scheduleVm.LessonList.Add(new LessonVm(curLesson));
            }
        }

        private ObservableCollection<Lesson> getLessons(int classId, DateTime date)
        {
            int curClassId = this.curClassId;
            ObservableCollection<Lesson> res = new ObservableCollection<Lesson>();
            ObservableCollection<Lesson> serverLessons = ModelGet<Lesson>.getByDateId(App.SERVER_NAME, date, classId);
            if (serverLessons != null)
            {
                foreach (var curLesson in serverLessons)
                {
                    res.Add(curLesson);
                }
            }
            return res;
        }

        private void UpdateSchedules(List<ObservableCollection<Lesson>> collection)
        {
            UpdateUiDaySchedule(mondayVm, collection[0]);
            UpdateUiDaySchedule(tuesdayVm, collection[1]);
            UpdateUiDaySchedule(wednesdayVm, collection[2]);
            UpdateUiDaySchedule(thursdayVm, collection[3]);
            UpdateUiDaySchedule(fridayVm, collection[4]);
            UpdateUiDaySchedule(saturdayVm, collection[5]);
        }

        private void FetchServerLessons(int classId, string className, DateTime monday, DateTime tuesday, DateTime wednesday,
            DateTime thursday, DateTime friday, DateTime saturday)
        {
            if (className == null)
                return;
            List<ObservableCollection<Lesson>> collections = new List<ObservableCollection<Lesson>>();
            collections.Add(getLessons(classId, monday));
            collections.Add(getLessons(classId, tuesday));
            collections.Add(getLessons(classId, wednesday));
            collections.Add(getLessons(classId, thursday));
            collections.Add(getLessons(classId, friday));
            collections.Add(getLessons(classId, saturday));
            App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateSchedulesCaller(UpdateSchedules), collections);
        }

        private void Update(object parameter)
        {
            AsyncCaller fetcher = new AsyncCaller(FetchClasses);
            fetcher.BeginInvoke(null, null);
        }

        private void InitializeWeek()
        {
            DateTime monday = DateTime.Today;
            while (monday.DayOfWeek != DayOfWeek.Monday)
            {
                monday = monday.AddDays(-1.0);
            }
            mondayVm.Date = monday;
            tuesdayVm.Date = monday.AddDays(1.0);
            wednesdayVm.Date = monday.AddDays(2.0);
            thursdayVm.Date = monday.AddDays(3.0);
            fridayVm.Date = monday.AddDays(4.0);
            saturdayVm.Date = monday.AddDays(5.0);
        }

        private void WeekForth()
        {
            mondayVm.Date = mondayVm.Date.AddDays(7.0);
            tuesdayVm.Date = tuesdayVm.Date.AddDays(7.0);
            wednesdayVm.Date = wednesdayVm.Date.AddDays(7.0);
            thursdayVm.Date = thursdayVm.Date.AddDays(7.0);
            fridayVm.Date = fridayVm.Date.AddDays(7.0);
            saturdayVm.Date = saturdayVm.Date.AddDays(7.0);
        }

        private void WeekBack()
        {
            mondayVm.Date = mondayVm.Date.AddDays(-7.0);
            tuesdayVm.Date = tuesdayVm.Date.AddDays(-7.0);
            wednesdayVm.Date = wednesdayVm.Date.AddDays(-7.0);
            thursdayVm.Date = thursdayVm.Date.AddDays(-7.0);
            fridayVm.Date = fridayVm.Date.AddDays(-7.0);
            saturdayVm.Date = saturdayVm.Date.AddDays(-7.0);
        }

        public RelayCommand UpdateCommand { get; private set; }
        public RelayCommand PrevWeekCommand { get; private set; }
        public RelayCommand NextWeekCommand { get; private set; }
        public RelayCommand ChangeClassCommand { get; private set; }

        public SchedulePageVm(SchedulePage schedulePage)
        {
            this.schedulePage = schedulePage;
            classes = new ObservableCollection<Class>();
            groupButtons = new ObservableCollection<GroupButton>();
            lessonFetcher = new ModelGet<Lesson>();
            classFetcher = new ModelGet<Class>();

            mondayVm = new DayScheduleVm(DayOfWeek.Monday);
            tuesdayVm = new DayScheduleVm(DayOfWeek.Tuesday);
            wednesdayVm = new DayScheduleVm(DayOfWeek.Wednesday);
            thursdayVm = new DayScheduleVm(DayOfWeek.Thursday);
            fridayVm = new DayScheduleVm(DayOfWeek.Friday);
            saturdayVm = new DayScheduleVm(DayOfWeek.Saturday);

            this.schedulePage.Monday.DataContext = mondayVm;
            this.schedulePage.Tuesday.DataContext = tuesdayVm;
            this.schedulePage.Wednesday.DataContext = wednesdayVm;
            this.schedulePage.Thursday.DataContext = thursdayVm;
            this.schedulePage.Friday.DataContext = fridayVm;
            this.schedulePage.Saturday.DataContext = saturdayVm;

            InitializeWeek();

            groupButtons = new ObservableCollection<GroupButton>();
            UpdateCommand = new RelayCommand(Update);
            NextWeekCommand = new RelayCommand((obj) =>
            {
                WeekForth();
                AsyncLessonsFetcher fetcher = new AsyncLessonsFetcher(FetchServerLessons);
                fetcher.BeginInvoke(curClassId, CurClassName, mondayVm.Date, tuesdayVm.Date,
                    wednesdayVm.Date, thursdayVm.Date, fridayVm.Date, saturdayVm.Date, null, null);
            });
            PrevWeekCommand = new RelayCommand((obj) =>
            {
                WeekBack();
                AsyncLessonsFetcher fetcher = new AsyncLessonsFetcher(FetchServerLessons);
                fetcher.BeginInvoke(curClassId, CurClassName, mondayVm.Date, tuesdayVm.Date,
                    wednesdayVm.Date, thursdayVm.Date, fridayVm.Date, saturdayVm.Date, null, null);
            });
            ChangeClassCommand = new RelayCommand(ChangeClass);
        }

        public ObservableCollection<GroupButton> GroupButtons
        {
            get => groupButtons;
        }

        public string CurClassName
        {
            set => OnPropertyChanged();
            get
            {
                Class curClass = FindClassById(curClassId);
                if (curClass == null)
                    return null;
                else
                    return curClass.Title;
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
