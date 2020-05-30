using System.Windows;
using Version_1._0.View.Pages;

namespace Version_1._0.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            App.MainNavigationService.Navigate(new MainPage());
            App.UiDispatcher = this.Dispatcher;
        }

    }
}
