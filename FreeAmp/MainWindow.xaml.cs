using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection.Emit;
using System.Timers;
using System.Windows;
using System.Windows.Data;

using System.Windows.Input;
using System.Windows.Threading;
using FreeAmp.Core;
using Microsoft.Win32;
using NAudio.CoreAudioApi;
using NAudio.Utils;
using NAudio.Wave;
using Un4seen.Bass;
using freeampcorelib;
using TrackList = freeampcorelib.TrackList;

namespace FreeAmp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public SoundPlayer sp { get; set; } = null;
        private freeampcorelib.TrackList tl = new TrackList();
        private DispatcherTimer timer;
        
        public MainWindow()
        {
            InitializeComponent();

            #region Command implementation

            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, this.OnMaximizeWindow,
                this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow,
                this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow,
                this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, this.OnSystemMenuShow,
                null));

            #endregion

            
            sp = new SoundPlayer();
            sp.TrackLoaded += Sp_TrackLoaded;
            timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(1000)};
            //timer.Tick += Timer_Tick;
            //_trackList.AppendTrack(
            //    new Track(@"E:\23_justin_timberlake_cant_stop_the_feeling_myzuka.fm.mp3"));
           
        }

       

        private void Sp_TrackLoaded(object sender, EventArgs e)
        {
            slider.Maximum = sp.TrackTotalTime;
          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            slider.Value = sp.CurrentTrackTime;
        }

       

        #region CommandWindow


        private void OnSystemMenuShow(object target, ExecutedRoutedEventArgs e)
        {
            SystemCommands.ShowSystemMenu(this, new Point(10, 10));
        }

        private void OnCanResizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode == ResizeMode.CanResize || this.ResizeMode == ResizeMode.CanResizeWithGrip;
        }

        private void OnCanMinimizeWindow(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.ResizeMode != ResizeMode.NoResize;
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Загрузите данные, установив свойство CollectionViewSource.Source:
            // trackListViewSource.Source = [универсальный источник данных]
        }




        private void button_Click(object sender, RoutedEventArgs e)
        {
           
           
            //sp.Load(_trackList.GetCurrentTrack());
            //if(!timer.IsEnabled) timer.Start();
            //sp.Play();
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
             
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.ShowDialog(this);
            tl.Items.Add(new freeampcorelib.Track(ofd.FileName));
           

        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sp.CurrentTrackTime = slider.Value;
        }
    }
}