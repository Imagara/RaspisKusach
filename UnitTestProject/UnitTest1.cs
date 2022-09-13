using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using RaspisKusach;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetRouteDirectionTest()
        {
            Trips trip1 = cnt.db.Trips.Where(item => item.IdTrip == 1).FirstOrDefault();
            string expected1 = "TOMSK - OMSK";
            Assert.AreEqual(Functions.GetRouteDirection(trip1), expected1);

            Trips trip2 = cnt.db.Trips.Where(item => item.IdTrip == 2).FirstOrDefault();
            string expected2 = "Novosibirsk - Anapa";
            Assert.AreEqual(Functions.GetRouteDirection(trip2), expected2);
        }
        [TestMethod]
        public void GetArrivalTimeTest()
        {
            Trips trip1 = cnt.db.Trips.Where(item => item.IdTrip == 1).FirstOrDefault();
            Stations station1 = cnt.db.Stations.Where(item => item.IdStation == 1).FirstOrDefault();
            DateTime expected1 = new DateTime(2022, 08, 23, 0, 0, 0);
            Assert.AreEqual(Functions.GetArrivalTime(station1, trip1), expected1);

            Trips trip2 = cnt.db.Trips.Where(item => item.IdTrip == 2).FirstOrDefault();
            Stations station2 = cnt.db.Stations.Where(item => item.IdStation == 5).FirstOrDefault();
            DateTime expected2 = new DateTime(2022, 08, 23, 5, 00, 00);
            Assert.AreEqual(Functions.GetArrivalTime(station2, trip2), expected2);
        }
        [TestMethod]
        public void GetDepartureTimeTest()
        {

            Trips trip1 = cnt.db.Trips.Where(item => item.IdTrip == 1).FirstOrDefault();
            Stations station1 = cnt.db.Stations.Where(item => item.IdStation == 1).FirstOrDefault();
            DateTime expected1 = new DateTime(2022, 08, 23, 0, 30, 0);
            Assert.AreEqual(Functions.GetDepartureTime(station1, trip1), expected1);

            Trips trip2 = cnt.db.Trips.Where(item => item.IdTrip == 2).FirstOrDefault();
            Stations station2 = cnt.db.Stations.Where(item => item.IdStation == 5).FirstOrDefault();
            DateTime expected2 = new DateTime(2022, 08, 23, 5, 20, 00);
            Assert.AreEqual(Functions.GetDepartureTime(station2, trip2), expected2);
        }
        [TestMethod]
        public void GetDepartureStationLocationTest()
        {

            Routes route1 = cnt.db.Routes.Where(item => item.IdRoute == 1).FirstOrDefault();
            string expected1 = "TOMSK";
            Assert.AreEqual(Functions.GetDepartureStationLocation(route1), expected1);

            Routes route2 = cnt.db.Routes.Where(item => item.IdRoute == 2).FirstOrDefault();
            string expected2 = "Novosibirsk";
            Assert.AreEqual(Functions.GetDepartureStationLocation(route2), expected2);
        }
        [TestMethod]
        public void GetArrivalStationLocationTest()
        {

            Routes route1 = cnt.db.Routes.Where(item => item.IdRoute == 1).FirstOrDefault();
            string expected1 = "OMSK";
            Assert.AreEqual(Functions.GetArrivalStationLocation(route1), expected1);

            Routes route2 = cnt.db.Routes.Where(item => item.IdRoute == 2).FirstOrDefault();
            string expected2 = "Anapa";
            Assert.AreEqual(Functions.GetArrivalStationLocation(route2), expected2);
        }
        [TestMethod]
        public void GetCountAvailableSeatsTest()
        {
            Carriages carriage1 = cnt.db.Carriages.Where(item => item.IdCarriage == 1).FirstOrDefault();
            int expected1 = 5;
            Assert.AreEqual(Functions.GetCountAvailableSeats(carriage1), expected1);

            Carriages carriage2 = cnt.db.Carriages.Where(item => item.IdCarriage == 2).FirstOrDefault();
            int expected2 = 0;
            Assert.AreEqual(Functions.GetCountAvailableSeats(carriage2), expected2);
        }
        [TestMethod]
        public void GetAvailableSeatTest()
        {
            Carriages carriage1 = cnt.db.Carriages.Where(item => item.IdCarriage == 1).FirstOrDefault();
            int expected1 = 11;
            Assert.AreEqual(Functions.GetAvailableSeat(carriage1), expected1);

            Carriages carriage2 = cnt.db.Carriages.Where(item => item.IdCarriage == 2).FirstOrDefault();
            int expected2 = -1;
            Assert.AreEqual(Functions.GetAvailableSeat(carriage2), expected2);
        }
        [TestMethod]
        public void GetCarriageNumTest()
        {
            Tickets ticket1 = cnt.db.Tickets.Where(item => item.IdTicket == 1).FirstOrDefault();
            int expected1 = 2;
            Assert.AreEqual(Functions.GetCarriageNum(ticket1), expected1);

            Tickets ticket2 = cnt.db.Tickets.Where(item => item.IdTicket == 25).FirstOrDefault();
            int expected2 = 3;
            Assert.AreEqual(Functions.GetCarriageNum(ticket2), expected2);
        }
        [TestMethod]
        public void IsOnlyDigitsAndLengthCorrectTest()
        {
            string str1 = "123asq312as123 321";
            Assert.IsFalse(Functions.IsOnlyDigitsAndLengthCorrect(str1, 5));

            string str2 = "123";
            Assert.IsFalse(Functions.IsOnlyDigitsAndLengthCorrect(str2, 5));

            string str3 = "123123123";
            Assert.IsTrue(Functions.IsOnlyDigitsAndLengthCorrect(str3, 9));
        }
        [TestMethod]
        public void IsOnlyDigitsTest()
        {
            string str1 = "123asq312as123 321";
            Assert.IsFalse(Functions.IsOnlyDigits(str1));

            string str2 = "123";
            Assert.IsTrue(Functions.IsOnlyDigits(str2));

            string str3 = "12323523";
            Assert.IsTrue(Functions.IsOnlyDigits(str3));
        }
        [TestMethod]
        public void IsEmailCorrectTest()
        {
            string email1 = "imagaragmail.com";
            Assert.IsFalse(Functions.IsEmailCorrect(email1));

            string email2 = "imagara@gmail";
            Assert.IsFalse(Functions.IsEmailCorrect(email2));

            string email3 = "imagara@gmail.com";
            Assert.IsTrue(Functions.IsEmailCorrect(email3));
        }

        [TestMethod]
        public void IsHHMMTimeSpanFromStringCorrectTest()
        {
            string str1 = "10 2";
            Assert.IsFalse(Functions.IsHHMMTimeSpanFromStringCorrect(str1));

            string str2 = "10";
            Assert.IsFalse(Functions.IsHHMMTimeSpanFromStringCorrect(str2));

            string str3 = "10:20";
            Assert.IsTrue(Functions.IsHHMMTimeSpanFromStringCorrect(str3));

            string str4 = "10 20";
            Assert.IsTrue(Functions.IsHHMMTimeSpanFromStringCorrect(str4));
        }

        [TestMethod]
        public void IsEmailAlreadyTakenTest()
        {
            string email1 = "imagara@mail.ru";
            Assert.IsFalse(Functions.IsEmailAlreadyTaken(email1));

            string email2 = "testmail@gmail.com";
            Assert.IsFalse(Functions.IsEmailAlreadyTaken(email2));

            string email3 = "imagara@gmail.com";
            Assert.IsTrue(Functions.IsEmailAlreadyTaken(email3));
        }
        [TestMethod]
        public void IsLogAndPassCorrectTest()
        {
            string login1 = " ";
            string password1 = "Imagara";
            Assert.IsFalse(Functions.IsLogAndPassCorrect(login1, password1));

            string login2 = "Imagara";
            string password2 = "";
            Assert.IsFalse(Functions.IsLogAndPassCorrect(login2, password2));

            string login3 = "Imagara";
            string password3 = "strongPassword";
            Assert.IsTrue(Functions.IsLogAndPassCorrect(login3, password3));
        }
        [TestMethod]
        public void IsLogNotEqualPassTest()
        {
            string login1 = "Imagara";
            string password1 = "Imagara";
            Assert.IsFalse(Functions.IsLogNotEqualPass(login1, password1));

            string login2 = "Imagara";
            string password2 = "strongPassword";
            Assert.IsTrue(Functions.IsLogNotEqualPass(login2, password2));
        }
        [TestMethod]
        public void IsMinLengthCorrectTest()
        {
            string str1 = "qwe";
            Assert.IsFalse(Functions.IsMinLengthCorrect(str1, 5));

            string str2 = "";
            Assert.IsFalse(Functions.IsMinLengthCorrect(str2, 5));

            string str3 = "string";
            Assert.IsTrue(Functions.IsMinLengthCorrect(str3, 5));
        }
        [TestMethod]
        public void LoginCheckTest()
        {
            string login1 = "Imagara";
            string password1 = "Imagara";
            Assert.IsFalse(Functions.LoginCheck(login1, password1));

            string login2 = "Imagara";
            string password2 = "strongPassword";
            Assert.IsTrue(Functions.LoginCheck(login2, password2));
        }
        [TestMethod]
        public void IsLoginAlreadyTakenTest()
        {
            string login1 = "User";
            Assert.IsFalse(Functions.IsLoginAlreadyTaken(login1));

            string login2 = "Imagara";
            Assert.IsTrue(Functions.IsLoginAlreadyTaken(login2));
        }
        [TestMethod]
        public void ToUlowerTest()
        {
            string str = "артем";
            string expected = "Артем";
            Assert.AreEqual(Functions.ToUlower(str), expected);
        }
        [TestMethod]
        public void GetAllStationsTest()
        {
            Routes route = cnt.db.Routes.Where(item => item.IdRoute == 1).FirstOrDefault();
            string expected = "TOMSK-1 → TOMSK-2 → Moscow-1 → Novosibirsk-1 → OMSK";
            Assert.AreEqual(Functions.GetAllStations(route), expected);
        }
        [TestMethod]
        public void IsPhoneNumberAlreadyTakenTest()
        {
            string phoneNumber1 = "89009222950";
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken(phoneNumber1));

            string phoneNumber2 = "89996194949";
            Assert.IsTrue(Functions.IsPhoneNumberAlreadyTaken(phoneNumber2));
        }

        [TestMethod]
        public void PasswordEncryptTest()
        {
            string password = "strongPassword";
            string expected = "6EBD14D4B3ED346D6AE4CDBD41A30F8D65D58093";
            Assert.AreEqual(Encrypt.GetHash(password), expected);
        }
    }
}