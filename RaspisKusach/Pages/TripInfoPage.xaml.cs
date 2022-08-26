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
    public partial class TripInfoPage : Page
    {
        public TripInfoPage(Trips trip)
        {
            InitializeComponent();
            string stationsList = "";
            foreach (RoutesStations rs in cnt.db.RoutesStations.Where(item => item.IdRoute == trip.IdRoute))
                stationsList += rs.Stations.Location == Functions.GetDepartureStationLocation(trip) ? rs.Stations.Name : $"{rs.Stations.Name} → ";
            Direction.Content = stationsList;

        }

    }
}
