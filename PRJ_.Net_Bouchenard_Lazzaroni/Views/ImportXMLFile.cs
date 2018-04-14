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
        private string Filename; // Path of the xml file

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
        /// <param name="Sender"> Mandadory because receive event </param>
        /// <param name="E"> Param of the event </param>
        private void BtnOpenFile_Click(object Sender, EventArgs E)
        {
            DocOpen_Window = new OpenFileDialog();
            DocOpen_Window.Filter = "XML Files (.xml) | *.xml";
            DocOpen_Window.FilterIndex = 1;
            DocOpen_Window.Title = "Select an XML File to parse";
            DocOpen_Window.RestoreDirectory = true;

            if (DocOpen_Window.ShowDialog() == DialogResult.OK)
            {
                Filename = DocOpen_Window.FileName; // Full path of the file
                Lab_FName.Text = DocOpen_Window.SafeFileName;
            }
        }

        /// <summary>
        /// Parse the file selected
        /// </summary>
        /// <param name="Sender"> Mandadory because receive event </param>
        /// <param name="E"> Argument of the event </param>
        private void BtnIntegrate_Click(object Sender, EventArgs E)
        {
            ListView.Items.Clear(); // Remove all items

            if (Lab_FName.Text.CompareTo("") != 0) // If no file has selected
            {
                ControllerParserXML ControllerParser;
                ProgressBar.Value = 0; // Reset the position of the bar

                if (Update_XML.Checked == true)
                {
                    ControllerParser = new ControllerParserXMLUpdate(Filename);
                    ControllerParser.EventUpdateListView += UpdateListView; // Event ListView
                    ControllerParser.EventUpdateProgressBar += UpdateProgressBar; // Event ProgressBar
                    ControllerParser.EventRangeMaxProgressBar += UpdateMaxRangeProgressBar; // Event max range ProgressBar
                    ControllerParser.Parse();
                }
                if (Integration_XML.Checked == true)
                {
                    ControllerParser = new ControllerParserXMLAdd(Filename);
                    ControllerParser.EventUpdateListView += UpdateListView; // Event ListView
                    ControllerParser.EventUpdateProgressBar += UpdateProgressBar; // Event ProgressBar
                    ControllerParser.EventRangeMaxProgressBar += UpdateMaxRangeProgressBar; // Event max range ProgressBar
                    ControllerParser.Parse();
                }
            }
        }

        /// <summary>
        /// Signal send by controller to add text line on the list view
        /// </summary>
        /// <param name="Sender"> Mandatory because receive event </param>
        /// <param name="E"> Argument of the event </param>
        void UpdateListView(object Sender, MyEventArgs E)
        {
            ListViewItem ListViewItem = new ListViewItem(new[] { E.Message, E.Type.ToString(), E.Subject.ToString()});

            if (E.Type == TypeMessage.Succès)
                ListViewItem.ForeColor = Color.Green;
            else if (E.Type == TypeMessage.Avertissement)
                ListViewItem.ForeColor = Color.Brown;
            else if (E.Type == TypeMessage.Erreur)
                ListViewItem.ForeColor = Color.Red;
            else
            {
                ListViewItem.ForeColor = Color.Black;
                ListViewItem.BackColor = Color.Red;
            }

            ListView.Items.Add(ListViewItem);
            ListView.Refresh();

            ListView.EnsureVisible(ListView.Items.Count - 1); // Auto scroll down
        }

        /// <summary>
        /// Signal send by controller to update the progress bar
        /// </summary>
        /// <param name="Sender"> Mandatory because receive event </param>
        /// <param name="E"> Argument of the event </param>
        void UpdateProgressBar(object Sender, MyEventArgs E)
        {
            ProgressBar.PerformStep();
        }

        /// <summary>
        /// Signal send by controller to set the max range of the progress bar
        /// </summary>
        /// <param name="Sender"> Mandatory because receive event </param>
        /// <param name="E"> Argument of the event </param>
        void UpdateMaxRangeProgressBar(object Sender, MyEventArgs E)
        {
            ProgressBar.Maximum = E.MaxRange;
        }
    }
}
