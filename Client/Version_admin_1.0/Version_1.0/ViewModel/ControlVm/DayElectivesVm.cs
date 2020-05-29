using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class DayElectivesVm : INotifyPropertyChanged
    {
        private string dayOfWeek;
        private DateTime date;
        private ObservableCollection<Elective> electives;

        public string DayOfWeek
        {
            get => dayOfWeek;
            set
            {
                dayOfWeek = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Elective> Electives
        {
            get => electives;
            set
            {
                electives = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged();
            }
        }

        public DayElectivesVm(string dayOfWeek)
        {
            Electives = new ObservableCollection<Elective>();
            DayOfWeek = dayOfWeek;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
