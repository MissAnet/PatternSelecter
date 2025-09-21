using System;
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

namespace PatternSelection.pageList
{
    /// <summary>
    /// Логика взаимодействия для UserControl5.xaml
    /// </summary>
    public partial class UserControl5 : UserControl
    {
        public UserControl5()
        {
            InitializeComponent();
        }
        private void border1_MouseEnter(object sender, MouseEventArgs e)
        {
            Color myColor = Color.FromRgb(68, 141, 98);
            border2.Background = new SolidColorBrush(Colors.OrangeRed);
            textT.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void border1_MouseLeave(object sender, MouseEventArgs e)
        {
            Color myColor = Color.FromRgb(22, 86, 80);
            border2.Background = new SolidColorBrush(Color.FromArgb(myColor.A, myColor.R, myColor.G, myColor.B));
            textT.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
