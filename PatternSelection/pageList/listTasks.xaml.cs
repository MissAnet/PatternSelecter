using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
using static PatternSelection.pageList.searchPattern;

namespace PatternSelection.pageList
{
    /// <summary>
    /// Логика взаимодействия для listTasks.xaml
    /// </summary>
    public partial class listTasks : Page
    {
        mainClass connect = new mainClass();
        string sql_tasks = "Select id_pattern,textTask,(select namePattern from patterns where patterns.id_pattern = tasks.id_pattern) as patternName from tasks;";

        public listTasks()
        {
            InitializeComponent();
            
            string sql_pattern = "select namePattern from patterns";
            List<string> strings = connect.comboBoxAdd(sql_pattern);

            listView_base.ItemsSource = connect.loaddataBase(sql_tasks);

            comboBoxPattern.Items.Add("Все типы");
            comboBoxPattern.SelectedIndex = 0;
            
            foreach (string st in strings)
            {
                comboBoxPattern.Items.Add(st);
            }
        }

        private void comboBoxPattern_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (comboBoxPattern.SelectedIndex != 0)
                {
                    string comboPattern = comboBoxPattern.SelectedItem.ToString();
                    sql_tasks = "Select id_pattern,textTask,(select namePattern from patterns where patterns.id_pattern = tasks.id_pattern) as patternName from tasks where tasks.id_pattern = (select id_pattern from patterns where patterns.namePattern ='" + comboPattern + "');";
                }
                else sql_tasks = "Select id_pattern,textTask,(select namePattern from patterns where patterns.id_pattern = tasks.id_pattern) as patternName from tasks;";

                listView_base.ItemsSource = connect.loaddataBase(sql_tasks);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
