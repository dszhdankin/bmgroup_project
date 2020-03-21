using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel
{
    class EmployeeVM : INotifyPropertyChanged
    {
        private Employee employee;

        public EmployeeVM(Employee employee)
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

        public ImageBrush Photo
        {
            get
            {
                ImageBrush brush = new ImageBrush();
                using (MemoryStream stream = new MemoryStream(employee.getBytePhoto()))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                    brush.ImageSource = image;
                }
                return brush;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
