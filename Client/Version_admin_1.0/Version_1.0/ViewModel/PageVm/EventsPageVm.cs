using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
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
    class EventsPageVm : INotifyPropertyChanged
    {
        //Just a delegate to call methods asynchronously
        private delegate void AsyncCaller();

        private delegate void UpdateUICaller(ObservableCollection<Event> argument);

        private ObservableCollection<EventButtonVm> eventButtonViewModels = null;
        private ObservableCollection<EventButton> eventButtons = null;
        private ObservableCollection<Event> eventInfos = null;
        private ModelGet<Event> modelGet = null;
        private Timer timer;

        private void FetchServerData()
        {
            ObservableCollection<Event> buffer = modelGet.get(App.SERVER_NAME);
            App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new UpdateUICaller(UpdateUI), buffer);
        }

        private void UpdateUI(ObservableCollection<Event> buffer)
        {
            if (buffer == null)
                eventInfos = new ObservableCollection<Event>();
            else
                eventInfos = buffer;

            eventButtonViewModels.Clear();
            eventButtons.Clear();
            foreach (var curEventInfo in eventInfos)
            {
                EventButton eventButton = new EventButton();
                EventButtonVm eventButtonVm = new EventButtonVm(curEventInfo, eventButton);
                
                eventButton.DataContext = eventButtonVm;
                eventButtonViewModels.Add(eventButtonVm);
                eventButtons.Add(eventButton);
            }
        }

        private void Update(object parameter)
        {
            AsyncCaller fetcher = new AsyncCaller(FetchServerData);
            fetcher.BeginInvoke(null, null);
        }

        private void AddEventDialog(object parameter)
        {
            EditEvent addEventView = new EditEvent();
            EditEventVM addEventVm = new EditEventVM();
            addEventView.DataContext = addEventVm;  
            addEventView.ShowDialog();
        }

        public String EventsTitle { get; private set; }
        public String SchoolOrUniName { get; private set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand AddEventCommand { get; set; }

        public EventsPageVm()
        {
            eventButtons = new ObservableCollection<EventButton>();
            eventButtonViewModels = new ObservableCollection<EventButtonVm>();
            EventsTitle = "Мероприятия";
            SchoolOrUniName = "Название учреждения";
            modelGet = new ModelGet<Event>();
            eventInfos = new ObservableCollection<Event>();
            UpdateCommand = new RelayCommand(Update);
            AddEventCommand = new RelayCommand(AddEventDialog);
        }

        public ObservableCollection<EventButton> EventButtons
        {
            get => eventButtons;
            set
            {
                eventButtons = value;
                OnPropertyChanged();
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
