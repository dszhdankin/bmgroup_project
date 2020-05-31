using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;
using Version_1._0.View.Dialogs;
using Version_1._0.ViewModel.WindowVm;

namespace Version_1._0.ViewModel.ControlVm
{
    class EventButtonVm : INotifyPropertyChanged
    {
        private Event eventInfo;
        private EventButton eventButton;

        public EventButtonVm(Event eventInfo, EventButton eventButton)
        {
            this.eventButton = eventButton;
            this.eventInfo = eventInfo;
            EditEventCommand = new RelayCommand(obj =>
            {
                EditEvent putEvent = new EditEvent();
                EditEventVM putEventVm = new EditEventVM("PUT", eventInfo);
                putEvent.DataContext = putEventVm;
                putEvent.ShowDialog();
            });
            DeleteEventCommand = new RelayCommand(obj =>
            {
                Action<Event> eventDeleter = new Action<Event>((curEvent) =>
                {
                    try
                    {
                        ModelGet<Event>.delete(App.SERVER_NAME, curEvent.EventId);
                        App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                            new Action<string>(message => { MessageBox.Show(message); }), "Event was successfully deleted!");
                    }
                    catch (Exception e)
                    {
                        App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                            new Action<string>(message => { MessageBox.Show(message); }), e.Message);
                    }
                });
                eventDeleter.BeginInvoke(eventInfo, null, null);
            });
            this.OpenMenuCommand = new RelayCommand(obj =>
            { 
                //this.eventButton.Button.ContextMenu.IsOpen = true;
                //this.eventButton.Button.ContextMenu.Focus();
                //this.eventButton.Button.ContextMenu.
            });
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

        public BitmapImage BackgroundImage
        {
            get
            {
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(eventInfo.getBytePhoto()))
                {
                    image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                }
                return image;
            }
        }

        public ICommand OpenMenuCommand { get; set; }
        public ICommand DeleteEventCommand { get; private set; }
        public ICommand EditEventCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
