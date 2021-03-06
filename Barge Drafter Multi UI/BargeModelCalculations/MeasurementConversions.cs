﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDG.Conversions
{
    public static class MeasurementConversions
    {
        public static string ToFeetAndInches( this double value)
        {
            int feet = (int)value / 12;
            double inches = (double)(value % 12);

            return string.Format("{0}' {1}\"", feet, inches.ToString("N2"));
        }
    }
}


