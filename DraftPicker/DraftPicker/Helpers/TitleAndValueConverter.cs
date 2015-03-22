﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace DraftPicker.Helpers
{
    [ValueConversion ( typeof ( string ), typeof ( String ) )]
    public class TitleAndValueConverter : IValueConverter
    {
        public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        {
            return string.Format ( "{0}:{1}", (string)value, (string)parameter );
        }

        public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        {

            return DependencyProperty.UnsetValue;
        }
    }
}
