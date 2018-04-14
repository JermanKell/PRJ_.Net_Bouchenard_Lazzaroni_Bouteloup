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
    /// <summary>
    /// View to add or modify a sub family
    /// </summary>
    partial class AddUpdateSubFamily : Form
    {
        SousFamilles SubFamily; // The sub family to modify or null if the user want to add a new sub family
        ControllerView_SubFamily ControllerSubFamilly;
        Dictionary<int, string> DictionaryFamilies;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="ControllerSubFamilly">Controller to use</param>
        /// <param name="SubFamily">The sub family to modify or null if none</param>
        public AddUpdateSubFamily(ControllerView_SubFamily ControllerSubFamilly, SousFamilles SubFamily = null)
        {
            this.ControllerSubFamilly = ControllerSubFamilly;
            this.SubFamily = SubFamily;
            InitializeComponent();
            InitializeGraphics();
        }

        /// <summary>
        /// Fill all graphical component
        /// </summary>
        private void InitializeGraphics()
        {
            InitializeCbxFamilies();

            if (SubFamily != null) //View Update
            {
                this.Text = "Modification";
                Btn_Valider.Text = "Modifier";

                KeyValuePair<int, string> PairFamily = new KeyValuePair<int, string>(SubFamily.IdFamille, DictionaryFamilies[SubFamily.IdFamille]);
                Cbx_Famille.SelectedIndex = Cbx_Famille.Items.IndexOf(PairFamily);

                Tbx_Famille.Text = SubFamily.Nom;
            }
            else //View Insert
            {
                this.Text = "Ajout";
                Btn_Valider.Text = "Ajouter";
            }
        }

        /// <summary>
        /// Initialize the combobox family by inserting the names
        /// </summary>
        private void InitializeCbxFamilies()
        {
            Cbx_Famille.Items.Clear();
            DictionaryFamilies = ControllerSubFamilly.GetAllFamilies().ToDictionary(x => x.Key, x => x.Value.Nom);
            if (DictionaryFamilies.Count > 0)
            {
                Cbx_Famille.DataSource = new BindingSource(DictionaryFamilies, null);
                Cbx_Famille.DisplayMember = "Value";
                Cbx_Famille.ValueMember = "Key";
            }
            Cbx_Famille.SelectedIndex = -1;
        }

        /// <summary>
        /// Check if all entries has been completed
        /// </summary>
        /// <returns>True if ok, else false</returns>
        private bool CheckEntries()
        {
            bool IsValid = true;

            if (Tbx_Famille.TextLength == 0)
            {
                Graphics Graph = Tbx_Famille.CreateGraphics();
                Pen Pen = new Pen(Brushes.Red, 2.0f);
                Graph.DrawRectangle(Pen, Tbx_Famille.ClientRectangle);
                IsValid = false;
            }
            if (Cbx_Famille.SelectedIndex == -1)
            {
                Graphics graph = Cbx_Famille.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Cbx_Famille.ClientRectangle);
                IsValid = false;
            }
            return IsValid;
        }

        /// <summary>
        /// Add of modify the sub family when the user has completed the form
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void Btn_Valider_Click(object Sender, EventArgs E)
        {
            if (!CheckEntries())
            {
                MessageBox.Show("Certains champs ne sont pas valides !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string NameMessage = "";
                try
                {
                    if (SubFamily == null)//ajout
                    {
                        NameMessage = "L'ajout ";
                        SubFamily = new SousFamilles(
                            ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key,
                            Tbx_Famille.Text
                        );

                        ControllerSubFamilly.AddElement(SubFamily);
                        this.DialogResult = DialogResult.OK;
                    }
                    else//modification
                    {
                        NameMessage = "La modification ";
                        SubFamily.Nom = Tbx_Famille.Text;
                        SubFamily.IdFamille = ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key;
                        ControllerSubFamilly.ChangeElement(SubFamily);
                        this.DialogResult = DialogResult.OK;
                    }
                    this.Close();
                }
                catch (Exception Ex)
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("Une erreur est survenue lors de " + NameMessage.ToLower() + "avec le message suivant:\n" + Ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void cancelButton_Click(object Sender, EventArgs E)
        {
            this.Close();
        }
    }
}
