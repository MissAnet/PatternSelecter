using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;
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
using static PatternSelection.MainWindow;

namespace PatternSelection
{
    public partial class MainWindow : Window
    {
        private bool expandMin = true;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new pageList.listPattern());
        }

        private void window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            }
            else
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MinimizeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Скрыть окно
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Изменение размера окна
            if (expandMin == false)
            {
                expandMin = true;
                WindowState = WindowState.Normal;
            }
            else
            {
                expandMin = false;
                WindowState = WindowState.Maximized;
            }
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Закрытие окна
            window.Close();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem list = (ListViewItem)listView_sel.SelectedItem;

            try 
            {
                switch (list.Name)
                {
                    case "patternPageItem":
                        mainFrame.Navigate(new pageList.listPattern());
                        break;

                    case "searchPatternPageItem":
                        mainFrame.Navigate(new pageList.searchPattern());
                        break;

                    case "infoPageItem":
                        mainFrame.Navigate(new pageList.infoAboutProgram());
                        break;
                    case "listTasksPageItem":
                        mainFrame.Navigate(new pageList.listTasks());
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
