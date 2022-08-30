using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RaspisKusach.Pages
{
    public partial class LoginPage : Page
    {
        bool Test = true;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.RegisterPage());
        }

        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            if (Test)
            {
                Session.User = cnt.db.Users.Where(item => item.IdUser == 1).FirstOrDefault();
                NavigationService.Navigate(new ProfilePage());
            }
            else
            {
                try
                {
                    if (!Functions.IsLogAndPassCorrect(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Поля не могут быть пустыми").Show();
                    else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Неверный логин или пароль").Show();
                    else
                    {
                        Session.User = cnt.db.Users.Where(item => item.Login == LogBox.Text).FirstOrDefault();
                        NavigationService.Navigate(new MenuPage());
                    }
                }
                catch
                {
                    new ErrorWindow("Ошибка входа").ShowDialog();
                }
            }
        }
    }
}
