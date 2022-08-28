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
        Trips trip;
        public TripInfoPage(Trips _trip)
        {
            InitializeComponent();
            trip = _trip;

            string stationsList = "";

            foreach (RoutesStations rs in cnt.db.RoutesStations.Where(item => item.IdRoute == trip.IdRoute))
                stationsList += rs.Stations.Location == Functions.GetDepartureStationLocation(trip.Routes) ? rs.Stations.Name : $"{rs.Stations.Name} → ";

            Direction.Content = stationsList;


            CarriageListBox.Items.Clear();
            UpdateCarriagesList();
        }
        private void UpdateCarriagesList()
        {
            List<CarriageClass> routeList = new List<CarriageClass>();
            int carrNum = 2;
            foreach (Carriages item in cnt.db.Carriages.Where(item => item.IdTrain == trip.IdTrain))
            {
                routeList.Add(new CarriageClass()
                {
                    Carriage = item,
                    CarriageNum = carrNum
                });
                carrNum++;
            }

            CarriageListBox.ItemsSource = routeList;
        }
        public class CarriageClass
        {
            public Carriages Carriage { get; set; }
            public int CarriageNum { get; set; }
        }

    }
}
