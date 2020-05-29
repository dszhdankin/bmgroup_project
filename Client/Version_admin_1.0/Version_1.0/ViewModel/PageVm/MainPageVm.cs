using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using Version_1._0.Utilities;
using Version_1._0.View.Pages;

namespace Version_1._0.ViewModel.PageVm
{
    class MainPageVm : INotifyPropertyChanged
    {
        private string employeesTitle = "Сотрудники";
        private string eventsTitle = "Мероприятия";
        private string electivesTitle = "Факультативы";
        private string timetableTitle = "Расписание";
        private string schoolOrUniName = "Название заведения";
        public ICommand ToEventsCommand { get; private set; }
        public ICommand ToEmployeesCommand { get; private set; }
        public ICommand ToScheduleCommand { get; private set; }

        public MainPageVm()
        {
            ToEventsCommand = new RelayCommand(ToEvents);
            ToEmployeesCommand = new RelayCommand(ToEmployees);
            ToScheduleCommand = new RelayCommand(ToSchedule);
        }

        public string EmployeesTitle
        {
            get => employeesTitle;
            set
            {
                employeesTitle = value;
                OnPropertyChanged();
            } 
        }

        public string EventsTitle
        {
            get => eventsTitle;
            set
            {
                eventsTitle = value;
                OnPropertyChanged();
            } 
        }

        public string ElectivesTitle
        {
            get => electivesTitle;
            set
            {
                electivesTitle = value;
                OnPropertyChanged();
            } 
        }

        public string TimetableTitle
        {
            get => timetableTitle;
            set
            {
                timetableTitle = value;
                OnPropertyChanged();
            } 
        }

        public string SchoolOrUniName
        {
            get => schoolOrUniName;
            set
            {
                schoolOrUniName = value;
                OnPropertyChanged();
            } 
        }

        private void ToEvents(object parameter)
        {
            App.MainNavigationService.Navigate(new EventsPage());
        }

        private void ToEmployees(object parameter)
        {
            App.MainNavigationService.Navigate(new EmployeesPage());
        }

        private void ToSchedule(object parameter)
        {
            App.MainNavigationService.Navigate(new SchedulePage());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
