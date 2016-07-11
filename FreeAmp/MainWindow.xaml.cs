using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using freeampcorelib;
using FreeAmp.Core;
using Microsoft.Win32;
using Track = freeampcorelib.Track;

namespace FreeAmp
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private readonly TrackList tl = new TrackList();

        public MainWindow()
        {
            InitializeComponent();

            #region Command implementation

            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, OnCloseWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, OnMaximizeWindow,
                OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, OnMinimizeWindow,
                OnCanMinimizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, OnRestoreWindow,
                OnCanResizeWindow));
            CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, OnSystemMenuShow,
                null));

            #endregion

            sp = new SoundPlayer();
            sp.TrackLoaded += Sp_TrackLoaded;
            timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(1000)};
            
        }


        public SoundPlayer sp { get; set; }


        private void Sp_TrackLoaded(object sender, EventArgs e)
        {
            //slider.Maximum = sp.TrackTotalTime;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //slider.Value = sp.CurrentTrackTime;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // trackListViewSource.Source = [универсальный источник данных]
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.ShowDialog(this);
            tl.Items.Add(new Track(ofd.FileName));
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //sp.CurrentTrackTime = slider.Value;
        }

        #region CommandWindow

        private void OnSystemMenuShow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.ShowSystemMenu(this, new Point(10, 10));
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode == ResizeMode.CanResize || ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = ResizeMode != ResizeMode.NoResize;
        }

        private void OnCloseWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
        }

        private void OnMaximizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void OnMinimizeWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void OnRestoreWindow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
        }

        #endregion

        private void slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ind.Value = ((Slider) sender).Value;
            
        }
    }
}