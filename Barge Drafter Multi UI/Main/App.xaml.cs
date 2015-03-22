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
            EventManager.RegisterClassHandler(typeof(TextBox),UIElement.GotFocusEvent,new RoutedEventHandler(TextBox_GotFocus));
            EventManager.RegisterClassHandler(typeof(TextBox), UIElement.MouseDownEvent, new RoutedEventHandler(TextBox_SelectivelyIgnoreMouseButton));
            base.OnStartup(e);
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
