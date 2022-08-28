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
        // Получение направления по маршруту или поездке
        public static string GetRouteDirection(Trips trip)
        {
            return GetDepartureStationLocation(trip.Routes) + " - " + GetArrivalStationLocation(trip.Routes);
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
        // Получение станции отправления (первой)
        public static string GetDepartureStationLocation(Routes route)
        {
            return cnt.db.RoutesStations.Where(item => item.IdRoute == route.IdRoute).OrderByDescending(item => item.IdRouteStation).Select(item => item.Stations.Location).FirstOrDefault();
        }
        // Получение станции прибытия (последней)
        public static string GetArrivalStationLocation(Routes route)
        {
            return cnt.db.RoutesStations.Where(item => item.IdRoute == route.IdRoute).Select(item => item.Stations.Location).FirstOrDefault();
        }
        public static int GetAvailableSeats(Carriages carriage)
        {
            //temp
            return 0;
        }

        // Валидация номера телефона
        public static bool IsPhoneNumberCorrect(string phoneNumber)
        {
            foreach (char c in phoneNumber)
                if (!char.IsDigit(c))
                    return false;
            if (phoneNumber.Length != 11)
                return false;
            return true;
        }

        // Проверка электронной почты на правильность ввода
        public static bool IsEmailCorrect(string email)
        {
            return Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }
        // Проверка на уникальность электронной почты
        public static bool IsEmailAlreadyTaken(string Email)
        {
            return cnt.db.Users.Select(item => item.Email).Contains(Email);
        }
        // Валидация дня рождения
        public static bool IsDateOfBirthdayCorrect(DateTime Date)
        {
            return Date <= DateTime.Now;
        }
        // Валидация логина и пароля при входе
        public static bool IsLogAndPassCorrect(string login, string password)
        {
            return login != "" && password != "";
        }
        // Валидация логина и пароля
        public static bool IsLogEqualPass(string login, string password)
        {
            return login != password;
        }
        // Валидация логина и пароля
        public static bool IsLengthCorrect(string str)
        {
            return str.Length >= 5;
        }
        // Проверка на правильность введеных данных при входе
        public static bool LoginCheck(string login, string password)
        {
            return cnt.db.Users.Select(item => item.Login + item.Password).Contains(login + Encrypt.GetHash(password));
        }
        // Проверка на уникальность логина
        public static bool IsLoginAlreadyTaken(string login)
        {
            return cnt.db.Users.Select(item => item.Login).Contains(login);
        }

        //// Проверка на уникальность электронной почты
        //public static bool IsPhoneNumberAlreadyTaken(string Phone)
        //{
        //    return cnt.db.Users.Select(item => item.).Contains(Phone);
        //}

        //Кодирование картинки
        public static byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }
        // Выбор картинки
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
            return op.ShowDialog() == true ? new BitmapImage(new Uri(op.FileName)) : null;
            #endregion
        }
        ////Декодирование картинки
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
