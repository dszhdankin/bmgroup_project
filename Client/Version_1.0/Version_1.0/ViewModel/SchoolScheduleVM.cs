using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;
using Version_1._0.View.Pages;

namespace Version_1._0.ViewModel
{
    class SchoolScheduleVM : INotifyPropertyChanged
    {
        private Class clas;

        private LessonVM _monday, _tuesday, _wednesday, _thursday, _friday, _saturday;

        private DaySchedule _mondayView,
            _tuesdayView,
            _wednesdayView,
            _thursdayView,
            _fridayView,
            _saturdayView;

        private void ChangeContext(object parameter)
        {
            _mondayView.DataContext = _monday;
            _tuesdayView.DataContext = _tuesday;
            _wednesdayView.DataContext = _wednesday;
            _thursdayView.DataContext = _thursday;
            _fridayView.DataContext = _friday;
            _saturdayView.DataContext = _saturday;
        }

        public string Name
        {
            get => clas.Title;
        }

        public ICommand ChangeContextCommand { get; private set; }

        public SchoolScheduleVM(Class clas, ObservableCollection<Lesson> lessons, SchedulePage schedulePage)
        {
            this.clas = clas;
            _monday = new LessonVM(DayOfWeek.Monday, lessons);
            _tuesday = new LessonVM(DayOfWeek.Tuesday, lessons);
            _wednesday = new LessonVM(DayOfWeek.Wednesday, lessons);
            _thursday = new LessonVM(DayOfWeek.Thursday, lessons);
            _friday = new LessonVM(DayOfWeek.Friday, lessons);
            _saturday = new LessonVM(DayOfWeek.Saturday, lessons);
            _mondayView = schedulePage.Monday;
            _tuesdayView = schedulePage.Tuesday;
            _wednesdayView = schedulePage.Wednesday;
            _thursdayView = schedulePage.Thursday;
            _fridayView = schedulePage.Friday;
            _saturdayView = schedulePage.Saturday;
            ChangeContextCommand = new RelayCommand(ChangeContext);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
