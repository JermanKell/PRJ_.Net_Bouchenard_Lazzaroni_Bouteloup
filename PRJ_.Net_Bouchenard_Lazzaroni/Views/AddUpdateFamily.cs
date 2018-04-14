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
    /// View to add or modify a family
    /// </summary>
    partial class AddUpdateFamily : Form
    {
        Familles Famille; // The family to modify or null if the user want to add a new family
        ControllerView_PFamily ControllerFamilly;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="ControllerFamilly">Controller to use</param>
        /// <param name="Famille">The family to modify or null if none</param>
        public AddUpdateFamily(ControllerView_PFamily ControllerFamilly, Familles Famille = null)
        {
            this.ControllerFamilly = ControllerFamilly;
            this.Famille = Famille;
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
            if (Famille != null) //View Update
            {
                this.Text = "Modification";
                Btn_Valider.Text = "Modifier";
                Tbx_Famille.Text = Famille.Nom;
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
        /// Add of modify the family when the user has completed the form
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
                    if (Famille == null)//ajout
                    {
                        NameMessage = "L'ajout ";
                        Famille = new Familles(
                            Tbx_Famille.Text
                        );

                        ControllerFamilly.AddElement(Famille);
                    }
                    else//modification
                    {
                        NameMessage = "La modification ";
                        Famille.Nom = Tbx_Famille.Text;

                        ControllerFamilly.ChangeElement(Famille);
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
