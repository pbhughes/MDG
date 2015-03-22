using System;
using System.Windows;
using System.Windows.Controls;

namespace MDG.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                EventManager.RegisterClassHandler ( typeof ( TextBox ), UIElement.GotFocusEvent, new RoutedEventHandler ( TextBox_GotFocus ) );
                EventManager.RegisterClassHandler ( typeof ( TextBox ), UIElement.MouseDownEvent, new RoutedEventHandler ( TextBox_SelectivelyIgnoreMouseButton ) );
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler ( CurrentDomain_UnhandledException );
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
          
        }

        void CurrentDomain_UnhandledException ( object sender, UnhandledExceptionEventArgs e )
        {
            throw new NotImplementedException ( );
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var box = sender as TextBox;
            if (box != null) box.SelectAll();
        }

        private void TextBox_SelectivelyIgnoreMouseButton(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                var box = sender as TextBox;
                box.Focus();
                e.Handled = true;
            }
         
        }

        
    }

}
