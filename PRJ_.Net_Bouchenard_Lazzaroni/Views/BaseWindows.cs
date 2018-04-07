using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni.Views
{
    abstract partial class BaseWindows : Form
    {
        public BaseWindows()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Init the header of the listView
        /// </summary>
        abstract protected void initHeader();

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        abstract protected void loadDataListView();
    }
}