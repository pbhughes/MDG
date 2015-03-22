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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestHarness
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow ( )
        {
            InitializeComponent ( );
        }

        private void Button_Click ( object sender, RoutedEventArgs e )
        {
            MessageBox.Show(draftReading.Value.ToString());
            string msg = string.Format(@"Draft Reading is: {0}", model.DraftInches);
            MessageBox.Show(msg);
        }
    }
}
