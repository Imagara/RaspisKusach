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
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ListBox.ItemsSource = cnt.db.Table.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
