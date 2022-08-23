using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

namespace RaspisKusach
{
    public class Functions
    {
        // Валидация номера телефона
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            foreach (char c in phoneNumber)
                if (!char.IsDigit(c))
                    return false;
            if (phoneNumber.Length != 11)
                return false;
            return true;
        }
        // Получение направления по маршруту или поездке
        public static string GetRouteDirection(Routes route)
        {
            string direction = "";
            direction += cnt.db.RoutesStations.Where(item => item.IdRoute == route.IdRoute).Select(item => item.Stations.Location).FirstOrDefault()
                + " - "
                + cnt.db.RoutesStations.Where(item => item.IdRoute == route.IdRoute).OrderByDescending(item => item.IdRouteStation).Select(item => item.Stations.Location).FirstOrDefault();
            return direction;
        }
        public static string GetRouteDirection(Trips trip)
        {
            string direction = "";
            direction += cnt.db.RoutesStations.Where(item => item.IdRoute == trip.Routes.IdRoute).Select(item => item.Stations.Location).FirstOrDefault()
                + " - "
                + cnt.db.RoutesStations.Where(item => item.IdRoute == trip.Routes.IdRoute).OrderByDescending(item => item.IdRouteStation).Select(item => item.Stations.Location).FirstOrDefault();
            return direction;
        }
        // Получение времени прибытия поезда на станцию
        public static DateTime GetArrivalTime(Stations station, Trips trip)
        {
            DateTime date = trip.TripStartDate;
            foreach (RoutesStations item in cnt.db.RoutesStations.Where(item => item.IdRoute == trip.IdRoute))
            {
                if (item.IdStation == station.IdStation)
                    break;
                date += item.StopTime + item.TravelTime;
            }
            return date;
        }
        // Получение времени отбытия поезда со станции
        public static DateTime GetDepartureTime(Stations station, Trips trip)
        {
            DateTime date = trip.TripStartDate;
            foreach (RoutesStations item in cnt.db.RoutesStations.Where(item => item.IdRoute == trip.IdRoute))
            {
                date += item.StopTime;
                if (item.IdStation == station.IdStation)
                    break;
                date += item.TravelTime;
            }
            return date;
        }
        // Валидация электронной почты
        public static bool IsValidEmail(string email)
        {
            if (Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                return true;
            else
                return false;
        }
        // Валидация дня рождения
        public static bool IsValidDateOfBirthday(DateTime Date)
        {
            if (Date > DateTime.Now)
                return false;
            else
                return true;
        }
        // Валидация логина и пароля при входе
        public static bool IsValidLogAndPass(string login, string password)
        {
            if (login == "" || password == "")
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsLogEqualPass(string login, string password)
        {
            if (login == password)
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsValidLength(string str)
        {
            if (str.Length < 5)
                return false;
            else
                return true;
        }
        // Проверка на правильность введеных данных при входе
        public static bool LoginCheck(string login, string password)
        {
            if (cnt.db.Users.Select(item => item.Login + item.Password).Contains(login + Encrypt.GetHash(password)))
                return true;
            else
                return false;
        }
        //// Проверка на уникальность логина
        //public static bool IsLoginAlreadyTaken(string login)
        //{
        //    return cnt.db.User.Select(item => item.NickName).Contains(login);
        //}
        //// Проверка на наличие чата
        //public static bool IsChatAlreadyCreated(string chatName)
        //{
        //    return cnt.db.Chat.Select(item => item.Name).Contains(chatName);
        //}
        //// Получение id чата по его названию
        //public static int GetIdChat(string chatName)
        //{
        //    return cnt.db.Chat.Where(item => item.Name == chatName).Select(item => item.IdChat).FirstOrDefault();
        //}
        //// Проверка на уникальность электронной почты
        //public static bool IsEmailAlreadyTaken(string Email)
        //{
        //    return cnt.db.User.Select(item => item.Email).Contains(Email);
        //}
        //// Проверка на уникальность электронной почты
        //public static bool IsPhoneNumberAlreadyTaken(string Phone)
        //{
        //    return cnt.db.User.Select(item => item.PhoneNumber).Contains(Phone);
        //}

        public static byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            #region Кодирование картинки
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
            #endregion
        }

        public static BitmapImage SelectImage()
        {
            #region Выбор картинки
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Выбрать изображение",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                        "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                        "Portable Network Graphic (*.png)|*.png"
            };
            if (op.ShowDialog() == true)
                return new BitmapImage(new Uri(op.FileName));
            else
                return null;
            #endregion
        }

        //public static BitmapImage NewImage(Users user)
        //{
        //    MemoryStream ms = new MemoryStream(user.Image);
        //    BitmapImage image = new BitmapImage();
        //    image.BeginInit();
        //    image.StreamSource = ms;
        //    image.EndInit();
        //    return image;
        //}
    }
}
