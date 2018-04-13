﻿using PRJ_.Net_Bouchenard_Lazzaroni.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni.Views
{
    partial class BrandWindows : PRJ_.Net_Bouchenard_Lazzaroni.Views.BaseWindows
    {
        public event EventHandler<MyEventArgs> eventResfreshListView; // Send events to the main to refresh his view
        private ControllerView_Brand controller;

        public BrandWindows()
        {
            InitializeComponent();
            controller = new ControllerView_Brand();

            InitHeader(); // Init header of the listView
            GroupsListView = new List<Hashtable>();
            refreshOwnView();
        }

        /// <summary>
        /// Init the header of the listView
        /// </summary>
        protected override void InitHeader()
        {
            //initialise columns
            List<string> listNameColumnTable = controller.getColumnHeader();

            for (int i = 0; i < listNameColumnTable.Count; i++)
            {
                ColumnHeader colHdr = new ColumnHeader();
                colHdr.Name = listNameColumnTable.ElementAt(i); //Set a ColumnHeader name 
                colHdr.Text = listNameColumnTable.ElementAt(i);
                colHdr.Width = listView1.Size.Width / listNameColumnTable.Count;
                listView1.Columns.Add(colHdr);
            }
        }

        /// <summary>
        /// Load the data into the listView
        /// </summary>
        protected override void LoadDataListView()
        {
            listView1.Items.Clear();

            foreach (KeyValuePair<int, Marques> familles in controller.getAllMarques())
            {
                ListViewItem item = new ListViewItem(new string[] {

                    familles.Value.Id.ToString(),
                    familles.Value.Nom,
                });
                item.Name = familles.Key.ToString();    //Set reference as item name
                listView1.Items.Add(item);
            }
        }

        /// <summary>
        /// Delete an object from modal window
        /// </summary>
        protected override void DeleteObjectListView()
        {
            eventResfreshListView(this, argsEvent); // Send the event
        }

        /// <summary>
        /// Add an object from modal window
        /// </summary>
        protected override void AddObjectListView()
        {
            eventResfreshListView(this, argsEvent); // Send the event
        }

        /// <summary>
        /// Update an object from modal window
        /// </summary>
        protected override void UpdateObjectListView()
        {
            eventResfreshListView(this, argsEvent); // Send the event
        }
    }
}
