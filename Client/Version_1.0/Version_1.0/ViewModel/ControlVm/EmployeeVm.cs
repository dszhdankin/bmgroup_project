using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class EmployeeVm : INotifyPropertyChanged
    {
        private Employee employee;

        public EmployeeVm(Employee employee)
        {
            this.employee = employee;
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
