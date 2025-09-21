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
using System.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatternSelection.pageList
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Color myColor = Color.FromRgb(68, 141, 98);
            //border1.Background = new SolidColorBrush(Color.FromArgb(myColor.A, myColor.R, myColor.G, myColor.B));
            border1.Background = new SolidColorBrush(Color.FromRgb(68, 141, 98));
            daw.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Color myColor = Color.FromRgb(22, 86, 80);
            border1.Background = new SolidColorBrush(Color.FromArgb(myColor.A, myColor.R, myColor.G, myColor.B));
            daw.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
