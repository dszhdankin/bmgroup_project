using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using JetBrains.Annotations;
using Version_1._0.Model;
using Version_1._0.Utilities;
using Version_1._0.View.Controls;
using Version_1._0.View.Dialogs;
using Version_1._0.ViewModel.WindowVm;

namespace Version_1._0.ViewModel.ControlVm
{
    class GroupButtonVm : INotifyPropertyChanged
    {
        private Class correspondingClass;
        private GroupButton groupButton;

        public GroupButton GroupButton
        {
            get => groupButton;
            set
            {
                groupButton = value;
                OnPropertyChanged();
            }
        }

        private void DeleteClass(object parameter)
        {
            Action<Class> classDeleter = new Action<Class>((curClass) =>
            {
                try
                {
                    ModelGet<Class>.delete(App.SERVER_NAME, curClass.ClassId);
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => { MessageBox.Show(message); }), "Class was successfully deleted!");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal,
                        new Action<string>(message => { MessageBox.Show(message); }), e.Message);
                }
            });
            classDeleter.BeginInvoke(correspondingClass, null, null);
        }

        private void OpenMenu(object parameter)
        {
            this.groupButton.Button.ContextMenu.IsOpen = true;
        }

        public GroupButtonVm(Class correspondingClass, ICommand command, GroupButton groupButton)
        {
            ChangeClassCommand = command;
            this.groupButton = groupButton;
            this.correspondingClass = new Class();
            this.correspondingClass.Title = correspondingClass.Title;
            this.correspondingClass.ClassId = correspondingClass.ClassId;
            PutClassCommand = new RelayCommand(obj =>
            {
                EditClass editClass = new EditClass();
                EditClassVm editClassVm = new EditClassVm("PUT", correspondingClass);
                editClass.DataContext = editClassVm;
                editClass.ShowDialog();
            });
            DeleteClassCommand = new RelayCommand(DeleteClass);
            OpenMenuCommand = new RelayCommand(OpenMenu);
        }

        public string Title
        {
            get => correspondingClass.Title;
        }

        public ICommand OpenMenuCommand { get; private set; }
        public ICommand ChangeClassCommand { get; private set; }
        public ICommand PutClassCommand { get; private set; }
        public ICommand DeleteClassCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
