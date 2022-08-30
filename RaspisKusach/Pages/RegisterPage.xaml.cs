using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RaspisKusach.Pages
{
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
                string[] fio = new string[3];

                switch (registerStage)
                {
                    case 1:
                        if (!Functions.IsMinLengthCorrect(LogBox.Text, 5))
                            new ErrorWindow("Поле «Логин» должно содержать не менее 5 символов.").Show();
                        else if (!Functions.IsMinLengthCorrect(PassBox.Password, 5))
                            new ErrorWindow("Поле «Пароль» должно содержать не менее 5 символов.").Show();
                        else if (!Functions.IsLogEqualPass(LogBox.Text, PassBox.Password))
                            new ErrorWindow("Поля «Логин» и «Пароль» не должны быть равны.").Show();
                        else if (Functions.IsLoginAlreadyTaken(LogBox.Text))
                            new ErrorWindow("Данный логин уже занят").Show();
                        else
                        {
                            RegA.Visibility = Visibility.Collapsed;
                            RegB.Visibility = Visibility.Visible;
                            RegC.Visibility = Visibility.Collapsed;
                            registerStage = 2;
                        }
                        break;
                    case 2:
                        fio = FIOBox.Text.Split(' ');
                        if (!Functions.IsEmailCorrect(EmailBox.Text))
                            new ErrorWindow("Email введен неверно.").Show();
                        else if (Functions.IsEmailAlreadyTaken(EmailBox.Text))
                            new ErrorWindow("Данный email уже используется.").Show();
                        else if (!Functions.IsMinLengthCorrect(fio[0], 2)
                            || !Functions.IsMinLengthCorrect(fio[1], 2)
                            || !Functions.IsMinLengthCorrect(fio[2], 2))
                            new ErrorWindow("Поле ФИО введено неверно.").Show();
                        else
                        {
                            RegA.Visibility = Visibility.Collapsed;
                            RegB.Visibility = Visibility.Collapsed;
                            RegC.Visibility = Visibility.Visible;
                            registerStage = 3;
                            RegisterButton.Content = "Регистрация";
                        }
                        break;
                    case 3:
                        if (!Functions.IsOnlyDigitsAndLengthCorrect(PhoneBox.Text, 11))
                            new ErrorWindow("Номер телефона введен неверно.").Show();
                        else if (!Functions.IsPhoneNumberAlreadyTaken(PhoneBox.Text))
                            new ErrorWindow("Номер телефона уже используется.").Show();
                        else if (!Functions.IsOnlyDigitsAndLengthCorrect(PassportBox.Text, 10))
                            new ErrorWindow("Паспорт введен неверно.").Show();
                        else
                        {
                            Users newUser = new Users()
                            {
                                IdUser = cnt.db.Users.Select(p => p.IdUser).DefaultIfEmpty(0).Max() + 1,
                                Login = LogBox.Text,
                                Password = Encrypt.GetHash(PassBox.Password),
                                Email = EmailBox.Text,
                                Surname = Functions.ToUlower(fio[0]),
                                Name = Functions.ToUlower(fio[1]),
                                Patronymic = Functions.ToUlower(fio[2]),
                                Permissions = cnt.db.Users.Count() == 0 ? 1 : 0,
                            };
                            cnt.db.Users.Add(newUser);
                            cnt.db.SaveChanges();
                            new ErrorWindow("Успешная регистрация").ShowDialog();
                            Session.User = cnt.db.Users.Max();
                            NavigationService.Navigate(new ProfilePage());
                        }
                        break;
                    default:
                        new ErrorWindow("Ошибка.").Show();
                        break;
                }
            }
            catch
            {
                new ErrorWindow("Ошибка.").ShowDialog();
            }
        }
    }
}
