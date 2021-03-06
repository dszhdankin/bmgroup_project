﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Version_1._0.View;

namespace Version_1._0
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string SERVER_NAME = "https://localhost:44374/";

        public const int NO_ID = -1;

        public static NavigationService MainNavigationService =>
            (App.Current.MainWindow as MainWindow).Frame.NavigationService;

        public static Dispatcher UiDispatcher;
    }
}
