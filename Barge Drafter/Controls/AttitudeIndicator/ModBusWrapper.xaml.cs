using System;
using System.Text;
using System.Windows;
using MDG.Model;
using MDG.Conversions;


namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for ModBusWrapper.xaml
    /// </summary>
    public partial class ModBusWrapper : IDisposable
    {
       

        public ModBusWrapper()
        {
            InitializeComponent();
           // barge.ModBusIsConnected = false;
            
            //load barge width options
            foreach (var option in Properties.Settings.Default.BargeWidthOptions)
            {
                int item = 0;
                bool worked = int.TryParse(option, out item);
                if(worked)
                    barge.AddBargeWidthOption(item);
            }

            //load barge depth options
            foreach (var option in Properties.Settings.Default.BargeDepthOptions)
            {
                double item = 0;
                bool worked = double.TryParse ( option, out item );
                if (worked)
                    barge.AddBargeDepthOptions(item );
            }

            //load deck plate options
            foreach (var option in Properties.Settings.Default.DeckPlateOptions)
            {
                double item = 0;
                bool worked = double.TryParse(option, out item);
                if(worked)
                    barge.AddDeckPlateOption(item);
            }

            //Load inches
            barge.AddInchOptions();


        }

       

        public void Dispose ( )
        {
            if(barge != null)
                barge.Dispose();
        }

        private void PrintButton_Click ( object sender, System.Windows.RoutedEventArgs e )
        {
            try
            {
                var barge = ((BargeModel) this.DataContext);
                StringBuilder sb = new StringBuilder("I-Draft Draft Ticket \n");
                sb.AppendLine(string.Format("Date: {0,10}", DateTime.Now.ToShortDateString()));
                sb.AppendLine(string.Format("Light Draft: {0,10}", barge.LightDraft));
                sb.AppendLine ( string.Format ("Heavy Draft: {0,10}", barge.HeavyDraft ) );
                sb.AppendLine(
                    string.Format("Bow Dock: {0,10}     Bow Water: {1,10}",
                                  ((barge.BargeDepth*12) - barge.DockSideFreeboardForward).ToFeetAndInches(),
                    ((barge.BargeDepth*12) - barge.WaterSideFreeboardForward).ToFeetAndInches()));

                sb.AppendLine (
                  string.Format ( "Stern Dock: {0,10}     Stern Water: {1,10}",
                                ((barge.BargeDepth * 12) - barge.DockSideFreeboardAft).ToFeetAndInches ( ),
                  ((barge.BargeDepth * 12) - barge.WaterSideFreeboardAft).ToFeetAndInches ( ) ) );

                IDraftSaveFileDialog sv = new IDraftSaveFileDialog(sb.ToString());
                sv.Owner = Application.Current.MainWindow;
                sv.WindowStartupLocation = WindowStartupLocation.Manual;
                sv.Top = 25;
                sv.Left = 25;
                sv.Title = "I-Draft Draft Ticket";
                sv.ShowDialog();

            }
            catch (Exception ex)
            {

               Application.Current.Dispatcher.Invoke (
                    new Action ( ( ) => MessageBox.Show ( Application.Current.MainWindow, ex.Message,
                                                     "I Draft Error", MessageBoxButton.OK,
                                                     MessageBoxImage.None, MessageBoxResult.OK,
                                                     MessageBoxOptions.None ) ) );
            }
        }

      

    }

}
