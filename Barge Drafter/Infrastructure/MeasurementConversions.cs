using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDG.Infrastructure.Conversions
{
    public static class MeasurementConversions
    {
        public static string ToFeetAndInches( this double value)
        {
            if(value <= 0)
                return string.Format ( "{0}' {1}\"", 0, 0.ToString ( "N2" ) );

            int feet = (int)value / 12;
            double inches = (double)(value % 12);

            if ((int)inches == 12)
            {
                feet++;
                inches = 0;
            }
            return string.Format("{0}' {1}\"", feet, inches.ToString("N2"));
        }

        public static int GetFeet(double inches)
        {
            double feet = (inches / 12);
            return (int)feet;
        }

        public static int GetIches(double inches)
        {
            double feet = (int)(inches / 12);
            double remaining = inches - (feet * 12);
            return (int)remaining;
        }
    }

}


