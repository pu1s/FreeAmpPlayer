using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Reflection.Emit;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using FreeAmp.Core;
using Microsoft.Win32;
using NAudio.CoreAudioApi;
using NAudio.Utils;
using NAudio.Wave;


namespace FreeAmp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public SoundPlayer sp { get; set; } = null;
        private TrackList _trackList = new TrackList("");
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
            
            _trackList.AppendTrack(new Track(@"D:\Music\Savage - Greatest Hits & Remixes (2 CD) (2016)\CD2\01-Only You (Remix).mp3"));
            sp.Load(_trackList);
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

            if (sp.PlaybackState == PlaybackState.Playing) return;
            sp.Play();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}