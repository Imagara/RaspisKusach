﻿using System;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
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
                NavigationService.Navigate(new MenuPage());
            }
            else
            {
                try
                {
                    if (!Functions.IsValidLogAndPass(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Поля не могут быть пустыми").Show();
                    else if (!Functions.LoginCheck(LogBox.Text, PassBox.Password))
                        new ErrorWindow("Неверный логин или пароль").Show();
                    else
                    {
                        //Profile.userId = cnt.db.User.Where(item => item.NickName == LogBox.Text).Select(item => item.Id).FirstOrDefault();
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
