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

namespace freeampcontrols.Controls.SegmentIndicatorControl
{
    /// <summary>
    /// Логика взаимодействия для freeamp_SegmentIndicator.xaml
    /// </summary>
    public partial class freeamp_SegmentIndicator : UserControl
    {
        public freeamp_SegmentIndicator()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty SignalProperty = DependencyProperty.Register(
            "Signal", typeof(bool), typeof(freeamp_SegmentIndicator), new PropertyMetadata(OnSignalChange));

        public static readonly DependencyProperty SegmentStateProperty = DependencyProperty.Register(
            "SegmentState", typeof(bool), typeof(freeamp_SegmentIndicator),
            new PropertyMetadata(OnSegmentStateChanged));


        public bool SegmentState
        {
            get { return (bool)GetValue(SegmentStateProperty); }
            set { SetValue(SegmentStateProperty, value); }
        }

        public bool Signal
        {
            get { return (bool)GetValue(SignalProperty); }
            set { SetValue(SignalProperty, value); }
        }

        public bool IsSignal { get; set; }
        public bool IsSegmentState { get; set; } = false;

        private static void OnSignalChange(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ((freeamp_SegmentIndicator)dependencyObject).IsSignal = (bool)args.NewValue;
        }

        private static void OnSegmentStateChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs args)
        {
            ((freeamp_SegmentIndicator)dependencyObject).SegmentState = (bool)args.NewValue;
        }
    }
}
