using System;
using System.Collections.Generic;
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
    class EventButtonVM : INotifyPropertyChanged
    {
        private Event eventInfo;

        public EventButtonVM(Event eventInfo)
        {
            this.eventInfo = eventInfo;
        }

        public string Description
        {
            get => eventInfo.Description;
        }

        public string Date
        {
            get => eventInfo.StartTime.ToString();
        }

        public string Name
        {
            get => eventInfo.Title;
        }

        public ImageBrush BackgroundImage
        {
            get
            {
                ImageBrush brush = new ImageBrush();
                using (MemoryStream stream = new MemoryStream(eventInfo.getBytePhoto()))
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
