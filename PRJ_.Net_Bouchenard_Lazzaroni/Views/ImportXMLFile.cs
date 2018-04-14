using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// View to parse XML file 
    /// </summary>
    public partial class ImportXMLFile : Form
    {
        private string filename; // Path of the xml file

        /// <summary>
        /// Constructor per default
        /// </summary>
        public ImportXMLFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Open dialog to select the xml file
        /// </summary>
        /// <param name="sender"> Mandadory because receive event </param>
        /// <param name="e"> Param of the event </param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            DocOpen_Window = new OpenFileDialog();
            DocOpen_Window.Filter = "XML Files (.xml) | *.xml";
            DocOpen_Window.FilterIndex = 1;
            DocOpen_Window.Title = "Select an XML File to parse";
            DocOpen_Window.RestoreDirectory = true;

            if (DocOpen_Window.ShowDialog() == DialogResult.OK)
            {
                filename = DocOpen_Window.FileName; // Full path of the file
                lab_FName.Text = DocOpen_Window.SafeFileName;
            }
        }

        /// <summary>
        /// Parse the file selected
        /// </summary>
        /// <param name="sender"> Mandadory because receive event </param>
        /// <param name="e"> Argument of the event </param>
        private void btnIntegrate_Click(object sender, EventArgs e)
        {
            listView.Items.Clear(); // Remove all items

            if (lab_FName.Text.CompareTo("") != 0) // If no file has selected
            {
                ControllerParserXML controllerParser;
                progressBar.Value = 0; // Reset the position of the bar

                if (Update_XML.Checked == true)
                {
                    controllerParser = new ControllerParserXMLUpdate(filename);
                    controllerParser.eventUpdateListView += updateListView; // Event ListView
                    controllerParser.eventUpdateProgressBar += updateProgressBar; // Event ProgressBar
                    controllerParser.eventRangeMaxProgressBar += updateMaxRangeProgressBar; // Event max range ProgressBar
                    controllerParser.parse();
                }
                if (Integration_XML.Checked == true)
                {
                    controllerParser = new ControllerParserXMLAdd(filename);
                    controllerParser.eventUpdateListView += updateListView; // Event ListView
                    controllerParser.eventUpdateProgressBar += updateProgressBar; // Event ProgressBar
                    controllerParser.eventRangeMaxProgressBar += updateMaxRangeProgressBar; // Event max range ProgressBar
                    controllerParser.parse();
                }
            }
        }

        /// <summary>
        /// Signal send by controller to add text line on the list view
        /// </summary>
        /// <param name="sender"> Mandatory because receive event </param>
        /// <param name="e"> Argument of the event </param>
        void updateListView(object sender, MyEventArgs e)
        {
            ListViewItem listViewItem = new ListViewItem(new[] { e.message, e.type.ToString(), e.subject.ToString()});

            if (e.type == TypeMessage.Succès)
                listViewItem.ForeColor = Color.Green;
            else if (e.type == TypeMessage.Avertissement)
                listViewItem.ForeColor = Color.Brown;
            else if (e.type == TypeMessage.Erreur)
                listViewItem.ForeColor = Color.Red;
            else
            {
                listViewItem.ForeColor = Color.Black;
                listViewItem.BackColor = Color.Red;
            }

            listView.Items.Add(listViewItem);
            listView.Refresh();

            listView.EnsureVisible(listView.Items.Count - 1); // Auto scroll down
        }

        /// <summary>
        /// Signal send by controller to update the progress bar
        /// </summary>
        /// <param name="sender"> Mandatory because receive event </param>
        /// <param name="e"> Argument of the event </param>
        void updateProgressBar(object sender, MyEventArgs e)
        {
            progressBar.PerformStep();
        }

        /// <summary>
        /// Signal send by controller to set the max range of the progress bar
        /// </summary>
        /// <param name="sender"> Mandatory because receive event </param>
        /// <param name="e"> Argument of the event </param>
        void updateMaxRangeProgressBar(object sender, MyEventArgs e)
        {
            progressBar.Maximum = e.maxRange;
        }
    }
}
