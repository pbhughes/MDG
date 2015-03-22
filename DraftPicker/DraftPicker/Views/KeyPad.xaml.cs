using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DraftPicker.Helpers;
using DraftPicker.ViewModels;

namespace DraftPicker.Views
{
    /// <summary>
    /// Interaction logic for KeyPad.xaml
    /// </summary>
    public partial class KeyPad : UserControl, INotifyPropertyChanged
    {
        public KeyPad()
        {
            InitializeComponent();
            layoutRoot.DataContext = this;
        }

        #region Properties and Fields

        private const string ErrorCaption = "I-DRAFT";
        #endregion

        #region Dependecy Properties

        public static readonly DependencyProperty ValueProperty =
           DependencyProperty.Register ( "Value", typeof ( int ), typeof ( KeyPad ), null );

        public int Value
        {
            get { return (int)GetValue ( ValueProperty ); }
            set
            {
                SetValue ( ValueProperty, value );
                OnPropertyChanged ( "Value" );
            }
        }

        public static readonly DependencyProperty ValueLabelProperty =
            DependencyProperty.Register("ValueLabel", typeof (string), typeof (KeyPad), null);

        public string ValueLabel
        {
            get { return (string) GetValue(ValueLabelProperty); }
            set { SetValue(ValueLabelProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof (int), typeof (KeyPad),null);

        public int MaxValue
        {
            get { return (int) GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof (int), typeof (KeyPad), new PropertyMetadata(default(int)));

        public int MinValue
        {
            get { return (int) GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        #endregion

        #region INotifyPropertyChanged

        public  event PropertyChangedEventHandler PropertyChanged;

        private  void OnPropertyChanged ( string name )
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler ( this, new PropertyChangedEventArgs ( name ) );
            }
        }

        #endregion

        #region Local Event Handlers

        private void Button_Click ( object sender, RoutedEventArgs e )
        {
            char inbound = ((Button)sender).Content.ToString ( )[0];
            if (inbound == 'C')
            {
                Value = 0;
                return;
            }
            int temp = int.Parse ( Value.ToString ( ) + ((Button)sender).Content.ToString ( ) );

            if (MaxValue != 0)
            {
                if (temp > MaxValue)
                {
                    MessageBox.Show ( string.Format ( "The value {0} exceeds the maximum limit of {1}", temp, MaxValue ),ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Stop );
                    return;
                }
                if (temp < MinValue)
                {
                    MessageBox.Show ( string.Format ( "The value {0} exceeds the minimum limit of {1}", temp, MinValue ), ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Stop );
                    return;
                }
            }

            Value = temp;
        }
        #endregion

    }
}
