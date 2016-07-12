using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using freeampcorelib;
using Microsoft.Win32;
using NAudio.CoreAudioApi;

namespace FreeAmp
{
    /// <summary>
    ///     Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TrackList tl = new TrackList();
        private readonly DispatcherTimer timer;
        private List<MMDevice> devices; 

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
            sp.TrackLoaded += Sp_TrackLoaded1;
            timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(16)};
            timer.Tick += Timer_Tick;
        }

        public SoundPlayer sp { get; set; }


        private void Sp_TrackLoaded1(object sender, EventArgs e)
        {
            Debug.WriteLine("Track loaded");
            Debug.WriteLine(ind.Value);
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
           
            ind.Value = sp.PicVolume*0.8d;

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            sp.Load(tl.GetCurrentTrack());
            timer.Start();
            sp.Play();
          
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

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sp.DeviceWaveOut.Volume = (float)((Slider) sender).Value;
        }
    }
}