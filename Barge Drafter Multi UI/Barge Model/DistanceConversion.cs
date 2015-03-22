using System;

namespace MDG.Model
{
    public class DistanceConversion
    {
        public int Feet { get; set; }

        public int Inches { get; set; }

        public DistanceConversion(double measurement)
        {
            Feet = ((int) measurement/12);
            double remainder = measurement - (Feet * 12);
            Inches = (int)remainder;
            if (Inches >= 12)
            {
                Feet++;
                Inches = 0;
            }
        }
    }
}