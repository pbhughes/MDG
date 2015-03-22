using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Microsoft.Win32;
using System.Drawing.Printing;

namespace MDG.Visuals
{
    /// <summary>
    /// Interaction logic for IDraftSaveFileDialog.xaml
    /// </summary>
    public partial class IDraftSaveFileDialog : Window
    {
        public IDraftSaveFileDialog ( string textToPrint )
        {
            InitializeComponent ( );
            txtPrintData.Text = textToPrint;
        }

        private void Save_Click ( object sender, RoutedEventArgs e )
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog ( );
                saveFileDialog.DefaultExt = ".txt";
                saveFileDialog.Filter =  "Text documents (.txt)|*.txt";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.MyDocuments );
                if (saveFileDialog.ShowDialog ( ) == true)
                    File.WriteAllText ( saveFileDialog.FileName, txtPrintData.Text );
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

        private void txtBargeID_KeyDown ( object sender, System.Windows.Input.KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                string txt = txtPrintData.Text;
                txt = txt.Replace ( "I-Draft Draft Ticket",
                                  string.Format ( "I-Draft Draft Ticket \nBarge ID: {0}", txtBargeID.Text ) );
                txtPrintData.Text = txt;
            }
        }

        private void Print_Button_Click ( object sender, RoutedEventArgs e )
        {
            try
            {
                throw new NotImplementedException("Functionality has not been implemented yet");
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
