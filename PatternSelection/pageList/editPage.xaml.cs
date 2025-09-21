using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace PatternSelection.pageList
{
    /// <summary>
    /// Логика взаимодействия для editPage.xaml
    /// </summary>
    public partial class editPage : Page
    {
        mainClass connect = new mainClass();
        public editPage(string selectedElement)
        {
            InitializeComponent();

            pattern_lab.Content = selectedElement;

            string sql_language = "Select listlanguages.nameLanguages from listlanguages inner join example on example.id_lang = listlanguages.id_lang and id_patern = (SELECT id_pattern from  patterns where namePattern = '"+selectedElement+"')";
            List<string> strings = connect.comboBoxAdd(sql_language);
            foreach (string st in strings)
            {
                comboBox_listLanguage.Items.Add(st);
            }
        }

        private void comboBox_listLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try 
            {
                if (comboBox_listLanguage.SelectedItem != null)
                {
                    string sql_textExample = "select exampleText from example where id_lang = " +
                        "(Select id_lang from listlanguages where nameLanguages = '" + comboBox_listLanguage.SelectedItem+"')" +
                        " and id_patern = (SELECT id_pattern from  patterns where namePattern = '"+pattern_lab.Content+"')";
                    string sql_textDescript = sql_textExample.Replace("exampleText", "exampleDescript");

                    textBlock.Text = connect.selectContent(sql_textExample);
                    textBlock_description.Text = connect.selectContent(sql_textDescript);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (comboBox_listLanguage.SelectedItem != null && textBlock.Text != null && textBlock_description.Text != null)
                {
                    connect.editInTable(
                    pattern_lab.Content.ToString(),
                    comboBox_listLanguage.SelectedItem.ToString(),
                    textBlock.Text.ToString(),
                    textBlock_description.Text.ToString());
                    this.NavigationService.GoBack();
                }
                else MessageBox.Show("Заполните все поля данных");
            }
            catch { }
        }

        private void backImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
