using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RaspisKusach.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
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
            if(Session.User == null)
                MainContentFrame.Content = new LoginPage();
            else
                MainContentFrame.Content = new ProfilePage();
        }
        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            if(Session.User.)
                MainContentFrame.Content = new ProfilePage();
        }
    }
}
