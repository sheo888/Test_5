using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace book
{
    class Bas
    {
        public static MySqlConnection Connect = new MySqlConnection("server=localhost; userid = root; password = ; database= nig; port =3360; persistsecurityinfo=true");

        public string log = "SELECT * FROM users where login=";
        public string pas = "SELECT * FROM users where id=";
        public string rol = "SELECT * FROM workers WHERE id=";

        public void Exi()
        {
            //MainWindow mainWindow = new MainWindow();
            //this.Close();
            //mainWindow.Show();
        }
    }
}
