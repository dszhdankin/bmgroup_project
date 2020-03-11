using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Version_1._0.View.Controls;
using Version_1._0.View.Pages;

namespace Version_1._0.ViewModel
{
    class SchedulePageVM : INotifyPropertyChanged
    {
        private ObservableCollection<SchoolScheduleVM> _schoolScheduleVms;
        private ObservableCollection<GroupButton> _groupButtons;
        private SchedulePage schedulePage;

        public SchedulePageVM(SchedulePage schedulePage)
        {
            this.schedulePage = schedulePage;
            _schoolScheduleVms = new ObservableCollection<SchoolScheduleVM>();
            _groupButtons = new ObservableCollection<GroupButton>();
            SchoolScheduleVM masterVm = new SchoolScheduleVM("Master school", false, schedulePage.Monday,
                schedulePage.Tuesday, schedulePage.Wednesday, schedulePage.Thursday, schedulePage.Friday,
                schedulePage.Saturday);
            GroupButton masterButton = new GroupButton();
            masterButton.DataContext = masterVm;
            SchoolScheduleVM slaveVm = new SchoolScheduleVM("Slave school", true, schedulePage.Monday,
                schedulePage.Tuesday, schedulePage.Wednesday, schedulePage.Thursday, schedulePage.Friday,
                schedulePage.Saturday);
            GroupButton slaveButton = new GroupButton();
            slaveButton.DataContext = slaveVm;
            _schoolScheduleVms.Add(masterVm);
            _schoolScheduleVms.Add(slaveVm);
            _groupButtons.Add(masterButton);
            _groupButtons.Add(slaveButton);
        }

        public ObservableCollection<GroupButton> GroupButtons
        {
            get => _groupButtons;
            set
            {
                _groupButtons = value;
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
