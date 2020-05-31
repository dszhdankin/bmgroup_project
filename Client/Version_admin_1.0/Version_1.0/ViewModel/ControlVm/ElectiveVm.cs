using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class ElectiveVm : INotifyPropertyChanged
    {
        private Elective elective;

        public Elective Elective
        {
            get => elective;
        }

        public ElectiveVm(Elective elective)
        {
            this.elective = elective;
        }

        public string Title
        {
            get => elective.Title;
            set
            {
                elective.Title = value;
                OnPropertyChanged();
            }
        }

        public string Info
        {
            get => elective.Info;
            set
            {
                elective.Info = value;
                OnPropertyChanged();
            }
        }

        public string Time
        {
            get
            {
                DateTime time = elective.Time;
                return (time.Hour / 10).ToString() + time.Hour % 10 + ":"
                       + time.Minute / 10 + time.Minute % 10;
            }
            set
            {
                string str = value;
                try
                {
                    int hour = int.Parse(str.Substring(0, 2));
                    int minute = int.Parse(str.Substring(3, 2));
                    elective.Time = new DateTime(elective.Time.Year, elective.Time.Month, elective.Time.Day, hour, minute, 0);
                    OnPropertyChanged();
                }
                catch (Exception e)
                {

                }
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
