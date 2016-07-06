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

        public double Value
        {
            get { return slider.Value; }
            set { slider.Value = value; }
        }

        public double Maximum => slider.Maximum;
    }
}