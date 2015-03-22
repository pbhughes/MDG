
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

namespace MDG.DataLogViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker _bw = new BackgroundWorker();
       
        public MainWindow ( )
        {
            InitializeComponent ( );
            _bw.DoWork += new DoWorkEventHandler ( _bw_DoWork );
            _bw.WorkerSupportsCancellation = true;
            _bw.WorkerReportsProgress = true;
            _bw.ProgressChanged += new ProgressChangedEventHandler ( _bw_ProgressChanged );
            _bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler ( _bw_RunWorkerCompleted );
        }

        void _bw_RunWorkerCompleted ( object sender, RunWorkerCompletedEventArgs e )
        {
            if(! _bw.CancellationPending &&  chkBox.IsChecked == true)
                _bw.RunWorkerAsync();
        }   

        void _bw_ProgressChanged ( object sender, ProgressChangedEventArgs e )
        {
            GetData ( );
        }

        void _bw_DoWork ( object sender, DoWorkEventArgs e )
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    System.Threading.Thread.Sleep ( 1000 );
                    _bw.ReportProgress(i);
                }
                   
            }
            catch (Exception ex)
            {
                _bw.CancelAsync();
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click ( object sender, RoutedEventArgs e )
        {
            try
            {
                GetData();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }

        private int GetData()
        {
            gridDataLog.ItemsSource = bargeData.DataLogFormated;
            return bargeData.DataLogFormated.Count;
        }

        private void ClearButtonClick ( object sender, RoutedEventArgs e )
        {
            try
            {
                bargeData.ClearEntries ( );
                GetData ( );
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }

        private void CheckBox_Checked ( object sender, RoutedEventArgs e )
        {
            try
            {
                if (((CheckBox)sender).IsChecked == true)
                    _bw.RunWorkerAsync ( );
                else
                {
                    _bw.CancelAsync ( );
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
          
        }

        private void Export_ButtonClick ( object sender, RoutedEventArgs e )
        {
            TextWriter tr = null;
            try
            {
                
                string fileName = string.Empty;
                SaveFileDialog fileOpen = new SaveFileDialog ( );
                fileOpen.DefaultExt = ".txt";
                fileOpen.Filter = "Text Files (*.txt)|*.txt";
                chkBox.IsChecked = false;
                if (fileOpen.ShowDialog ( this ) == true)
                {
                    fileName = fileOpen.FileName;
                }
               
                if(File.Exists(fileName))
                    File.Delete(fileName);

                tr = new StreamWriter ( new FileStream ( fileName, FileMode.CreateNew, FileAccess.ReadWrite ) );
                tr.WriteLine ( "Count,ReadingDate,Stern Angle," +
                                            "Stern Water Side, Stern Dock Side, " +
                                            "Bow Angle, Bow Water Side, Bow Dock Side, " +
                                            "Average Draft, Light Draft, Heavy Draft" );

                foreach (var row in bargeData.DataLogFormated)
                {
                    tr.WriteLine ( row.ToString ( ) );
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show ( ex.Message );
            }
            finally
            {
                if (tr != null)
                {
                    tr.Flush();
                    tr.Close();
                }

            }
        }

       
    }
}
