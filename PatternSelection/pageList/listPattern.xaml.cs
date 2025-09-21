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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PatternSelection.pageList
{
    public partial class listPattern : Page
    {
        public listPattern()
        {
            InitializeComponent();

            string sql = "SELECT  patterns.id_pattern, patterns.namePattern, typepatterns.typeName,  " +
                "realitiontypepatterns.relTypeName,  patterns.description,  patterns.problem,  patterns.decision,  " +
                "patterns.imageSource FROM patterns INNER JOIN typepatterns ON patterns.idTypePattern = typepatterns.id_type " +
                "INNER JOIN realitiontypepatterns ON patterns.id_relTypePattern = realitiontypepatterns.id_relType";
            mainClass connect = new mainClass();

            listView_base.ItemsSource = connect.loaddataBase(sql);
        }
    
        private void listView_base_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Открытие выбранного элемента

            var CompRow = listView_base.Items.GetItemAt(listView_base.SelectedIndex) as DataRowView;

            this.NavigationService.Navigate(new openPattern(int.Parse(CompRow[0].ToString())));
        }
    }
}
