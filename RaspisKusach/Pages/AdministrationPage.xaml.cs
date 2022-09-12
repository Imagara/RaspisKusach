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
    public partial class AdministrationPage : Page
    {
        public TimeSpan ts1;
        public TimeSpan ts2;

        public AdministrationPage()
        {
            InitializeComponent();
        }

        private void TrainsButton_Click(object sender, RoutedEventArgs e)
        {
            TrainsDataGrid.ItemsSource = cnt.db.Trains.ToList();
        }

        private void TrainsAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cnt.db.Trains.Add(new Trains()
                {
                    IdTrain = cnt.db.Trains.Select(p => p.IdTrain).DefaultIfEmpty(0).Max() + 1,
                    Name = TrainsNameBox.Text,
                    Category = TrainsCategoryBox.Text
                });
                cnt.db.SaveChanges();
                cnt.db.SaveChanges();

            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
        private void StationsAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cnt.db.Stations.Add(new Stations()
                {
                    IdStation = cnt.db.Stations.Select(p => p.IdStation).DefaultIfEmpty(0).Max() + 1,
                    Name = StationsNameBox.Text,
                    Location = StationsLocationBox.Text
                });
                cnt.db.SaveChanges();
                cnt.db.SaveChanges();

            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
        private void TripsAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int trainId;
                if (Functions.IsOnlyDigits(TripsIdTrainBox.Text) && cnt.db.Trains.Select(item => item.IdTrain).Contains(Convert.ToInt32(TripsIdTrainBox.Text)))
                    trainId = Convert.ToInt32(TripsIdTrainBox.Text);
                else if (cnt.db.Trains.Select(item => item.Name).Contains(TripsIdTrainBox.Text))
                    trainId = cnt.db.Trains.Where(item => item.Name == TripsIdTrainBox.Text).Select(item => item.IdTrain).FirstOrDefault();
                else
                {
                    new ErrorWindow("Поезд с таким названием или id не найден").ShowDialog();
                    return;
                }

                int routeId;
                if (Functions.IsOnlyDigits(TripsIdRouteBox.Text) && cnt.db.Routes.Select(item => item.IdRoute).Contains(Convert.ToInt32(TripsIdRouteBox.Text)))
                    routeId = Convert.ToInt32(TripsIdRouteBox.Text);
                else if (cnt.db.Routes.Select(item => item.Name).Contains(TripsIdRouteBox.Text))
                    routeId = cnt.db.Routes.Where(item => item.Name == TripsIdRouteBox.Text).Select(item => item.IdRoute).FirstOrDefault();
                else
                {
                    new ErrorWindow("Маршрут с таким названием или id не найден").ShowDialog();
                    return;
                }
                    
                cnt.db.Trips.Add(new Trips()
                {
                    IdTrip = cnt.db.Trips.Select(p => p.IdTrip).DefaultIfEmpty(0).Max() + 1,
                    IdTrain = trainId,
                    IdRoute = routeId,
                    TripStartDate = (DateTime)TripsStartDatePicker.SelectedDate
                });
                cnt.db.SaveChanges();
                cnt.db.SaveChanges();

            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
        private void RoutesAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cnt.db.Routes.Add(new Routes()
                {
                    IdRoute = cnt.db.Routes.Select(p => p.IdRoute).DefaultIfEmpty(0).Max() + 1,
                    Name = RoutesNameBox.Text
                });
                cnt.db.SaveChanges();
                new ErrorWindow("Успешно").ShowDialog();
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
        private void RoutesStationsAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int routeId;
                if (Functions.IsOnlyDigits(RoutesStationsIdRouteBox.Text) && cnt.db.Routes.Select(item => item.IdRoute).Contains(Convert.ToInt32(RoutesStationsIdRouteBox.Text)))
                    routeId = Convert.ToInt32(RoutesStationsIdRouteBox.Text);
                else if (cnt.db.Routes.Select(item => item.Name).Contains(RoutesStationsIdRouteBox.Text))
                    routeId = cnt.db.Routes.Where(item => item.Name == RoutesStationsIdRouteBox.Text).Select(item => item.IdRoute).FirstOrDefault();
                else
                {
                    new ErrorWindow("Маршрут с таким названием или id не найден").ShowDialog();
                    return;
                }
                
                int stationId;
                if (Functions.IsOnlyDigits(RoutesStationsIdStationBox.Text) && cnt.db.Stations.Select(item => item.IdStation).Contains(Convert.ToInt32(RoutesStationsIdStationBox.Text)))
                    stationId = Convert.ToInt32(RoutesStationsIdStationBox.Text);
                else if (cnt.db.Stations.Select(item => item.Name).Contains(RoutesStationsIdStationBox.Text))
                    stationId = cnt.db.Routes.Where(item => item.Name == RoutesStationsIdStationBox.Text).Select(item => item.IdRoute).FirstOrDefault();
                else
                {
                    new ErrorWindow("Станция с таким названием или id не найден").ShowDialog();
                    return;
                }

                cnt.db.RoutesStations.Add(new RoutesStations()
                {
                    IdRouteStation = cnt.db.RoutesStations.Select(p => p.IdRouteStation).DefaultIfEmpty(0).Max() + 1,
                    IdRoute = routeId,
                    IdStation = stationId,
                    //StopTime = RoutesStationsStopTimeBox.Text,
                    //TravelTime = RoutesStationsTravelTimeBox.Text
                });
                cnt.db.SaveChanges();
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
        private void CarriagesAddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
