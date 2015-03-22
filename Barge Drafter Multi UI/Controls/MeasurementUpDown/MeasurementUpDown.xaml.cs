using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using UserControl = System.Windows.Controls.UserControl;

namespace MDG.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MearsurementUpDown : UserControl
    {
        public MearsurementUpDown()
        {
            InitializeComponent();
            DataContext = this;
        }

        #region Properties and Fields

        private int _feet;
        public int Feet
        {
            get { return _feet; }
            set
            {
                _feet = value;
                SetValue(ValueProperty,(_feet + _inches/12));
                OnPropertyChanged("Feet");
            }
        }

        private int _inches;
        public int Inches
        {
            get { return _inches; }
            set
            {
                _inches = value;
                    SetValue(ValueProperty,(_feet + _inches / 12));
                OnPropertyChanged("Inches");
            }
        }

        //public static readonly DependencyProperty ValueProperty =
        //    DependencyProperty.Register("Value", typeof(double), typeof(MearsurementUpDown), new UIPropertyMetadata(0.0, MyDepPropertyChangedHandler));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (double), typeof (MearsurementUpDown),
                                        new FrameworkPropertyMetadata(0.0, MyDepPropertyChangedHandler, CoerceValue));

        public double Value
        {
            get
            {
                return (double) GetValue(ValueProperty);
                //return Feet + Inches/12;
            }
            set { 
                SetValue(ValueProperty, value);
            }
        }

        public static void MyDepPropertyChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                int x = 0;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
           
        }

        private static object CoerceValue(DependencyObject element, object value)
        {
            MearsurementUpDown ctrl = (MearsurementUpDown) element;
            double newValue = Convert.ToDouble(value);
            int feet = Convert.ToInt32(Math.Truncate(newValue));
            ctrl.numFeet.Value = feet;
            int inches = Convert.ToInt32((newValue * 10) - (feet * 10));
            ctrl.numInches.Value = inches;
            
            return value;
        }

        //public double Value {
        //    get
        //    {
        //        return Feet + Inches / 12;
        //    }
        //    set
        //    {
        //        
        //OnPropertyChanged("Value");
        //    }

        //}

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

       

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        private void numFeet_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Decimal)
            {
                e.Handled = true;
                numInches.Focus();
            }
        }
    }
}
