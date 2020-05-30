using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Dialogs;
using Version_1._0.ViewModel.WindowVm;

namespace Version_1._0.ViewModel.ControlVm
{
    class EmployeeVm : INotifyPropertyChanged
    {
        private Employee employee;

        private void PutEmployee(object parameter)
        {
            EditEmployee editEmployee = new EditEmployee();
            EditEmployeeVm editEmployeeVm = new EditEmployeeVm("PUT", employee);
            editEmployee.DataContext = editEmployeeVm;
            editEmployee.ShowDialog();
        }

        private void DeleteEmployee(object parameter)
        {
            Action<Employee> eventDeleter = new Action<Employee>((curEmployee) =>
            {
                try
                {
                    // TODO
                    // Put delete request for curEmployee here.
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => { MessageBox.Show(message); }), "Employee was successfully deleted!");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => { MessageBox.Show(message); }), e.Message);
                }
            });
            eventDeleter.BeginInvoke(employee, null, null);
        }

        public EmployeeVm(Employee employee)
        {
            this.employee = employee;
            EditEmployeeCommand = new RelayCommand(PutEmployee);
            DeleteEmployeeCommand = new RelayCommand(DeleteEmployee);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => employee.Name;
        }

        public string Position
        {
            get => "Не предусмотренно бэкэндом";
        }

        public string Description
        {
            get => employee.Info;
        }

        public ICommand EditEmployeeCommand { get; private set; }
        public ICommand DeleteEmployeeCommand { get; private set; }

        public BitmapImage Photo
        {
            get
            {
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(employee.getBytePhoto()))
                {
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
