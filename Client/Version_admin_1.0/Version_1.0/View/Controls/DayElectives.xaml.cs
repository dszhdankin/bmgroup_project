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
    /// Логика взаимодействия для DayElectives.xaml
    /// </summary>
    public partial class DayElectives : UserControl
    {
        public List<int> SelectedIndices { get; set; }
        public DayElectives()
        {
            InitializeComponent();
            SelectedIndices = new List<int>();
        }

        private void HandleSelectionChanged(object sender, EventArgs e)
        {
            SelectedIndices.Clear();
            foreach (var curItem in ElectivesView.SelectedItems)
            {
                SelectedIndices.Add(ElectivesView.Items.IndexOf(curItem));
            }
        }
    }
}
