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

            Btn_Valider.DialogResult = DialogResult.OK;
            Btn_Annuler.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Fill all graphical component
        /// </summary>
        private void InitializeGraphics()
        {
            if (SubFamily != null) //View Update
            {
                this.Text = "Modification";
                Btn_Valider.Text = "Modifier";

                // REMPLIR ICI TOUTES LES FAMILLES ET METTRE LA SELECTION SUR SA FAMILLE ACTUELLE

                Tbx_Famille.Text = SubFamily.Nom;
            }
            else //View Insert
            {
                this.Text = "Ajout";
                Btn_Valider.Text = "Ajouter";
            }
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
                Graphics graph = Tbx_Famille.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Tbx_Famille.ClientRectangle);
                IsValid = false;
            }
            return IsValid;
        }

        /// <summary>
        /// Add of modify the sub family when the user has completed the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Valider_Click(object sender, EventArgs e)
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
                        //SubFamily = new SousFamilles(
                          //  Tbx_Famille.Text
                        //);

                        ControllerSubFamilly.AddElement(SubFamily);
                    }
                    else//modification
                    {
                        NameMessage = "La modification ";
                        SubFamily.Nom = Tbx_Famille.Text;

                        ControllerSubFamilly.ChangeElement(SubFamily);
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    this.DialogResult = DialogResult.Cancel;
                    MessageBox.Show("Une erreur est survenue lors de " + NameMessage.ToLower() + "avec le message suivant:\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Cancel and close the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
