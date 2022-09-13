using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System;

namespace RaspisKusach.Pages
{
    public partial class TripInfoPage : Page
    {
        Trips trip;
        public TripInfoPage(Trips _trip)
        {
            InitializeComponent();
            trip = _trip;

            Direction.Content = Functions.GetAllStations(trip.Routes);

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
                    CarriageNum = carrNum,
                    AvailableSeats = Functions.GetCountAvailableSeats(item)
                });
                carrNum++;
            }

            CarriageListBox.ItemsSource = routeList;
        }
        private void BuyTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.DataContext is CarriageClass)
            {
                Carriages carr = ((CarriageClass)btn.DataContext).Carriage;
                if (Session.User == null)
                    new ErrorWindow("Для покупки необходимо войти в профиль.").ShowDialog();
                else if (Functions.GetCountAvailableSeats(carr) <= 0)
                    new ErrorWindow("Свободных мест не осталось.").ShowDialog();
                else
                {
                    try
                    {
                        Tickets newTicket = new Tickets()
                        {
                            IdTicket = cnt.db.Tickets.Select(p => p.IdTicket).DefaultIfEmpty(0).Max() + 1,
                            IdUser = Session.User.IdUser,
                            IdTrip = trip.IdTrip,
                            IdCarriage = carr.IdCarriage,
                            PlaceNumber = Functions.GetAvailableSeat(carr),
                            BuyDate = DateTime.Now,
                        };
                        cnt.db.Tickets.Add(newTicket);
                        cnt.db.SaveChanges();
                        new ErrorWindow("Успешная покупка").ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        new ErrorWindow($"Ошибка: {ex}").ShowDialog();
                    }
                }
            }

        }
        public class CarriageClass
        {
            public Carriages Carriage { get; set; }
            public int CarriageNum { get; set; }
            public int AvailableSeats { get; set; }
        }

    }
}
