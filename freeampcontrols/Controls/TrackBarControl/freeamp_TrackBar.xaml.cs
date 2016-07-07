using System.Windows;
using System.Windows.Controls;

namespace freeampcontrols.Controls.TrackBarControl
{
    /// <summary>
    /// Логика взаимодействия для freeamp_TrackBar.xaml
    /// </summary>
    public partial class freeamp_TrackBar : UserControl
    {
        public freeamp_TrackBar()
        {
            InitializeComponent();
            this.slider.IsMoveToPointEnabled = true;
        }

       

        public double Maximum => slider.Maximum;

        public static readonly DependencyProperty ValProperty = DependencyProperty.Register(
            "Val", typeof (double), typeof (freeamp_TrackBar), new PropertyMetadata(default(double)));

        public double Val
        {
            get { return (double) GetValue(ValProperty); }
            set
            {
                SetValue(ValProperty, value);
         
            }
        }

        public double Value
        {
            get { return slider.Value; }
            set { slider.Value = value; }
        }
    }

    
}