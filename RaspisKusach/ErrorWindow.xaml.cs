using System.Windows;
using System.Windows.Input;
namespace RaspisKusach
{
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string error)
        {
            InitializeComponent();
            ErrorLabel.Text = error;
            this.SizeToContent = SizeToContent.Height;
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}

