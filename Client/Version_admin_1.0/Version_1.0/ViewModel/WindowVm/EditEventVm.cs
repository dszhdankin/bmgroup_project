using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Web.UI;
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
    class EditEventVM : INotifyPropertyChanged
    {
        private Event eventInfo;

        public String Title { get; private set; }

        public String Name
        {
            get => eventInfo.Title;
            set
            {
                eventInfo.Title = value;
                OnPropertyChanged();
            }
        }

        public String Description
        {
            get => eventInfo.Description;
            set
            {
                eventInfo.Description = value;
                OnPropertyChanged();
            }
        }

        private void PostEvent(object parameter)
        {
            Action<Event> dataPoster = new Action<Event>(curEvent =>
            {
                try
                {
                    ModelGet<Event>.post(App.SERVER_NAME, curEvent);
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<object>(sender => MessageBox.Show("Event was successfully created!")), this);
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, 
                        new Action<string>(message => MessageBox.Show(message)), e.Message);
                }
            });
            dataPoster.BeginInvoke(eventInfo, null, null);
        }

        private void PutEvent(object parameter)
        {
            Action<Event> dataPuter = new Action<Event>(curEvent =>
            {
                try
                {
                    // TODO
                    // Make a PUT request for curEvent
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => MessageBox.Show(message)), "Event was successfully edited!");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => MessageBox.Show(message)), e.Message);
                }
            });
            dataPuter.BeginInvoke(eventInfo, null, null);
        }

        public BitmapImage Photo
        {
            set => OnPropertyChanged();
            
            get
            {
                BitmapImage brush = new BitmapImage();
                try
                {
                    eventInfo.getBytePhoto();
                }
                catch (Exception e)
                {
                    return brush;
                }
                using (MemoryStream stream = new MemoryStream(eventInfo.getBytePhoto()))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                    brush = image;
                }
                return brush;
            }
        }


        private void ChoosePhoto(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                string filePath = fileDialog.FileName;
                eventInfo.Photo = Convert.ToBase64String(File.ReadAllBytes(filePath));
                Photo = null;
            }
        }

        public DateTime StartDate
        {
            get => eventInfo.StartTime;
            set
            {
                eventInfo.StartTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand RequestCommand { get; set; }
        public ICommand ChoosePhotoCommand { get; set; }

        public EditEventVM(string requestType = "POST", Event curEvent = null)
        {
            Title = "New event";
            if (curEvent == null)
            {
                eventInfo = new Event();
                eventInfo.Title = "My event.";
                eventInfo.StartTime = new DateTime(2020, 5, 30);
                eventInfo.Description = "This is some cool description.";
            }
            else
            {
                eventInfo = new Event();
                eventInfo.Description = curEvent.Description;
                eventInfo.Photo = curEvent.Photo;
                eventInfo.Title = curEvent.Title;
                eventInfo.EventId = curEvent.EventId;
                eventInfo.StartTime = curEvent.StartTime;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (requestType.Equals("POST"))
                RequestCommand = new RelayCommand(PostEvent);
            else if (requestType.Equals("PUT"))
                RequestCommand = new RelayCommand(PutEvent);
            ChoosePhotoCommand = new RelayCommand(ChoosePhoto);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
