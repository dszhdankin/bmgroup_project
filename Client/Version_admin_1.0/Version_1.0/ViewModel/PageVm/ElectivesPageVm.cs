using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.ViewModel.ControlVm;

namespace Version_1._0.ViewModel.PageVm
{
    class ElectivesPageVm : INotifyPropertyChanged
    {
        private delegate void AsyncCaller();

        private delegate void UiUpdater(List<ObservableCollection<Elective>> collections);

        private DayElectivesVm monday, tuesday, wednesday, thursday, friday, saturday;

        private void InitializeWeek()
        {
            DateTime monday = DateTime.Today;
            while (monday.DayOfWeek != DayOfWeek.Monday)
            {
                monday = monday.AddDays(-1.0);
            }
            Monday.Date = monday;
            Tuesday.Date = monday.AddDays(1.0);
            Wednesday.Date = monday.AddDays(2.0);
            Thursday.Date = monday.AddDays(3.0);
            Friday.Date = monday.AddDays(4.0);
            Saturday.Date = monday.AddDays(5.0);
        }

        private ObservableCollection<Elective> FetchByDate(DateTime date)
        {
            ObservableCollection<Elective> res = new ObservableCollection<Elective>();
            ObservableCollection<Elective> serverCollection = ModelGet<Elective>.getByDate(App.SERVER_NAME, date);
            if (serverCollection != null)
            {
                foreach (var curElective in serverCollection)
                {
                    res.Add(curElective);
                }
            }
            return res;
        }

        private void FetchData()
        {
            List<ObservableCollection<Elective>> collections = new List<ObservableCollection<Elective>>();
            collections.Add(FetchByDate(Monday.Date));
            collections.Add(FetchByDate(Tuesday.Date));
            collections.Add(FetchByDate(Wednesday.Date));
            collections.Add(FetchByDate(Thursday.Date));
            collections.Add(FetchByDate(Friday.Date));
            collections.Add(FetchByDate(Saturday.Date));
            App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new UiUpdater(UpdateUi), collections);
        }

        private void UpdateDayElectives(DayElectivesVm dayElectivesVm, ObservableCollection<Elective> collection)
        {
            dayElectivesVm.Electives.Clear();
            foreach (var curElective in collection)
            {
                dayElectivesVm.Electives.Add(curElective);
            }
        }

        private void UpdateUi(List<ObservableCollection<Elective>> collections)
        {
            UpdateDayElectives(Monday, collections[0]);
            Monday = monday;
            UpdateDayElectives(Tuesday, collections[1]);
            Tuesday = tuesday;
            UpdateDayElectives(Wednesday, collections[2]);
            Wednesday = wednesday;
            UpdateDayElectives(Thursday, collections[3]);
            Thursday = thursday;
            UpdateDayElectives(Friday, collections[4]);
            Friday = friday;
            UpdateDayElectives(Saturday, collections[5]);
            Saturday = saturday;
        }

        private void WeekForth()
        {
            Monday.Date = Monday.Date.AddDays(7.0);
            Tuesday.Date = Tuesday.Date.AddDays(7.0);
            Wednesday.Date = Wednesday.Date.AddDays(7.0);
            Thursday.Date = Thursday.Date.AddDays(7.0);
            Friday.Date = Friday.Date.AddDays(7.0);
            Saturday.Date = Saturday.Date.AddDays(7.0);
        }

        private void WeekBack()
        {
            Monday.Date = Monday.Date.AddDays(-7.0);
            Tuesday.Date = Tuesday.Date.AddDays(-7.0);
            Wednesday.Date = Wednesday.Date.AddDays(-7.0);
            Thursday.Date = Thursday.Date.AddDays(-7.0);
            Friday.Date = Friday.Date.AddDays(-7.0);
            Saturday.Date = Saturday.Date.AddDays(-7.0);
        }

        public DayElectivesVm Monday
        {
            get => monday;
            set
            {
                monday = value;
                OnPropertyChanged();
            }
        }
        public DayElectivesVm Tuesday
        {
            get => tuesday;
            set
            {
                tuesday = value;
                OnPropertyChanged();
            }
        }

        public DayElectivesVm Wednesday
        {
            get => wednesday;
            set
            {
                wednesday = value;
                OnPropertyChanged();
            }
        }
        public DayElectivesVm Thursday
        {
            get => thursday;
            set
            {
                thursday = value;
                OnPropertyChanged();
            }
        }
        public DayElectivesVm Friday
        {
            get => friday;
            set
            {
                friday = value;
                OnPropertyChanged();
            }
        }
        public DayElectivesVm Saturday
        {
            get => saturday;
            set
            {
                saturday = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; private set; }
        public ICommand NextWeekCommand { get; private set; }
        public ICommand PrevWeekCommand { get; private set; }

        public ElectivesPageVm()
        {
            Monday = new DayElectivesVm("Понедельник");
            Tuesday = new DayElectivesVm("Вторник");
            Wednesday = new DayElectivesVm("Среда");
            Thursday = new DayElectivesVm("Четверг");
            Friday = new DayElectivesVm("Пятница");
            Saturday = new DayElectivesVm("Суббота");

            InitializeWeek();
            UpdateCommand = new RelayCommand((obj) => { new AsyncCaller(FetchData).BeginInvoke(null, null); });
            NextWeekCommand =  new RelayCommand((obj) =>
            {
                WeekForth();
                new AsyncCaller(FetchData).BeginInvoke(null, null);
            });
            PrevWeekCommand = new RelayCommand((obj) =>
            {
                WeekBack();
                new AsyncCaller(FetchData).BeginInvoke(null, null);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
