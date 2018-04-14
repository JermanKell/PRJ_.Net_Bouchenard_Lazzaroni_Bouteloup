using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// Singleton - Establish the connection with the database
    /// </summary>
    class DBConnection
    {
        private static DBConnection dbm = null;
        private static SQLiteConnection con = null;

        /// <summary>
        /// Constructor of this class
        /// </summary>
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

        /// <summary>
        /// Return the instance of the class
        /// </summary>
        /// <returns> The instance of the class </returns>
        public static DBConnection getInstance()
        {
            if (dbm == null)
            {
                dbm = new DBConnection();
            }
            return dbm;
        }

        /// <summary>
        /// Allow user to get the database
        /// </summary>
        /// <returns>The database</returns>
        public SQLiteConnection getDataBase()
        {
            return con;
        }
    }
}
