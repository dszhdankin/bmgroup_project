using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Version_1._0.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для DaySchedule.xaml
    /// </summary>
    public partial class DaySchedule : UserControl
    {
        private List<int> selectedInicies;

        private void HandleSelectionChanged(object sender, EventArgs e)
        {
            selectedInicies.Clear();
            foreach (var curItem in lessonsView.SelectedItems)
            {
                selectedInicies.Add(lessonsView.Items.IndexOf(curItem));                
            }
        }

        public List<int> SelectedIndicies
        {
            get => selectedInicies;
        }

        public DaySchedule()
        {
            InitializeComponent();
            selectedInicies = new List<int>();
        }

    }
}
