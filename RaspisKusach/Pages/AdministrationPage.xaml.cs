using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Page
    {
        public AdministrationPage()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            cnt.db.SaveChanges();

        }

        private void TrainsButton_Click(object sender, RoutedEventArgs e)
        {
            TrainsDataGrid.ItemsSource = cnt.db.Trains.ToList();
        }
        private void FindRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            //temp
        }

        private void TrainsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void CarriagesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
