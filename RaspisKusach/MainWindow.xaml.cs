using System.Windows;
using System.Linq;
using System.Windows.Input;

namespace RaspisKusach
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //test
            Routes route = cnt.db.Routes.Where(item => item.IdRoute == 1).FirstOrDefault();
            temp.Text = Functions.GetAllStations(route).ToString();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonMininize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
