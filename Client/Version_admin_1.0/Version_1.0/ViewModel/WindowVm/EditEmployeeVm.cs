using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using JetBrains.Annotations;
using Microsoft.Win32;
using Version_1._0.Model;
using Version_1._0.Utilities;

namespace Version_1._0.ViewModel.WindowVm
{
    class EditEmployeeVm : INotifyPropertyChanged
    {
        private Employee employee;

        private void ChoosePhoto(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                string filePath = fileDialog.FileName;
                employee.Photo = Convert.ToBase64String(File.ReadAllBytes(filePath));
                Photo = null;
            }
        }

        private void PostEmployee(object parameter)
        {
            Action<Employee> dataPoster = new Action<Employee>(curEmployee =>
            {
                try
                {
                    // TODO
                    // Make a POST request for curEmployee
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => MessageBox.Show(message)), "Employee was successfully added!");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => MessageBox.Show(message)), e.Message);
                }
            });
            dataPoster.BeginInvoke(employee, null, null);
        }

        private void PutEmployee(object parameter)
        {
            Action<Employee> dataPuter = new Action<Employee>(curEmployee =>
            {
                try
                {
                    // TODO
                    // Make a PUT request for curEmployee
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => MessageBox.Show(message)), "Employee was successfully edited!");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => MessageBox.Show(message)), e.Message);
                }
            });
            dataPuter.BeginInvoke(employee, null, null);
        }

        public EditEmployeeVm(string requestType = "POST", Employee employee = null)
        {
            if (employee != null)
            {
                this.employee = new Employee();
                this.employee.EmployeeId = employee.EmployeeId;
                this.employee.Info = employee.Info;
                this.employee.Name = employee.Name;
                this.employee.Photo = employee.Photo;
            }
            else
            {
                this.employee = new Employee();
                this.employee.Info = "Employee info";
                this.employee.Name = "Employee name";
                this.employee.Photo = "";
            }
            ChoosePhotoCommand = new RelayCommand(ChoosePhoto);
            if (requestType.Equals("POST"))
                RequestCommand = new RelayCommand(PostEmployee);
            else if (requestType.Equals("PUT"))
                RequestCommand = new RelayCommand(PutEmployee);
        }

        public string Name
        {
            get => employee.Name;
            set
            {
                employee.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => employee.Info;
            set
            {
                employee.Info = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChoosePhotoCommand { get; private set; }
        public ICommand RequestCommand { get; private set; }

        public BitmapImage Photo
        {
            set => OnPropertyChanged();
            get
            {
                BitmapImage image = new BitmapImage();
                try
                {
                    employee.getBytePhoto();
                }
                catch (Exception e)
                {
                    return image;
                }
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
