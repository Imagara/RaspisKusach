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

            foreach (var station in cnt.db.Station.GroupBy(item => item.Location))
            {
                StationArrivalComboBox.Items.Add(station.Key);
                StationDepartureComboBox.Items.Add(station.Key);
            }
            //UpdateRoutesList();
        }
        private void UpdateRoutesList()
        {
            //var routesList = cnt.db.Routes.ToList();
            //var list = routesList;
            //foreach (Routes route in routesList)
            //{
            //    if (!cnt.db.RoutesStations.Select(item => item.IdRoute + " " + item.IdStation).Contains(route.IdRoute + " " + StationArrivalComboBox.Text)
            //        || !cnt.db.RoutesStations.Select(item => item.IdRoute + " " + item.IdStation).Contains(route.IdRoute + " " + StationDepartureComboBox.Text))
            //        list.Remove(route);
            //}

            List<RouteClass> playerApps = new List<RouteClass>();

            foreach (Routes route in cnt.db.Routes)
            {
                RouteClass playerApp = new RouteClass();
                playerApp.route = route;
                playerApp.str = cnt.db.RoutesStations.Where(item => item.IdStation == cnt.db.Station.Where(it => it.Location == StationArrivalComboBox.Text).Select(it => it.IdStation).FirstOrDefault()).Select(item => item.DateTime).FirstOrDefault().ToString();
                playerApp.stationArrival = StationArrivalComboBox.Text;
                playerApp.stationDeparture = StationDepartureComboBox.Text;
                //playerApp.timeArrival = Data
                //playerApp.timeDe


                playerApps.Add(playerApp);
            }

            ListBox.ItemsSource = playerApps;
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

        public class RouteClass
        {

            public Routes route { get; set; }
            public string str { get; set; }
            public string stationDeparture { get; set; }
            public string stationArrival { get; set; }
            public DateTime timeDeparture { get; set; }
            public DateTime timeArrival { get; set; }

        }

        private void FindRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRoutesList();
        }
    }
}
