
using System.Windows;
using System.Windows.Controls;


namespace CineQuebec.Windows.View
{
    /// <summary>
    /// Logique d'interaction pour NavWindows.xaml
    /// </summary>
    public partial class NavWindows : Window
    {
        public NavWindows()
        {
            InitializeComponent();
            mainContentControl.Content = new AdminHomeControl();
        }



        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            mainContentControl.Content = new UsersControl();

        }

     
    }
}
