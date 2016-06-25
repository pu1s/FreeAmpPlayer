using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Input;
using FreeAmp.Core;
using Microsoft.Win32;
using NAudio.CoreAudioApi;


namespace FreeAmp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SPlayer _sp;

        public MainWindow()
        {
            InitializeComponent();
            this.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, this.OnCloseWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, this.OnMaximizeWindow,
                this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, this.OnMinimizeWindow,
                this.OnCanMinimizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, this.OnRestoreWindow,
                this.OnCanResizeWindow));
            this.CommandBindings.Add(new CommandBinding(SystemCommands.ShowSystemMenuCommand, this.OnSystemMenuShow,
                null));

            _sp = new SPlayer();
            _sp.TrackList.AppendTrack(
                new Track(@"D:\Music\Savage - Greatest Hits & Remixes (2 CD) (2016)\CD2\07-Radio (Maxi Version).mp3"));
            _sp.TrackList.AppendTrack(new Track(@"D:\Music\Океан Ельзи - Без меж (2016)\02. Не йди.mp3"));
            _sp.TrackList.AppendTrack(
                new Track(@"D:\Music\Savage - Greatest Hits & Remixes (2 CD) (2016)\CD2\07-Radio (Maxi Version).mp3"));
        }

        public static readonly DependencyProperty ShowMenuItemProperty = DependencyProperty.Register(
            "));ShowMenuItem", typeof (bool), typeof (MainWindow), new PropertyMetadata(default(bool)));

        public bool ShowMenuItem
        {
            get { return (bool) GetValue(ShowMenuItemProperty); }
            set { SetValue(ShowMenuItemProperty, value); }
        }

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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (_sp.IsPlaying)
            {
                button.Content = "Pause";
                _sp.Pause();
                return;
            }
            if (_sp.IsPaused)
            {
                button.Content = "Play";
                _sp.Resume();
                return;
            }
            _sp.Play(_sp.TrackList.GetCurrentTrack());
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _sp.Stop();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _sp.Next();
            label.Content = _sp.TrackList.GetCurrentTrack().Name;
            ;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "(Файлы mp3)|*.mp3"
            };

            var dr = ofd.ShowDialog();
            if (dr != null && dr.Value)
            {
                var files = ofd.FileNames;
                foreach (var file in files)
                {
                    _sp.TrackList.AppendTrack(new Track(file));
                }
            }
        }
    }
}