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
                saveFileDialog.FileName = string.Format("{0}_{1}_{2}_{3}", 
                    DateTime.Now.Month,DateTime.Now.Day,DateTime.Now.Year, txtBargeID.Text);
                saveFileDialog.Filter =  "Text documents (.txt)|*.txt";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath ( Environment.SpecialFolder.MyDocuments );
                saveFileDialog.ValidateNames = false;
                string dataToPrint = txtPrintData.Text;

                if (saveFileDialog.ShowDialog ( ) == true)
                    File.WriteAllText ( saveFileDialog.FileName, dataToPrint );


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
                string newText = SetTicketContent();

                txtPrintData.Text = newText;
            }
        }

        private string SetTicketContent()
        {
            string txt = txtPrintData.Text;
            string newText = string.Format("I-DRAFT TICKET {0}***************************************{0}Hull ID: {1} {0}Record Date: {2} {0}{3}",
                Environment.NewLine, txtBargeID.Text, DateTime.Now.ToShortDateString(), txt);
            return newText;
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

        private void Email_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature would email the results using the clients email client, Not yet implemented.");
        }

        private void Publish_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature would publish the results via the web to bargedata.com for storage and tracking, Not yet implemented.");
        }

        private void SendText_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This feature would text the tow company or the tow driver, Not yet implemented.");

        }

        private void splitButton_Click(object sender, RoutedEventArgs e)
        {
            splitButton.IsOpen = true;
        }
    }
}
