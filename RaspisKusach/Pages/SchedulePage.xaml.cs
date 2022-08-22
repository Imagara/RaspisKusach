using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();
            TripsListBox.Items.Clear(); // temp
            Thread th = new Thread(ThreadUpdate);
            th.Start();
        }
        public void ThreadUpdate()
        {
            while (true)
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    TimeUpdate();
                    TripsUpdate();
                }));
                Thread.Sleep(1000);
            }
        }
        void TimeUpdate()
        {
            TimeNowLabel.Content = DateTime.Now.ToString(new System.Globalization.CultureInfo("ru-RU"));
        }
        void TripsUpdate()
        {

            List<TripClass> routeList = new List<TripClass>();

            foreach (Trips trip in cnt.db.Trips)
            {
                TripClass rt = new TripClass();
                rt.Trip = trip;
                rt.StationDirection = "direction";

                //rt.timeArrival = cnt.db.RoutesStations.
                //    Where(item => item.IdRoute == trip.Routes.IdRoute && item.Station.Location == StationArrivalComboBox.Text).
                //    Select(item => item.DateTime).FirstOrDefault();
                //rt.timeDeparture = cnt.db.RoutesStations.
                //    Where(item => item.IdRoute == trip.Routes.IdRoute && item.Station.Location == StationDepartureComboBox.Text).
                //    Select(item => item.DateTime).FirstOrDefault();
                //rt.timeBetween = rt.timeDeparture - rt.timeArrival;

                routeList.Add(rt);
            }

            TripsListBox.ItemsSource = routeList;

        }
        public class TripClass
        {
            public Trips Trip { get; set; }
            public string StationDirection { get; set; }
            public DateTime TimeDeparture { get; set; }
            public DateTime TimeArrival { get; set; }
        }
    }
}
