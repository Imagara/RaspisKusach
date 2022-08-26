using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;

namespace RaspisKusach
{
    public partial class Style : ResourceDictionary
    {
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = new SolidColorBrush(Color.FromRgb(0x8B, 0x00, 0xFF));
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            ((Border)sender).BorderBrush = new SolidColorBrush(Color.FromRgb(0x00, 0x00, 0x00));
        }
    }
}