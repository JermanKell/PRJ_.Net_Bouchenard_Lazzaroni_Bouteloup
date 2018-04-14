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
        private static DBConnection Dbm = null;
        private static SQLiteConnection Con = null;

        /// <summary>
        /// Constructor of this class
        /// </summary>
        private DBConnection()
        {
            try
            {
                Con = new SQLiteConnection();
                Con.ConnectionString = @"Data Source=Mercure.SQLite; Version=3";
                Con.Open();
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message.ToString());
            }

        }

        /// <summary>
        /// Return the instance of the class
        /// </summary>
        /// <returns> The instance of the class </returns>
        public static DBConnection GetInstance()
        {
            if (Dbm == null)
            {
                Dbm = new DBConnection();
            }
            return Dbm;
        }

        /// <summary>
        /// Allow user to get the database
        /// </summary>
        /// <returns>The database</returns>
        public SQLiteConnection GetDataBase()
        {
            return Con;
        }
    }
}
