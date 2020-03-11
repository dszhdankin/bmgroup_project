using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.View.Controls;

namespace Version_1._0.ViewModel
{
    class EventsPageVM : INotifyPropertyChanged
    {
        private ObservableCollection<EventButtonVM> eventButtonViewModels = null;
        private ObservableCollection<EventButton> eventButtons = null;
        private ObservableCollection<EventInfo> eventInfos = null;
        private ModelEvent modelEvent = null;

        public String EventsTitle { get; private set; }
        public String SchoolOrUniName { get; private set; }

        public EventsPageVM()
        {
            eventButtons = new ObservableCollection<EventButton>();
            eventButtonViewModels = new ObservableCollection<EventButtonVM>();
            EventsTitle = "Мероприятия";
            SchoolOrUniName = "Название учреждения";
            modelEvent = new ModelEvent();
            eventInfos = modelEvent.get("http://localhost:8080/");
            if (eventInfos == null)
                return;
            foreach (var curEventInfo in eventInfos)
            {
                EventButtonVM eventButtonVm = new EventButtonVM(curEventInfo);
                EventButton eventButton = new EventButton();
                eventButton.DataContext = eventButtonVm;
                eventButtonViewModels.Add(eventButtonVm);
                eventButtons.Add(eventButton);
            }
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
