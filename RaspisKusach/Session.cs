using System.Linq;

namespace RaspisKusach
{
    class Session
    {
        public static Users User { get; set; }
        public static Stations ThisStation = cnt.db.Stations.Where(item => item.IdStation == 4).FirstOrDefault();//cnt.db.Stations.Where(item => item.Location == "Tomsk").FirstOrDefault
    }
}
