using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
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
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    TimeUpdate();
                    TripsUpdate();
                }));
                Task.Delay(1000).Wait();
            }
        }
        void TimeUpdate()
        {
            TimeNowLabel.Content = DateTime.Now.ToString(new CultureInfo("ru-RU"));
        }
        void TripsUpdate()
        {
            List<TripClass> routeList = new List<TripClass>();
            TimeSpan timeOffset = new TimeSpan(0,15,0);
            foreach (Trips trip in cnt.db.Trips)
            {
                if(Functions.GetArrivalTime(Session.ThisStation, trip) >= (DateTime.Now-timeOffset)
                    && Functions.GetDepartureTime(Session.ThisStation, trip) <= DateTime.Now+timeOffset)
                routeList.Add(new TripClass()
                {
                    Trip = trip,
                    StationDirection = Functions.GetRouteDirection(trip),
                    TimeArrival = Functions.GetArrivalTime(Session.ThisStation, trip).ToString(new CultureInfo("ru-RU")),
                    TimeDeparture = Functions.GetDepartureTime(Session.ThisStation, trip).ToString(new CultureInfo("ru-RU"))
                });
            }

            TripsListBox.ItemsSource = routeList;

        }
        public class TripClass
        {
            public Trips Trip { get; set; }
            public string StationDirection { get; set; }
            public string TimeArrival { get; set; }
            public string TimeDeparture { get; set; }
        }
    }
}
