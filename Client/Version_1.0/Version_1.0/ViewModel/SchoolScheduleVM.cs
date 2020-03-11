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

namespace Version_1._0.ViewModel
{
    class SchoolScheduleVM : INotifyPropertyChanged
    {
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

        public string Name { get; private set; }

        public ICommand ChangeContextCommand { get; private set; }

        public SchoolScheduleVM(string name, bool slave, DaySchedule monday, DaySchedule tuesday, 
            DaySchedule wednesday, DaySchedule thursday, DaySchedule friday, DaySchedule saturday)
        {
            Name = name;
            _monday = new LessonVM("Понедельник", slave);
            _tuesday = new LessonVM("Вторник", slave);
            _wednesday = new LessonVM("Среда", slave);
            _thursday = new LessonVM("Четверг", slave);
            _friday = new LessonVM("Пятница", slave);
            _saturday = new LessonVM("Суббота", slave);
            _mondayView = monday;
            _tuesdayView = tuesday;
            _wednesdayView = wednesday;
            _thursdayView = thursday;
            _fridayView = friday;
            _saturdayView = saturday;
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
