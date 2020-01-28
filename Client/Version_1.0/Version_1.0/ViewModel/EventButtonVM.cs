using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel
{
    class EventButtonVM : INotifyPropertyChanged
    {
        private EventInfo eventInfo;

        public EventButtonVM(EventInfo eventInfo)
        {
            this.eventInfo = eventInfo;
        }

        public string Description
        {
            get => eventInfo.Discription;
        }

        public string Date
        {
            get => eventInfo.Date;
        }

        public string Name
        {
            get => eventInfo.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
