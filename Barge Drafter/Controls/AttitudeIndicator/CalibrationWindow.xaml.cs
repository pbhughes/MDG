using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for CalibrationWindow.xaml
    /// </summary>
    public partial class CalibrationWindow : Window, INotifyPropertyChanged
    {
        private const double RADIANINV = 180 / Math.PI;

        private double _targeAngle;

        public CalibrationWindow ( double currentFreeBoard, double bargeDepth, double bargeWidth, Color color, 
                                    List<double> deckPlateOptions, string title,  double deckPlate )
        {
            InitializeComponent ( );
            layoutRoot.DataContext = this;
            calculatedDraft.freeboardPanel.Visibility = Visibility.Collapsed;
            BargeDepth = bargeDepth;
            CurrentFreeboard = currentFreeBoard;
            BargeWidth = bargeWidth;
            calculatedDraft.cboDeckPlate.ItemsSource = deckPlateOptions;
            calculatedDraft.DeckPlate = deckPlate;
            
            if (color == Colors.Black)
            {
                calculatedDraft.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                calculatedDraft.Foreground = new SolidColorBrush(Colors.Black);
            }

            pnlCurrentDraft.Background = new SolidColorBrush(color);
            actualDraft.Title = title;
        }

        private double _currentFreeBoard = 0.0;
        public double CurrentFreeboard { get { return _currentFreeBoard; } set { _currentFreeBoard = value; OnPropertyChanged ( "CurrentFreeboard" ); } }


        public double BargeDepth { get; set; }

        public double BargeWidth { get; set; }

        public double CalculateTareAngle()
        {
            double sternRefDraft = ( (BargeDepth * 12) - CurrentFreeboard) -  (actualDraft.Value);

            _targeAngle =   (Math.Atan ( sternRefDraft / (BargeWidth * 12) ) * RADIANINV) * -1;

            return _targeAngle * -1;

        }

        private double _deckPlate = 0.0;

        private void OK_Button_Click ( object sender, RoutedEventArgs e )
        {
            this.DialogResult = true;
        }

        private void Cancel_Button_Click ( object sender, RoutedEventArgs e )
        {
            this.DialogResult = false;
        }

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
    }
}
