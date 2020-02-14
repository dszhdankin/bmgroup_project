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

namespace Version_1._0.ViewModel
{
    class EmployeesPageVM : INotifyPropertyChanged
    {
        private List<EmployeeVM> employeeVms;
        private ObservableCollection<EmployeeButton> employees;

        public String EmployeesTitle { get; private set; }
        public String SchoolOrUniName { get; private set; }

        public EmployeesPageVM()
        {
            employees = new ObservableCollection<EmployeeButton>();
            employeeVms = new List<EmployeeVM>();
            EmployeesTitle = "Сотрудники";
            SchoolOrUniName = "Название учреждения";
            for (int i = 0; i < 10; i++)
            {
                employeeVms.Add(new EmployeeVM("Имя", "Должность", "Описание"));
                var button = new EmployeeButton();
                button.DataContext = employeeVms[i];
                employees.Add(button);
            }
        }

        public ObservableCollection<EmployeeButton> EmployeeButtons
        {
            get => employees;
            set
            {
                employees = value;
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

