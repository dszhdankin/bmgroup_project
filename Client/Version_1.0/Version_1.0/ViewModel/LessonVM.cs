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

namespace Version_1._0.ViewModel
{
    class LessonVM : INotifyPropertyChanged
    {
        private ObservableCollection<Lesson> lessonList = null;

        public string DayOfWeek { get; private set; }

        public ObservableCollection<Lesson> LessonList
        {
            get => lessonList;
        }

        public LessonVM(string dayOfWeek, bool slave)
        {
            DayOfWeek = dayOfWeek;
            string name = "master", cab = "cab";
            if (slave)
                name = "slave";
            lessonList = new ObservableCollection<Lesson>();
            for (int i = 0; i < 5; i++)
            {
                Lesson lesson = new Lesson(name, cab, new DateTime());
                lessonList.Add(lesson);
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
