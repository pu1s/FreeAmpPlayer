using System.Windows;
using System.Windows.Controls;

namespace freeampcontrols.Controls.SegmentIndicatorControl
{
    /// <summary>
    ///     Логика взаимодействия для freeamp_SegmentIndicator.xaml
    /// </summary>
    public partial class freeamp_SegmentIndicator : UserControl
    {
        public static readonly DependencyProperty SignalProperty = DependencyProperty.Register(
            "Signal", typeof (bool), typeof (freeamp_SegmentIndicator), new PropertyMetadata(OnSignalChange));

        public static readonly DependencyProperty SegmentStateProperty = DependencyProperty.Register(
            "SegmentState", typeof (bool), typeof (freeamp_SegmentIndicator),
            new PropertyMetadata(OnSegmentStateChanged));

        public freeamp_SegmentIndicator()
        {
            InitializeComponent();
        }


        public bool SegmentState
        {
            get { return (bool) GetValue(SegmentStateProperty); }
            set { SetValue(SegmentStateProperty, value); }
        }

        public bool Signal
        {
            get { return (bool) GetValue(SignalProperty); }
            set { SetValue(SignalProperty, value); }
        }

        public bool IsSignal { get; set; }
        public bool IsSegmentState { get; set; } = false;

        private static void OnSignalChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ((freeamp_SegmentIndicator) dependencyObject).IsSignal = (bool) args.NewValue;
        }

        private static void OnSegmentStateChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            ((freeamp_SegmentIndicator) dependencyObject).SegmentState = (bool) args.NewValue;
        }
    }
}