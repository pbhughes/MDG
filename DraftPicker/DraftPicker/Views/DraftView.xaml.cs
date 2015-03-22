using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

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
