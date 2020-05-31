using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;
using Version_1._0.View.Dialogs;
using Version_1._0.ViewModel.ControlVm;
using Version_1._0.ViewModel.WindowVm;

namespace Version_1._0.ViewModel.PageVm
{
    class EmployeesPageVm : INotifyPropertyChanged
    {
        private delegate void AsyncCaller();

        private delegate void UpdateUICaller(ObservableCollection<Employee> argument);

        private List<EmployeeVm> employeeVms = null;
        private ObservableCollection<EmployeeButton> employeeButtons = null;
        private ModelGet<Employee> modelGet = null;

        private void FetchServerData()
        {
            ObservableCollection<Employee> buffer = modelGet.get(App.SERVER_NAME);
            App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateUICaller(UpdateUI), buffer);
        }

        private void UpdateUI(ObservableCollection<Employee> buffer)
        {
            employeeVms.Clear();
            employeeButtons.Clear();
            if (buffer == null)
                return;
            foreach (var curEmployee in buffer)
            {
                EmployeeButton employeeButton = new EmployeeButton();
                EmployeeVm employeeVm = new EmployeeVm(curEmployee, employeeButton);
                employeeButton.DataContext = employeeVm;
                employeeVms.Add(employeeVm);
                employeeButtons.Add(employeeButton);
            }
        }

        private void Update(object parameter)
        {
            AsyncCaller fetcher = new AsyncCaller(FetchServerData);
            fetcher.BeginInvoke(null, null);
        }

        public String EmployeesTitle { get; private set; }
        public String SchoolOrUniName { get; private set; }

        public ICommand UpdateCommand { get; private set; }
        public ICommand AddEmployeeCommand { get; private set; }

        public EmployeesPageVm()
        {
            employeeButtons = new ObservableCollection<EmployeeButton>();
            employeeVms = new List<EmployeeVm>();
            modelGet = new ModelGet<Employee>();
            UpdateCommand = new RelayCommand(Update);
            EmployeesTitle = "Сотрудники";
            AddEmployeeCommand = new RelayCommand(obj =>
            {
                EditEmployee editEmployee = new EditEmployee();
                EditEmployeeVm editEmployeeVm = new EditEmployeeVm("POST", null);
                editEmployee.DataContext = editEmployeeVm;
                editEmployee.ShowDialog();
            });
        }

        public ObservableCollection<EmployeeButton> EmployeeButtons
        {
            get => employeeButtons;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

