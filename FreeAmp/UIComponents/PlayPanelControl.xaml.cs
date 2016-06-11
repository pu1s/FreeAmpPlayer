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

namespace FreeAmp.UIComponents
{
    /// <summary>
    /// Логика взаимодействия для PlayPanelControl.xaml
    /// </summary>
    public partial class PlayPanelControl : UserControl
    {
        public PlayPanelControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty AlbumIconResourceProperty = DependencyProperty.Register(
            "AlbumIconResource", typeof (string), typeof (PlayPanelControl), new PropertyMetadata(default(string)));

        public string AlbumIconResource
        {
            get { return (string) GetValue(AlbumIconResourceProperty); }
            set { SetValue(AlbumIconResourceProperty, value); }
        }
        public bool IsPlaying { get; set; }
    }
    
}
