using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.IO;


namespace ARMRBT
{
    public class Database
    {
        public string Server { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NameDatabase { get; set; }

        public MySqlConnection mysqlconnection;
        public MySqlCommand mysqlcommand;
        public MySqlDataAdapter mysqladapter;

        public string NetConnectionString
        {
            get
            {
                return string.Format("Server={0};Uid={1};Pwd={2};Database={3};charset=utf8", Server, Username, Password, NameDatabase);
            }
        }

        public bool OpenConnect()
        {
            mysqlconnection = new MySqlConnection(NetConnectionString);

            try
            {
                mysqlconnection.Open();
                return true;
            }
            catch
            {
                MessageBox.Show("Неправильно введены логин или пароль!", "Ошибка!");
                return false;
            }
        }

        public DataTable SelectQuery(string query)
        {
            DataTable dataTable = new DataTable();
            mysqlcommand = new MySqlCommand(query, mysqlconnection);
            mysqladapter = new MySqlDataAdapter(mysqlcommand);
            mysqladapter.Fill(dataTable);
            return dataTable;
        }

        public void Query(string query)
        {
            try
            {
                OpenConnect();
                mysqlcommand = new MySqlCommand(query, mysqlconnection);
                mysqlcommand.ExecuteNonQuery();
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK);
            }
        }

        public Database(string server, string username, string password, string namedatabase = "remontbytovoytekhniki")
        {
            this.Server = server;
            this.Username = username;
            this.Password = password;
            this.NameDatabase = namedatabase;
        }
    }
}
