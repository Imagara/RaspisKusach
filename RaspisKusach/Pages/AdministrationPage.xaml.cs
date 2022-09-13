using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RaspisKusach.Pages
{
    public partial class AdministrationPage : Page
    {
        public AdministrationPage()
        {
            InitializeComponent();
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
                new ErrorWindow("Успешно").ShowDialog();

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
                new ErrorWindow("Успешно").ShowDialog();

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
                new ErrorWindow("Успешно").ShowDialog();
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
                    new ErrorWindow("Станция с таким названием или id не найдена").ShowDialog();
                    return;
                }

                if(Functions.IsHHMMTimeSpanFromStringCorrect(RoutesStationsStopTimeBox.Text))
                {
                    new ErrorWindow("Строка с временем остановки имела неверный формат").ShowDialog();
                    return;
                }
                if(Functions.IsHHMMTimeSpanFromStringCorrect(RoutesStationsTravelTimeBox.Text))
                {
                    new ErrorWindow("Строка с временем пути имела неверный формат").ShowDialog();
                    return;
                }

                cnt.db.RoutesStations.Add(new RoutesStations()
                {
                    IdRouteStation = cnt.db.RoutesStations.Select(p => p.IdRouteStation).DefaultIfEmpty(0).Max() + 1,
                    IdRoute = routeId,
                    IdStation = stationId,
                    StopTime = new TimeSpan(Convert.ToInt32(RoutesStationsStopTimeBox.Text.Substring(0, 2)), Convert.ToInt32(RoutesStationsStopTimeBox.Text.Substring(3, 2)), 0),
                    TravelTime = new TimeSpan(Convert.ToInt32(RoutesStationsTravelTimeBox.Text.Substring(0, 2)), Convert.ToInt32(RoutesStationsTravelTimeBox.Text.Substring(3, 2)), 0),
                });
                cnt.db.SaveChanges();
                new ErrorWindow("Успешно").ShowDialog();
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }
        private void CarriagesAddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int trainId;
                if (Functions.IsOnlyDigits(CarriagesIdTrainBox.Text) && cnt.db.Trains.Select(item => item.IdTrain).Contains(Convert.ToInt32(CarriagesIdTrainBox.Text)))
                    trainId = Convert.ToInt32(CarriagesIdTrainBox.Text);
                else if (cnt.db.Trains.Select(item => item.Name).Contains(CarriagesIdTrainBox.Text))
                    trainId = cnt.db.Trains.Where(item => item.Name == CarriagesIdTrainBox.Text).Select(item => item.IdTrain).FirstOrDefault();
                else
                {
                    new ErrorWindow("Поезд с таким названием или id не найден").ShowDialog();
                    return;
                }

                if (Functions.IsOnlyDigits(CarriagesPlacesBox.Text))
                {
                    new ErrorWindow("Строка с количеством мест имела неверный формат").ShowDialog();
                    return;
                }
                
                cnt.db.Carriages.Add(new Carriages()
                {
                    IdCarriage = cnt.db.Carriages.Select(p => p.IdCarriage).DefaultIfEmpty(0).Max() + 1,
                    IdTrain = trainId,
                    Places = Convert.ToInt32(CarriagesPlacesBox.Text),
                    Category = CarriagesCategoryBox.Text
                });
                cnt.db.SaveChanges();
                new ErrorWindow("Успешно").ShowDialog();
            }
            catch (Exception ex)
            {
                new ErrorWindow(ex.Message).ShowDialog();
            }
        }

        private void RoutesStationsTabItemClick(object sender, MouseButtonEventArgs e)
        {
            RoutesStationsDataGrid.ItemsSource = cnt.db.RoutesStations.ToList();
        }
        private void CarriagesTabItemClick(object sender, MouseButtonEventArgs e)
        {
            CarriagesDataGrid.ItemsSource = cnt.db.Carriages.ToList();
        }
        private void RoutesTabItemClick(object sender, MouseButtonEventArgs e)
        {
            RoutesDataGrid.ItemsSource = cnt.db.Routes.ToList();
        }
        private void StationsTabItemClick(object sender, MouseButtonEventArgs e)
        {
            StationsDataGrid.ItemsSource = cnt.db.Stations.ToList();
        }
        private void TripsTabItemClick(object sender, MouseButtonEventArgs e)
        {
            TripsDataGrid.ItemsSource = cnt.db.Trips.ToList();
        }
        private void TrainsTabItemClick(object sender, MouseButtonEventArgs e)
        {
            TrainsDataGrid.ItemsSource = cnt.db.Trains.ToList();
        }
    }
}
