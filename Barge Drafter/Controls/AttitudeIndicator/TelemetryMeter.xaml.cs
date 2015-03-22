using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MDG.Model;
using System.Windows.Media.Animation;

namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for TelemetryMeter.xaml
    /// </summary>
    public partial class TelemetryMeter 
    {

        public TelemetryMeter()
        {
            InitializeComponent();
            blackBox.DeckPlateChanged += new DeckPlateChangedDelegate ( blackBox_DeckPlateChanged );
            yellowBox.DeckPlateChanged += new DeckPlateChangedDelegate ( yellowBox_DeckPlateChanged );

            cboFreeboardShow.SelectedIndex = 0;
        }

        void yellowBox_DeckPlateChanged ( double newValue )
        {
            yellowBox.DeckPlate = newValue;
        }

        void blackBox_DeckPlateChanged ( double newValue )
        {
            blackBox.DeckPlate = newValue;
        }

        private void ShowHideFreeboard_Changed ( object sender, SelectionChangedEventArgs e )
        {
            ComboBoxItem  item = (ComboBoxItem) ((ComboBox)sender).SelectedItem;

            

            if (item.Content.ToString ( ) == "Show Freeboard")
            {
                blackBox.freeboardPanel.Visibility = Visibility.Visible;
                yellowBox.freeboardPanel.Visibility = Visibility.Visible;
                yellowOpposite.freeboardPanel.Visibility = Visibility.Visible;
                blackOpposite.freeboardPanel.Visibility = Visibility.Visible;
            }
            else
            {
                blackBox.freeboardPanel.Visibility = Visibility.Collapsed;
                yellowBox.freeboardPanel.Visibility = Visibility.Collapsed;
                yellowOpposite.freeboardPanel.Visibility = Visibility.Collapsed;
                blackOpposite.freeboardPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void cmdSwap_Click ( object sender, RoutedEventArgs e )
        {
            if (Grid.GetColumn ( yellowBoxPrimary ) == 3)
            {
                Grid.SetColumn ( yellowBoxPrimary, 0 );
                Grid.SetColumn ( yellowBoxSecondary, 0 );
                Grid.SetColumn ( yellowTileAngle, 0 );
                Grid.SetColumn(blackBoxPrimary,3);
                Grid.SetColumn ( blackBoxSecondary, 3 );
                Grid.SetColumn ( blackTileAngle, 3 );
            }
            else
            {
                Grid.SetColumn ( yellowBoxPrimary, 3 );
                Grid.SetColumn ( yellowBoxSecondary, 3 );
                Grid.SetColumn ( yellowTileAngle, 3 );
                Grid.SetColumn(blackBoxPrimary,0);
                Grid.SetColumn ( blackBoxSecondary, 0 );
                Grid.SetColumn ( blackTileAngle, 0 );
            }
        }

        

        private void ComboBox_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            BargeModel current = (BargeModel)DataContext;
            try
            {
                if (current.IsBusy)
                {
                    MessageBox.Show ( "Please click Stop before changing barge depth.", "I - Draft", MessageBoxButton.OK, MessageBoxImage.Information );
                    return;
                }

                var box = (ComboBox) sender;
                var val = (double) box.SelectedValue;

                if ( val > 0d)
                    current.StartTakingData();
            }
            catch (Exception ex)
            {
                
                current.ReportStatus("Unable to start taking data.", true);
            }
        }



       
    }
}
