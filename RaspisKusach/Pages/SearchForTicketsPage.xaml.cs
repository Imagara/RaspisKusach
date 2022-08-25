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
    public partial class SearchForTicketsPage : Page
    {
        public SearchForTicketsPage()
        {
            InitializeComponent();
            ListBox.Items.Clear();
            foreach (var station in cnt.db.Stations.GroupBy(item => item.Location))
            {
                StationArrivalComboBox.Items.Add(station.Key);
                StationDepartureComboBox.Items.Add(station.Key);
            }
            UpdateRoutesList();
        }
        private void UpdateRoutesList()
        {
            List<TripClass> routeList = new List<TripClass>();

            foreach (Trips trip in cnt.db.Trips)
            {
                if (cnt.db.RoutesStations.Select(item => item.Stations.Location + " " + item.IdRoute).Contains(StationArrivalComboBox.Text + " " + trip.IdRoute)
                    && cnt.db.RoutesStations.Select(item => item.Stations.Location + " " + item.IdRoute).Contains(StationDepartureComboBox.Text + " " + trip.IdRoute))
                {
                    TripClass rt = new TripClass();
                    rt.trip = trip;
                    rt.trainCategory = trip.Trains.Category;
                    rt.stationArrival = StationArrivalComboBox.Text;
                    rt.stationDeparture = StationDepartureComboBox.Text;
                    //rt.timeArrival = cnt.db.RoutesStations.
                    //    Where(item => item.IdRoute == trip.Routes.IdRoute && item.Stations.Location == StationArrivalComboBox.Text).
                    //    Select(item => item.DateTime).FirstOrDefault();
                    //rt.timeDeparture = cnt.db.RoutesStations.
                    //    Where(item => item.IdRoute == trip.Routes.IdRoute && item.Stations.Location == StationDepartureComboBox.Text).
                    //    Select(item => item.DateTime).FirstOrDefault();
                    //rt.timeBetween = rt.timeDeparture - rt.timeArrival;

                    routeList.Add(rt);
                }
            }

            ListBox.ItemsSource = routeList;
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void FindRoutesButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRoutesList();
        }

        public class TripClass
        {
            public Trips trip { get; set; }
            public string trainCategory { get; set; }
            public string stationDeparture { get; set; }
            public string stationArrival { get; set; }
            public DateTime timeDeparture { get; set; }
            public DateTime timeArrival { get; set; }
            public TimeSpan timeBetween { get; set; }
        }
    }
}
