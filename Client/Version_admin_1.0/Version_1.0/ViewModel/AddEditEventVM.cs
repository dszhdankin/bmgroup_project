﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Microsoft.Win32;
using Version_1._0.Model;
using Version_1._0.Utilities;

namespace Version_1._0.ViewModel
{
    class AddEditEventVM : INotifyPropertyChanged
    {
        private Event eventInfo;

        public String Title { get; private set; }

        public String Name
        {
            get => eventInfo.Title;
            set
            {
                eventInfo.Title = value;
                OnPropertyChanged();
            }
        }

        public String Description
        {
            get => eventInfo.Description;
            set
            {
                eventInfo.Description = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage Photo
        {
            set => OnPropertyChanged();
            
            get
            {
                BitmapImage brush = new BitmapImage();
                try
                {
                    eventInfo.getBytePhoto();
                }
                catch (Exception e)
                {
                    return brush;
                }
                using (MemoryStream stream = new MemoryStream(eventInfo.getBytePhoto()))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    image.Freeze();
                    brush = image;
                }
                return brush;
            }
        }

        private void SendData(object parameter)
        {
            MessageBox.Show(Name);
            MessageBox.Show(Description);
        }

        private void ChoosePhoto(object parameter)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                string filePath = fileDialog.FileName;
                eventInfo.Photo = Encoding.Default.GetString(File.ReadAllBytes(filePath));
                Photo = null;
            }
        }

        public DateTime StartDate
        {
            get => eventInfo.StartTime;
            set
            {
                eventInfo.StartTime = value;
                OnPropertyChanged();
            }
        }

        public ICommand RequestCommand { get; set; }
        public ICommand ChoosePhotoCommand { get; set; }

        public AddEditEventVM()
        {
            Title = "New event";
            eventInfo = new Event();
            eventInfo.Title = "My event.";
            eventInfo.StartTime = new DateTime(2020, 5, 30);
            eventInfo.Description = "This is some cool description.";
            OpenFileDialog fileDialog = new OpenFileDialog();
            RequestCommand = new RelayCommand(SendData);
            ChoosePhotoCommand = new RelayCommand(ChoosePhoto);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
