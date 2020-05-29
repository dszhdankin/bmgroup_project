using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using JetBrains.Annotations;
using Version_1._0.Model;

namespace Version_1._0.ViewModel.ControlVm
{
    class GroupButtonVm : INotifyPropertyChanged
    {
        private Class correspondingClass;

        public GroupButtonVm(Class correspondingClass, ICommand command)
        {
            ChangeClassCommand = command;
            this.correspondingClass = correspondingClass;
        }

        public string Title
        {
            get => correspondingClass.Title;
        }

        public ICommand ChangeClassCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
