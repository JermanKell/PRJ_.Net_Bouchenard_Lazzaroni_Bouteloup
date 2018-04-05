using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class DBConnection
    {
        private static DBConnection dbm = null;
        private static SQLiteConnection con = null;

        private DBConnection()
        {
            try
            {
                con = new SQLiteConnection();
                con.ConnectionString = @"Data Source=Mercure.SQLite; Version=3";
                con.Open();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }

        }

        public static DBConnection getInstance()
        {
            if (dbm == null)
            {
                dbm = new DBConnection();
            }
            return dbm;
        }

        public SQLiteConnection getDataBase()
        {
            return con;
        }
    }
}
