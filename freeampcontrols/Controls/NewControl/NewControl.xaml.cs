using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace freeampcontrols.Controls.NewControl
{
    /// <summary>
    /// Логика взаимодействия для NewControl.xaml
    /// </summary>
    public partial class NewControl : UserControl
    {
        static NewControl()
        {
            ValueProperty = DependencyProperty.Register("Value", typeof (double), typeof (NewControl),
                new FrameworkPropertyMetadata(OnChangeValue));
        }
        public NewControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ValueProperty;
        
        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value);}
        }

        public string Cont
        {
            get { return (string)label.Content; }
            set { label.Content = value; }
        }

        private static void OnChangeValue(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((NewControl) sender).Value = (double) args.NewValue;
            ((NewControl) sender).Cont = args.NewValue.ToString();
        }
    }
}
