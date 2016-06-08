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
    /// Логика взаимодействия для MainMenuButton.xaml
    /// </summary>
    public partial class MainMenuButton : UserControl
    {
       
        public MainMenuButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IconCaptionProperty = DependencyProperty.Register(
            "IconCaption", typeof (Image), typeof (MainMenuButton), new PropertyMetadata(default(Image)));

        public Image IconCaption
        {
            get { return (Image) GetValue(IconCaptionProperty); }
            set { SetValue(IconCaptionProperty, value); }
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
