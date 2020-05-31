using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;

namespace Version_1._0.ViewModel.ControlVm
{
    class DayElectivesVm : INotifyPropertyChanged
    {
        private delegate void Applier(DateTime date, List<Elective> electives);
        private string dayOfWeek;
        private DateTime date;
        private ObservableCollection<ElectiveVm> electives;
        private DayElectives dayElectives;

        private void AddElective(object parameter)
        {
            Elective elective = new Elective();
            elective.Title = "Elective";
            elective.Time = date.Date.AddHours(1.0);
            elective.Info = "Elective info";
            electives.Add(new ElectiveVm(elective));
        }

        private void RemoveElectives(object parameter)
        {
            List<int> indices = new List<int>(dayElectives.SelectedIndices);
            indices.Sort();
            indices.Reverse();
            foreach (var curIndex in indices)
                electives.RemoveAt(curIndex);
        }

        private void ApplyElectives(object parameter)
        {
            Applier applier = new Applier((curDate, curElectives) =>
            {
                try
                {
                    ObservableCollection<Elective> serverElectives = ModelGet<Elective>.getByDate(App.SERVER_NAME, date.Date);
                    foreach (var curElective in serverElectives)
                    {
                        ModelGet<Elective>.delete(App.SERVER_NAME, curElective.EllectiveId);
                    }
                    foreach (var curElective in curElectives)
                    {
                        ModelGet<Elective>.post(App.SERVER_NAME, curElective);
                    }
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(message =>
                        MessageBox.Show(message)), "Successfully applied!");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(message =>
                        MessageBox.Show(message)), e.Message);
                }
            });
            applier.BeginInvoke(date, ElectivesList, null, null);
        }

        public List<Elective> ElectivesList
        {
            get
            {
                List<Elective> res = new List<Elective>();
                foreach (var cur in electives)
                    res.Add(cur.Elective);
                return res;
            }
        }

        public ICommand AddElectiveCommand { get; private set; }
        public ICommand DeleteElectivesCommand { get; private set; }
        public ICommand ApplyElectivesCommand { get; private set; }

        public string DayOfWeek
        {
            get => dayOfWeek;
            set
            {
                dayOfWeek = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ElectiveVm> Electives
        {
            get => electives;
            set
            {
                electives = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public DayElectivesVm(string dayOfWeek, DayElectives dayElectives)
        {
            this.dayElectives = dayElectives;
            Electives = new ObservableCollection<ElectiveVm>();
            DayOfWeek = dayOfWeek;
            AddElectiveCommand = new RelayCommand(AddElective);
            DeleteElectivesCommand = new RelayCommand(RemoveElectives);
            ApplyElectivesCommand = new RelayCommand(ApplyElectives);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
