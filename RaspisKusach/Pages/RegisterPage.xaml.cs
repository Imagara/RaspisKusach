using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        byte registerStage = 1;
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (registerStage == 1)
                {
                    if (!Functions.IsLengthCorrect(LogBox.Text.Trim()))
                        new ErrorWindow("Поле «Логин» должно содержать не менее 5 символов.").Show();
                    else if (!Functions.IsLengthCorrect(PassBox.Password.Trim()))
                        new ErrorWindow("Поле «Пароль» должно содержать не менее 5 символов.").Show();
                    else if (!Functions.IsLogEqualPass(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Поля «Логин» и «Пароль» не должны быть равны.").Show();
                    else if (Functions.IsLoginAlreadyTaken(LogBox.Text))
                        new ErrorWindow("Данный логин уже занят").Show();
                    else
                    {
                        RegA.Visibility = Visibility.Collapsed;
                        RegB.Visibility = Visibility.Visible;
                        registerStage = 2;
                        RegisterButton.Content = "Регистрация";
                    }
                }
                else
                {
                    if (!Functions.IsEmailCorrect(EmailBox.Text))
                        new ErrorWindow("Email введен неверно.").Show();
                    else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                        new ErrorWindow("Данный email уже используется.").Show();
                    else
                    {
                        string[] fio = new string[3];
                        fio = FIOBox.Text.Split(' ');
                        Users newUser = new Users()
                        {
                            IdUser = cnt.db.Users.Select(p => p.IdUser).DefaultIfEmpty(0).Max() + 1,
                            Login = LogBox.Text,
                            Password = Encrypt.GetHash(PassBox.Password),
                            Email = EmailBox.Text,
                            Surname = fio[0],
                            Name = fio[1],
                            Patronymic = fio[2],
                            Permissions = cnt.db.Users.Count() == 0 ? 1 : 0,
                        };
                        cnt.db.Users.Add(newUser);
                        cnt.db.SaveChanges();
                        new ErrorWindow("Успешная регистрация").ShowDialog();

                        Session.User = cnt.db.Users.Max();
                        NavigationService.Navigate(new ProfilePage());
                    }
                }
            }
            catch
            {
                new ErrorWindow("Ошибка.").ShowDialog();
            }
        }
    }
}
