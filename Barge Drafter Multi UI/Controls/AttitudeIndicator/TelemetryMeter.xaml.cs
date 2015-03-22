using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            if (Grid.GetColumn ( yellowTile ) == 3)
            {
                Grid.SetColumn(yellowTile,0);
                Grid.SetColumn(yellowTileOpp,0);
                Grid.SetColumn(yellowTileAngle,0);
                Grid.SetColumn(blackTile,3);
                Grid.SetColumn(blackTileOpp,3);
                Grid.SetColumn(blackTileAngle,3);
            }
            else
            {
                Grid.SetColumn(yellowTile,3);
                Grid.SetColumn(yellowTileOpp,3);
                Grid.SetColumn(yellowTileAngle,3);
                Grid.SetColumn(blackTile,0);
                Grid.SetColumn(blackTileOpp,0);
                Grid.SetColumn(blackTileAngle,0);
            }
        }



       
    }
}
