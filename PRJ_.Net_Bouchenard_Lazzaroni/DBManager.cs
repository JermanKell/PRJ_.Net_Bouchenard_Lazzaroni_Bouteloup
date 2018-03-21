using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    class DBManager
    {
        private SQLiteConnection conn;

        public DBManager()
        {
            conn = DBConnection.getInstance().getDataBase();
        }

        public void insertArticle(Articles article)
        {
            // TODO
        }
    }
}
