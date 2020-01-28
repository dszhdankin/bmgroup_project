using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Version_1._0.View.Controls;

namespace Version_1._0.ViewModel
{
    class EventsPageVM : INotifyPropertyChanged
    {
        private List<EventButtonVM> eventButtonViewModels;
        private ObservableCollection<EventButton> eventButtons;

        public String EventsTitle { get; private set; }

        public EventsPageVM()
        {
            eventButtons = new ObservableCollection<EventButton>();
            eventButtonViewModels = new List<EventButtonVM>();
            EventsTitle = "Мероприятия";
            for (int i = 0; i < 10; i++)
            {
                eventButtonViewModels.Add(new EventButtonVM(new Version_1._0.Model.EventInfo("Название мероприятия",
                    "Это типа суперофигенное мероприятие kkkkkkkk\n kkkkkkkk \n kkkkkkkk",
                    new DateTime(2008, 5, 1, 8, 30, 52)
                )));
                var button = new EventButton();
                button.DataContext = eventButtonViewModels[i];
                eventButtons.Add(button);
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
