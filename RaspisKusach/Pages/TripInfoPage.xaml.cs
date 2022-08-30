using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace RaspisKusach.Pages
{
    public partial class TripInfoPage : Page
    {
        Trips trip;
        public TripInfoPage(Trips _trip)
        {
            InitializeComponent();
            trip = _trip;

            Direction.Content = Functions.GetDirection(trip.Routes);

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
                    AvailableSeats = Functions.GetAvailableSeats(item)
                });
                carrNum++;
            }

            CarriageListBox.ItemsSource = routeList;
        }
        public class CarriageClass
        {
            public Carriages Carriage { get; set; }
            public int CarriageNum { get; set; }
            public int AvailableSeats { get; set; }
        }

    }
}
