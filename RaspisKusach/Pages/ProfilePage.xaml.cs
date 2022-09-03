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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            TicketsListBox.Items.Clear();
            UpdateUserInfo();
            UpdateCarriagesList();
        }
        private void UpdateUserInfo()
        {
            FIOLabel.Content = string.Concat(Session.User.Surname, " ", Session.User.Name, " ", Session.User.Patronymic);
            EmailLabel.Content = Session.User.Email;
            ProfileImage.Source = Session.User.Image == null ?
                new BitmapImage(new Uri("../Resources/StandartProfile.png", UriKind.RelativeOrAbsolute)) :
                ProfileImage.Source = Functions.NewImage(Session.User);
            string temp = Session.User.PhoneNum;
            PhoneLabel.Content = $"+{temp.Substring(0, 1)}({temp.Substring(1, 3)}){temp.Substring(4, 3)}-{temp.Substring(7, 2)}-{temp.Substring(9, 2)}";
        }
        private void UpdateCarriagesList()
        {
            List<TicketsClass> routeList = new List<TicketsClass>();
            int carrNum = 2;
            foreach (var item in cnt.db.Tickets.Where(item => item.IdUser == Session.User.IdUser).ToList())
            {
                routeList.Add(new TicketsClass()
                {
                    Ticket = item,
                    FIO = string.Concat(item.Users.Surname, " ", item.Users.Name, " ", item.Users.Patronymic),
                    CarriageNum = Functions.GetCarriageNum(item),
                    BuyDate = item.BuyDate.ToString(new CultureInfo("ru-RU"))
                });
                carrNum++;
            }
            TicketsListBox.ItemsSource = routeList;
        }
        private void ProfileImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BitmapImage image = Functions.SelectImage();
            if (image != null)
            {
                ProfileImage.Source = image;
                Session.User.Image = Functions.BitmapSourceToByteArray((BitmapSource)ProfileImage.Source);
                cnt.db.SaveChanges();
            }
        }
        public class TicketsClass
        {
            public Tickets Ticket { get; set; }
            public string FIO { get; set; }
            public int CarriageNum { get; set; }
            public string BuyDate { get; set; }
        }
    }
}
