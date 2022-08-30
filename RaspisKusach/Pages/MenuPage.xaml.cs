using System.Windows;
using System.Windows.Controls;

namespace RaspisKusach.Pages
{
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new SearchForTicketsPage();
        }
        private void ScheduleButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.Content = new SchedulePage();
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (Session.User == null)
                MainContentFrame.Content = new LoginPage();
            else
                MainContentFrame.Content = new ProfilePage();
        }
        private void AdministrationButton_Click(object sender, RoutedEventArgs e)
        {
            if (Session.User == null || Session.User.Permissions == 1)
                MainContentFrame.Content = new AdministrationPage();
            else
                ((Button)sender).Visibility = Visibility.Collapsed;
        }
    }
}
