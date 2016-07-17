using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace freeampcontrols.Controls.VolumIndicator
{
    public class Volumeter : DependencyObject
    {
        private double _value;
        /// <summary>
        /// 
        /// </summary>
        public double Max { get; set; } = 10;
        /// <summary>
        /// 
        /// </summary>
        public double Min { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public double Value
        {
            get { return _value; }
            set
            {
                if (value < Min && value > Max)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _value = value;
            }
        }

    }
}
