using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;
using Version_1._0.View.Pages;

namespace Version_1._0.ViewModel
{
    class SchedulePageVM : INotifyPropertyChanged
    {
        private delegate void AsyncCaller();

        private delegate void UpdateUiCaller(ObservableCollection<Class> classes, ObservableCollection<Lesson> lessons);

        private ObservableCollection<SchoolScheduleVM> schoolScheduleVms = null;
        private ObservableCollection<GroupButton> groupButtons = null;
        private SchedulePage schedulePage;
        private ModelGet<Class> classesGetter;
        private ModelGet<Lesson> lessonsGetter;
        

        private void FetchServerData()
        {
            ObservableCollection<Class> classes = classesGetter.get(App.SERVER_NAME);
            ObservableCollection<Lesson> lessons = lessonsGetter.get(App.SERVER_NAME);
            App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateUiCaller(UpdateUi), classes, lessons);
        }

        private void UpdateUi(ObservableCollection<Class> classes, ObservableCollection<Lesson> lessons)
        {
            schoolScheduleVms.Clear();
            GroupButtons.Clear();
            if (classes == null || lessons == null)
                return;
            var sortClasses = from clas in classes orderby clas.ClassId select clas;
            var groupAndSortLessons =
                from lesson in lessons
                group lesson by lesson.ClassId
                into lessonGroup
                orderby lessonGroup.Key
                select lessonGroup;
            schoolScheduleVms.Clear();
            groupButtons.Clear();
            var listOfClasses = sortClasses.ToList();
            int indexOfClass = 0;
            foreach (var curGrouping in groupAndSortLessons.ToList())
            {
                SchoolScheduleVM schoolScheduleVm = new SchoolScheduleVM(listOfClasses[indexOfClass], 
                    new ObservableCollection<Lesson>(curGrouping), schedulePage);
                GroupButton groupButton = new GroupButton();
                groupButton.DataContext = schoolScheduleVm;
                schoolScheduleVms.Add(schoolScheduleVm);
                groupButtons.Add(groupButton);
                indexOfClass++;
            }
        }

        private void Update(object parameter)
        {
            AsyncCaller fetcher = new AsyncCaller(FetchServerData);
            fetcher.BeginInvoke(null, null);
        }

        public ICommand UpdateCommand { get; private set; }

        public SchedulePageVM(SchedulePage schedulePage)
        {
            this.schedulePage = schedulePage;
            schoolScheduleVms = new ObservableCollection<SchoolScheduleVM>();
            groupButtons = new ObservableCollection<GroupButton>();
            classesGetter = new ModelGet<Class>();
            lessonsGetter = new ModelGet<Lesson>();
            UpdateCommand = new RelayCommand(Update);
        }

        public ObservableCollection<GroupButton> GroupButtons
        {
            get => groupButtons;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
