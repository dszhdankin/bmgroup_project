using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Version_1._0.Utilities;
using Version_1._0.View.Pages;

namespace Version_1._0.ViewModel
{
    class MainPageVM : INotifyPropertyChanged
    {
        private string employeesTitle = "Сотрудники";
        private string eventsTitle = "Мероприятия";
        private string electivesTitle = "Факультативы";
        private string timetableTitle = "Расписание";
        private string schoolOrUniName = "Название заведения";
        public ICommand ToEventsCommand { get; private set; }

        public MainPageVM()
        {
            ToEventsCommand = new RelayCommand(ToEvents);
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
