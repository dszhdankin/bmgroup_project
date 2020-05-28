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

namespace Version_1._0.ViewModel
{
    class EmployeesPageVM : INotifyPropertyChanged
    {
        private delegate void AsyncCaller();

        private delegate void UpdateUICaller(ObservableCollection<Employee> argument);

        private List<EmployeeVM> employeeVms = null;
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
                EmployeeVM employeeVm = new EmployeeVM(curEmployee);
                EmployeeButton employeeButton = new EmployeeButton();
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

        public EmployeesPageVM()
        {
            employeeButtons = new ObservableCollection<EmployeeButton>();
            employeeVms = new List<EmployeeVM>();
            modelGet = new ModelGet<Employee>();
            UpdateCommand = new RelayCommand(Update);
            EmployeesTitle = "Сотрудники";
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

