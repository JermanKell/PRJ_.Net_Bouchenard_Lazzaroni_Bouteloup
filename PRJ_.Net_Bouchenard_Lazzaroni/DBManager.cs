using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class DBManager
    {
        private static DBManager dbm = null;
        private static SQLiteConnection con = null;

        private DBManager()
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

        public static DBManager getInstance()
        {
            if (dbm == null)
            {
                dbm = new DBManager();
            }
            return dbm;
        }

        public SQLiteConnection getDataBase()
        {
            return con;
        }
    }
}
