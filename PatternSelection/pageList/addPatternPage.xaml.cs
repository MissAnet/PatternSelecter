using Org.BouncyCastle.Pkcs;
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
using static Google.Protobuf.Reflection.UninterpretedOption.Types;

namespace PatternSelection.pageList
{
    /// <summary>
    /// Логика взаимодействия для addPatternPage.xaml
    /// </summary>
    public partial class addPatternPage : Page
    {
        mainClass connect = new mainClass();
        public addPatternPage(string selectElemetn)
        {
            InitializeComponent();

            string sql_patterns = "Select patterns.namePattern from patterns";
            string sql_language = "Select listlanguages.nameLanguages from listlanguages";


            List<string> strings = connect.comboBoxAdd(sql_language);
            List<string> strings2 = connect.comboBoxAdd(sql_patterns);

            foreach (string st in strings2)
            {
                comboBox_Patterns.Items.Add(st);
            }
            foreach (string st in strings)
            {
                comboBox_listLanguage.Items.Add(st);
            }

            comboBox_Patterns.SelectedItem = selectElemetn;
        }
        private void backImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBox_Patterns.SelectedItem != null || comboBox_listLanguage.SelectedItem != null || textBlock.Text != null)
                {
                    connect.insertInTable(
                    comboBox_Patterns.SelectedItem.ToString(),
                    comboBox_listLanguage.SelectedItem.ToString(),
                    textBlock.Text.ToString(),
                    textBlock_description.Text.ToString());
                    this.NavigationService.GoBack();
                }
                else MessageBox.Show("Заполните все поля данных");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
