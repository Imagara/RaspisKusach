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
        // Получение количества свободных мест в вагоне
        public static int GetCountAvailableSeats(Carriages carriage)
        {
            return carriage.Places - cnt.db.Tickets.Where(item => item.IdCarriage == carriage.IdCarriage).Count();
        }
        // Получение свободного места
        public static int GetAvailableSeat(Carriages carriage)
        {
            int availableSeat = -1;

            if(GetCountAvailableSeats(carriage) > 0)
            {
                if (cnt.db.Tickets.Where(item => item.IdCarriage == carriage.IdCarriage).Select(item => item.PlaceNumber).DefaultIfEmpty(0).Max() + 1 <= carriage.Places)
                    availableSeat = cnt.db.Tickets.Where(item => item.IdCarriage == carriage.IdCarriage).Select(item => item.PlaceNumber).DefaultIfEmpty(0).Max() + 1;
                else
                {
                    int seatCounter = 1;
                    foreach (var item in cnt.db.Tickets.Where(item => item.IdCarriage == carriage.IdCarriage).OrderBy(item => item.PlaceNumber))
                    {
                        if (item.PlaceNumber != seatCounter)
                        {
                            availableSeat = seatCounter;
                            break;
                        }
                        seatCounter++;
                    }
                }
            }
            return availableSeat;
        }
        // Получение номера вагона по билету
        public static int GetCarriageNum(Tickets ticket)
        {
            Carriages requiredСarriage = cnt.db.Carriages.Where(item => item.IdCarriage == ticket.IdCarriage).FirstOrDefault();
            int carrNum = 2;

            foreach(Carriages item in cnt.db.Carriages.Where(item => item.IdTrain == ticket.Trips.IdTrain))
            {
                if (item == requiredСarriage)
                    return carrNum;
                carrNum++;
            }

            return -1;
        }

        // Проверка на необходимую длину и содержание только цифр
        public static bool IsOnlyDigitsAndLengthCorrect(string str, int length)
        {
            foreach (char c in str.Trim())
                if (!char.IsDigit(c))
                    return false;
            if (str.Length != length)
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
        // Проверка на необходимую длину строки
        public static bool IsMinLengthCorrect(string str, int minLength)
        {
            return str.Trim().Length >= minLength;
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
        // Преобразует из "string" в "String"
        public static string ToUlower(string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1, str.Length);
        }
        // Получение всех станций в маршруте в виде строки
        public static string GetDirection(Routes route)
        {
            string stationsList = "";
            foreach (RoutesStations rs in cnt.db.RoutesStations.Where(item => item.IdRoute == route.IdRoute))
                stationsList += rs.Stations.Location == GetDepartureStationLocation(route) ? rs.Stations.Name : $"{rs.Stations.Name} → ";
            return stationsList;
        }

        // Проверка на уникальность номера телефона
        public static bool IsPhoneNumberAlreadyTaken(string Phone)
        {
            return cnt.db.Users.Select(item => item.PhoneNum).Contains(Phone);
        }

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
        //Декодирование картинки
        public static BitmapImage NewImage(Users user)
        {
            MemoryStream ms = new MemoryStream(user.Image);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}
