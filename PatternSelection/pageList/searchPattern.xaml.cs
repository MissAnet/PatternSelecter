using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq.Expressions;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Windows.Ink;
using Org.BouncyCastle.Bcpg.Sig;

namespace PatternSelection.pageList
{
    /// <summary>
    /// Логика взаимодействия для searchPattern.xaml
    /// </summary>
    public partial class searchPattern : Page
    {
        mainClass connect = new mainClass();
        
        string zapros = "SELECT pat.id_pattern,pat.namePattern,pat.description FROM patterns as pat JOIN realitiontypepatterns as reltp ON pat.id_relTypePattern = reltp.id_relType JOIN typepatterns as tp ON pat.idTypePattern = tp.id_type JOIN questionspattern as quest ON pat.id_pattern = quest.id_pattern JOIN listlanguages as lang ON lang.nameLanguages like '%%' JOIN example as examp ON examp.id_lang = lang.id_lang AND examp.id_patern = pat.id_pattern WHERE (typeName Like '%%' OR relTypeName Like '%%')";
        
        //Добавление хэштегов
        private class Pattern
        {
            public string textTag { get; set; }

            public override string ToString()
            {
                return textTag;
            }
        }
        List<Pattern> patterns = new List<Pattern>();
        string[] quest = new string[] { };

        public searchPattern()
        {
            InitializeComponent();

            string sql_types = "SELECT typepatterns.typeName  FROM typepatterns  UNION  SELECT realitiontypepatterns.relTypeName FROM realitiontypepatterns";
            string sql_language = "Select listlanguages.nameLanguages from listlanguages";
            string sql_question = "Select heshtag From questionspattern ";

            List<string> strings = connect.comboBoxAdd(sql_language);
            List<string> strings2 = connect.comboBoxAdd(sql_types);
            List<string> questList = connect.comboBoxAdd(sql_question);

            comboBox_type.Items.Add("Все типы"); 
            comboBox_type.SelectedIndex = 0;
            comboBox_language.Items.Add("Все языки");
            comboBox_language.SelectedIndex = 0;

            foreach (string st in questList)
            {
                quest = quest.Concat(st.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            }
            foreach (string st in strings2)
            {
                comboBox_type.Items.Add(st);
            }
            foreach (string st in strings)
            {
                comboBox_language.Items.Add(st);
            }
            foreach (string st in quest)
            {
                string str = st.Replace(Environment.NewLine, "");
                features.Items.Add(str);
            }
        }
        
        private void search_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                zapros = zapros.Replace("\\", "");

                if (patterns.Count > 0)
                    zapros += ") Group by pat.id_pattern";
                else
                    zapros += " Group by pat.id_pattern";

                listView1.ItemsSource = connect.loaddataBase(zapros);

                if (patterns.Count > 0)
                    zapros = zapros.Replace(") Group by pat.id_pattern", "");
                else
                    zapros = zapros.Replace(" Group by pat.id_pattern", "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //add hashtag
        private void features_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int k = 0;
            for (int i = 0; i < patterns.Count(); i++)
            {
                if (patterns[i].ToString() == features.SelectedItem.ToString())
                    k++;
            }
            if(k==0)
                patterns.Add(new Pattern() { textTag = features.SelectedItem.ToString() });

            if (!Regex.IsMatch(zapros, "heshtag like '%" + features.SelectedItem.ToString() + "%'", RegexOptions.IgnoreCase) && patterns.Count == 1)
                zapros += " and (heshtag like '%" + features.SelectedItem.ToString() + "%'";
            else if (!Regex.IsMatch(zapros, "heshtag like '%" + features.SelectedItem.ToString() + "%'", RegexOptions.IgnoreCase))
            {
                zapros += " or heshtag like '%" + features.SelectedItem.ToString() + "%'";                 
                zapros = zapros.Replace(") or"," or");
            }


            listFeatures.ItemsSource = null;
            listFeatures.ItemsSource = patterns;
        }
        //delete heshtag
        private void listFeatures_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int sel = listFeatures.SelectedIndex;

            if (sel >= 0 && sel < patterns.Count)
            {
                //for (int i = 0; i < patterns.Count(); i++)

                //if (Regex.IsMatch(zapros, "and (heshtag like '%" + listFeatures.SelectedItem.ToString() + "%'", RegexOptions.IgnoreCase))
                string st = "and (heshtag like '%" + listFeatures.SelectedItem.ToString() + "%'";
                    if(zapros.Contains(st))
                        if(patterns.Count == 1)
                            zapros = zapros.Replace("and (heshtag like '%" + listFeatures.SelectedItem.ToString() + "%'", "");
                        else            
                            zapros = zapros.Replace("heshtag like '%" + listFeatures.SelectedItem.ToString() + "%' or ", "");
                    else if (Regex.IsMatch(zapros, "or heshtag like '%" + listFeatures.SelectedItem.ToString() + "%'", RegexOptions.IgnoreCase))
                        zapros = zapros.Replace(" or heshtag like '%" + listFeatures.SelectedItem.ToString() + "%'", "");

                
                
                patterns.RemoveAt(listFeatures.SelectedIndex);

                listFeatures.ItemsSource = null;
                listFeatures.ItemsSource = patterns;
            }
        }
        //add type
        private void comboBox_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBox_type.SelectedItem.ToString() == "Все типы")
                zapros = zapros.Replace("typeName Like '%" + comboBox_type.Text + "%' OR relTypeName Like '%" + comboBox_type.Text + "%'", "typeName Like '%%' OR relTypeName Like '%%'");
            else
                if (comboBox_type.Text =="Все типы")
                    zapros = zapros.Replace("typeName Like '%%' OR relTypeName Like '%%'", "typeName Like '%" + comboBox_type.SelectedItem.ToString() + "%' OR relTypeName Like '%" + comboBox_type.SelectedItem.ToString() + "%'");
                else
                zapros = zapros.Replace("typeName Like '%" + comboBox_type.Text + "%' OR relTypeName Like '%" + comboBox_type.Text + "%'", "typeName Like '%" + comboBox_type.SelectedItem.ToString() + "%' OR relTypeName Like '%" + comboBox_type.SelectedItem.ToString() + "%'");

        }
        //add language
        private void comboBox_language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_language.SelectedItem.ToString() == "Все языки")
                zapros = zapros.Replace("lang.nameLanguages like '%" + comboBox_language.Text + "%'", "lang.nameLanguages like '%%'");
            else
                if (comboBox_language.Text == "Все языки")
                    zapros = zapros.Replace("lang.nameLanguages like '%%'", "lang.nameLanguages like '%" + comboBox_language.SelectedItem.ToString() + "%'");
                else
                    zapros = zapros.Replace("lang.nameLanguages like '%" + comboBox_language.Text + "%'", "lang.nameLanguages like '%" + comboBox_language.SelectedItem.ToString() + "%'");

        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Открытие выбранного элемента
            var CompRow = listView1.Items.GetItemAt(listView1.SelectedIndex) as DataRowView;
            this.NavigationService.Navigate(new openPattern(int.Parse(CompRow[0].ToString())));
        }
    }
}
