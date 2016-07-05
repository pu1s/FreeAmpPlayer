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

namespace freeampcontrols
{
    /// <summary>
    /// Логика взаимодействия для freeampTrackBarControl.xaml
    /// </summary>
    public partial class freeampTrackBarControl : UserControl
    {
        public freeampTrackBarControl()
        {
            InitializeComponent();
        }




        public int MyProperty
        {
            get { return (int)GetValue(MyPropertyProperty); }
            set {
                SetValue(MyPropertyProperty, value);
                }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(int), typeof(freeampTrackBarControl), new PropertyMetadata(0));




    }
}
