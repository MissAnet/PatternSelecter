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
    /// Логика взаимодействия для openPattern.xaml
    /// </summary>
    public partial class openPattern : Page
    {
        public openPattern(int chislo)
        {
            InitializeComponent();

            string sql_patternSelect = "SELECT  patterns.namePattern,  typepatterns.typeName, realitiontypepatterns.relTypeName, patterns.description, patterns.problem,  patterns.decision, patterns.imageSource FROM patterns INNER JOIN typepatterns ON patterns.idTypePattern = typepatterns.id_type INNER JOIN realitiontypepatterns ON patterns.id_relTypePattern = realitiontypepatterns.id_relType WHERE patterns.id_pattern = " + chislo;
            string sql_patternExample = "SELECT  example.exampleText,example.exampleDescript,  listlanguages.nameLanguages FROM example INNER JOIN patterns ON example.id_patern = patterns.id_pattern INNER JOIN listlanguages ON example.id_lang = listlanguages.id_lang WHERE example.id_patern = " + chislo;
            string sql_patternQuestion = "SELECT questionspattern.question FROM questionspattern where questionspattern.id_pattern = " + chislo;

            mainClass connect = new mainClass();
            
            listView1.ItemsSource = connect.loaddataBase(sql_patternSelect);
            listView3.ItemsSource = connect.loaddataBase(sql_patternExample);
            listView2.ItemsSource = connect.loaddataBase(sql_patternQuestion);
        }

        private void backImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.GoBack(); //вернуться назад
        }

        private void addImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var CompRow = listView1.Items.GetItemAt(0) as DataRowView;

            this.NavigationService.Navigate(new addPatternPage(CompRow[0].ToString())); //добавить пример
        }

        private void editImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var CompRow = listView1.Items.GetItemAt(0) as DataRowView;

            this.NavigationService.Navigate(new editPage(CompRow[0].ToString())); //редактировать пример
        }
    }
}
