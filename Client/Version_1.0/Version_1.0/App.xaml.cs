﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Version_1._0
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string SERVER_NAME = "https://localhost:44374/";

        public static NavigationService MainNavigationService =>
            (App.Current.MainWindow as MainWindow).Frame.NavigationService;

        public static Dispatcher UiDispatcher;
    }
}
