using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для UserControl3.xaml
    /// </summary>
    public partial class UserControl3 : UserControl
    {
        public UserControl3()
        {
            InitializeComponent();

            string sql_patterns = "Select patterns.namePattern from patterns";
            string sql_language = "Select listlanguages.nameLanguages from listlanguages";

            //comboBox_Patterns.SelectedIndex = 0;

            mainClass connect = new mainClass();

            List<string> strings = connect.comboBoxAdd(sql_language);
            List<string> strings2 = connect.comboBoxAdd(sql_patterns);

            foreach(string st in strings2)
            {
                comboBox_Patterns.Items.Add(st);
            }
            foreach (string st in strings)
            {
                comboBox_listLanguage.Items.Add(st);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(comboBox_Patterns.SelectedItem.ToString());
        }
    }
}
