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
    public partial class TripInfoPage : Page
    {
        public TripInfoPage(Trips trip)
        {
            InitializeComponent();
            string stationsList = "";
            foreach (RoutesStations rs in cnt.db.RoutesStations.Where(item => item.IdRoute == trip.IdRoute))
                stationsList += rs.Stations.Location == Functions.GetDepartureStationLocation(trip) ? rs.Stations.Name : $"{rs.Stations.Name} → ";

            Direction.Content = stationsList;
            UpdateCarriagesList();
        }
        private void UpdateCarriagesList()
        {

            Stations arrivalStation = cnt.db.Stations.Where(item => item.Location == StationArrivalComboBox.Text).FirstOrDefault(),
                departureStation = cnt.db.Stations.Where(item => item.Location == StationDepartureComboBox.Text).FirstOrDefault();
            if (arrivalStation == null || departureStation == null)
                return;

            List<TripClass> routeList = new List<TripClass>();

            foreach (Trips trip in cnt.db.Trips)
            {
                if (cnt.db.RoutesStations.Select(item => item.Stations.Location + " " + item.IdRoute).Contains(arrivalStation.Location + " " + trip.IdRoute)
                    && cnt.db.RoutesStations.Select(item => item.Stations.Location + " " + item.IdRoute).Contains(departureStation.Location + " " + trip.IdRoute)
                    && Functions.GetArrivalTime(arrivalStation, trip) > Functions.GetDepartureTime(departureStation, trip)
                    && (Functions.GetArrivalTime(arrivalStation, trip).ToShortDateString() == ArrivalDate.Text
                        || ArrivalDate.Text == null
                        || ArrivalDate.Text.Trim() == ""))
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

            CarriageListBox.ItemsSource = routeList;
        }
        public class TripClass
        {
            public Carriages carriage { get; set; }
            public string trainCategory { get; set; }
            public string stationDeparture { get; set; }
            public string stationArrival { get; set; }
            public string timeDeparture { get; set; }
            public string timeArrival { get; set; }
            public TimeSpan timeBetween { get; set; }
        }

    }
}
