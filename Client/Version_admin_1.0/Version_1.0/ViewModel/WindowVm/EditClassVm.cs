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

namespace Version_1._0.ViewModel.WindowVm
{
    class EditClassVm : INotifyPropertyChanged
    {
        private Class curClass;

        private void PostCommand(object parameter)
        {
            Action<Class> classPoster = new Action<Class>((postClass) =>
            {
                try
                {
                    ModelGet<Class>.post(App.SERVER_NAME, postClass);
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(message =>
                        MessageBox.Show(message)), "Class successfully added.");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(message =>
                        MessageBox.Show(message)), e.Message);
                }
            });
            Class newClass = new Class();
            newClass.ClassId = this.curClass.ClassId;
            newClass.Title = this.ClassName;
            classPoster.BeginInvoke(newClass, null, null);
        }

        private void PutCommand(object parameter)
        {
            Action<Class> classPuter = new Action<Class>((putClass) =>
            {
                try
                {
                    ModelGet<Class>.put(App.SERVER_NAME, putClass, putClass.ClassId);
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(message =>
                        MessageBox.Show(message)), "Class successfully added.");
                }
                catch (Exception e)
                {
                    App.UiDispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<string>(message =>
                        MessageBox.Show(message)), e.Message);
                }
            });
            Class newClass = new Class();
            newClass.ClassId = this.curClass.ClassId;
            newClass.Title = this.ClassName;
            classPuter.BeginInvoke(newClass, null, null);
        }

        public string ClassName
        {
            get => curClass.Title;
            set
            {
                curClass.Title = value;
                OnPropertyChanged();
            }
        }

        public EditClassVm(string requestType = "POST", Class curClass = null)
        {
            if (curClass == null)
            {
                this.curClass = new Class();
                this.curClass.Title = "Class";
            }
            else
            {
                this.curClass = new Class();
                this.curClass.ClassId = curClass.ClassId;
                this.curClass.Title = curClass.Title;
            }

            if (requestType.Equals("POST"))
                RequestCommand = new RelayCommand(PostCommand);
            else
                RequestCommand = new RelayCommand(PutCommand);
        }

        public ICommand RequestCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
