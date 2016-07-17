using System;
using System.Windows;
using System.Windows.Controls;

namespace freeampcontrols.Controls.MinVolIndicator
{
    /// <summary>
    ///     Логика взаимодействия для MinVolIndicator.xaml
    /// </summary>
    public partial class MinVolIndicator : UserControl
    {
        public static readonly DependencyProperty ValueProperty;

        static MinVolIndicator()
        {
            ValueProperty = DependencyProperty.Register(
                "Value", typeof (double), typeof (MinVolIndicator), new FrameworkPropertyMetadata(OnValueChanged), ValidateValueCallback);
        }

        private static bool ValidateValueCallback(object value)
        {
            return !((double)value< 0) || !((double) value >1);
        }

        public MinVolIndicator()
        {
            InitializeComponent();
        }

        public double X { get; set; }
        /// <summary>
        /// Свойство зависимостей
        /// </summary>
        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var obj = (MinVolIndicator) dependencyObject;
            var arg = (double) args.NewValue;
            obj.Value = arg;
            obj.ScaleTransform.ScaleX = 1 - arg;
        }

       
    }
}