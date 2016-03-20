using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MDG.Model;
using System.Windows.Media.Animation;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for TelemetryMeter.xaml
    /// </summary>
    public partial class TelemetryMeter 
    {
        private string _boxLocationFile = "boxlocations.xml";

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
            int yellowColumn;
            int blackColumn;

            if (Grid.GetColumn ( yellowBoxPrimary ) == 3)
            {
                yellowColumn = 0;
                blackColumn = 3;
                Grid.SetColumn ( yellowBoxPrimary, 0 );
                Grid.SetColumn ( yellowBoxSecondary, 0 );
                Grid.SetColumn ( yellowTileAngle, 0 );
                Grid.SetColumn ( blackBoxPrimary, 3 );
                Grid.SetColumn ( blackBoxSecondary, 3 );
                Grid.SetColumn ( blackTileAngle, 3 );
            }
            else
            {
                yellowColumn = 3;
                blackColumn = 0;
                Grid.SetColumn ( yellowBoxPrimary, 3 );
                Grid.SetColumn ( yellowBoxSecondary, 3 );
                Grid.SetColumn ( yellowTileAngle, 3 );
                Grid.SetColumn ( blackBoxPrimary, 0 );
                Grid.SetColumn ( blackBoxSecondary, 0 );
                Grid.SetColumn ( blackTileAngle, 0 );
            }

            try
            {
                
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.CheckCharacters = true;
                settings.Indent = true;
                settings.IndentChars = "\t";
                settings.OmitXmlDeclaration = true;
                settings.ConformanceLevel = ConformanceLevel.Fragment;

                using (XmlWriter xwr = XmlWriter.Create(_boxLocationFile, settings))
                {
                    xwr.WriteStartElement("position");
                    xwr.WriteStartElement("boxes");
                    xwr.WriteStartElement("box");
                    xwr.WriteAttributeString("color", "yellow");
                    xwr.WriteAttributeString("column", yellowColumn.ToString());
                    xwr.WriteEndElement();
                    xwr.WriteStartElement("box");
                    xwr.WriteAttributeString("color", "black");
                    xwr.WriteAttributeString("column", blackColumn.ToString());
                    xwr.WriteEndElement();

                    xwr.WriteEndElement();
                    xwr.WriteEndElement();
                }
            }
            catch (Exception)
            {

                throw;
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists(_boxLocationFile))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(_boxLocationFile);
                    XmlNode yellow = xDoc.SelectSingleNode("//boxes/box[@color='yellow']").Attributes["column"];
                    XmlNode black = xDoc.SelectSingleNode("//boxes/box[@color='black']").Attributes["column"];
                    int yellowColumn = int.Parse(yellow.Value);
                    int blackColumn = int.Parse(black.Value);

                    Grid.SetColumn(yellowBoxPrimary, yellowColumn);
                    Grid.SetColumn(yellowBoxSecondary, yellowColumn);
                    Grid.SetColumn(yellowTileAngle, yellowColumn);
                    Grid.SetColumn(blackBoxPrimary, blackColumn);
                    Grid.SetColumn(blackBoxSecondary, blackColumn);
                    Grid.SetColumn(blackTileAngle, blackColumn);
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
