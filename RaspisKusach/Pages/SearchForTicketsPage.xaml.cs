using System;
using System.Collections.Generic;
using System.Globalization;
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
            Stations arrivalStation = cnt.db.Stations.Where(item => item.Location == StationArrivalComboBox.Text).FirstOrDefault(),
                departureStation = cnt.db.Stations.Where(item => item.Location == StationDepartureComboBox.Text).FirstOrDefault();
            if (arrivalStation == null || departureStation == null)
                return;
            List<TripClass> routeList = new List<TripClass>();

            //Переделать
            foreach (Trips trip in cnt.db.Trips)
            {
                if (cnt.db.RoutesStations.Select(item => item.Stations.Location + " " + item.IdRoute).Contains(arrivalStation.Location + " " + trip.IdRoute)
                    && cnt.db.RoutesStations.Select(item => item.Stations.Location + " " + item.IdRoute).Contains(departureStation.Location + " " + trip.IdRoute)
                    && Functions.GetArrivalTime(arrivalStation, trip) > Functions.GetDepartureTime(departureStation, trip))
                {
                    TripClass rt = new TripClass();
                    rt.trip = trip;
                    rt.trainCategory = trip.Trains.Category;
                    rt.stationDeparture = StationDepartureComboBox.Text;
                    rt.stationArrival = StationArrivalComboBox.Text;
                    rt.timeArrival = Functions.GetArrivalTime(arrivalStation, trip).ToString(new CultureInfo("ru-RU"));
                    rt.timeDeparture = Functions.GetDepartureTime(departureStation, trip).ToString(new CultureInfo("ru-RU"));

                    rt.timeBetween = Functions.GetArrivalTime(arrivalStation, trip) - Functions.GetDepartureTime(departureStation, trip);

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
            public string timeDeparture { get; set; }
            public string timeArrival { get; set; }
            public TimeSpan timeBetween { get; set; }
        }
    }
}
