using System.Windows;
namespace RaspisKusach
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new Pages.MainPage();
        }
    }
}
