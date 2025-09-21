using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PatternSelection.pageList.searchPattern;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Runtime.Remoting.Messaging;
using System.Windows.Controls;

namespace PatternSelection
{
    internal class mainClass
    {
        //old static string connectionString = "server=sql.freedb.tech;database=freedb_diplom;username=freedb_polzovatel;password=Vb44TA*mrZYT5PS";
        static string connectionString = "server=sql.freedb.tech;database=freedb_diploms;username=freedb_polzovatel;password=*SmXR3M*A8JK7?e";
        MySqlConnection conn = new MySqlConnection(connectionString);
        public DataView loaddataBase(string sql)
        {
            try
            {
                DataTable dt = new DataTable();

                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                da.SelectCommand = new MySqlCommand(sql, conn);

                da.Fill(dt);

                return dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public List<string> comboBoxAdd(string sql)
        {
            DataSet dt = new DataSet();

            MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);

            da.SelectCommand = new MySqlCommand(sql, conn);

            da.Fill(dt);

            List<string> list = new List<string>();

            foreach (DataRow data in dt.Tables[0].Rows)
            {
                list.Add(data[0].ToString());
            }

            return list;
        }

        public void insertInTable(string namePat,string nameLang,string text, string textDes)
        {
            try
            {
                text = text.Replace($"'", $"\\'");
                textDes = textDes.Replace($"'", $"\\'");

                conn.Open();
                string sql = $"INSERT INTO example(id_lang, id_patern, exampleText, exampleDescript) SELECT* FROM((SELECT listlanguages.id_lang FROM listlanguages WHERE listlanguages.nameLanguages = '{nameLang}') as tmp1, (SELECT patterns.id_pattern FROM patterns WHERE patterns.namePattern = '{namePat}') as tmp2,(Select  '{text}') as frha,(Select  '{textDes}') as frhaf) WHERE NOT EXISTS(SELECT id_lang, id_patern FROM example WHERE id_lang = (SELECT listlanguages.id_lang FROM listlanguages WHERE listlanguages.nameLanguages = '{nameLang}') AND id_patern = (SELECT patterns.id_pattern FROM patterns WHERE patterns.namePattern = '{namePat}')) LIMIT 1;";
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();
            }
        }
        public void editInTable(string namePat, string nameLang, string text,string textDes)
        {
            try
            {
                text = text.Replace($"'", $"\\'");
                textDes = textDes.Replace($"'", $"\\'");

                conn.Open();
                string sql = "UPDATE example Set exampleText = '"+ text + "', exampleDescript = '"+ textDes + "' " +
                    "WHERE id_lang = (Select id_lang from listlanguages where nameLanguages = '" + nameLang+"') AND " +
                    "id_patern = (Select id_pattern from patterns where namePattern = '"+namePat+"')";
                MySqlCommand command = new MySqlCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
                conn.Close();
            }
        }

        public string selectContent(string sql)
        {
            try
            {
                conn.Open();

                MySqlCommand command = new MySqlCommand(sql, conn);

                string res = command.ExecuteScalar().ToString();

                conn.Close();
                return res;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                conn.Close();   
                return null;
            }
        }
    }
}
