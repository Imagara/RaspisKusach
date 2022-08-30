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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            TicketsListBox.Items.Clear();
            TicketsListBox.ItemsSource = cnt.db.Tickets.Where(item => item.IdUser == Session.User.IdUser).ToList();
        }

        private void TicketsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
