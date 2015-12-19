using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System;
using MDG.Infrastructure.Conversions;

namespace DraftPicker.Views
{
    /// <summary>
    /// Interaction logic for DraftView.xaml
    /// </summary>
    public partial class DraftView : UserControl, INotifyPropertyChanged
    {
        public DraftView ( )
        {
            InitializeComponent ( );
            layoutRoot.DataContext = this;
        }

        #region Properties and Fields

        private int _freeBoard;
        public int FreeBoard
        {
            get
            {
                return _freeBoard;
            }

            set
            {
                _freeBoard = value;
                Feet = MeasurementConversions.GetFeet(value);
                Inches = MeasurementConversions.GetIches(value);
                OnPropertyChanged("FreeBoard");
            }
        }

        private int _feet = 0;

        public int Feet
        {
            get { return _feet; }
            set
            {
                _feet = value;
                SetValue(ValueProperty, (_feet*12) + _inches);
                OnPropertyChanged("Feet");
            }
        }

        private int _inches = 0;

        public int Inches
        {
            get { return _inches; }
            set
            {
                _inches = value;
                SetValue(ValueProperty, (_feet * 12) + _inches);
                OnPropertyChanged("Inches");
            }
        }

        public int TotalInches
        {
            get { return (_feet * 12) + _inches; }
        }



        #endregion

        #region Depedency Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof (string), typeof (DraftView), null);

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }


        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof (int), typeof (DraftView), null);

        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        



        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        


        private void OnPropertyChanged ( string name )
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler ( this, new PropertyChangedEventArgs ( name ) );
            }
        }



        #endregion

        #region Local Events

        #endregion

     
    }
}
