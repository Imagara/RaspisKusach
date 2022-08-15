using System.Linq;
namespace RaspisKusach
{
    class Session
    {
        public static Users User { get; set; }
        public static Station ThisTableStation = cnt.db.Station.Where(item => item.IdStation == 1).FirstOrDefault();//cnt.db.Station.Where(item => item.Location == "Tomsk").FirstOrDefault();
    }
}
