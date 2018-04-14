using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// View to add or modify a brand
    /// </summary>
    partial class AddUpdateBrand : Form
    {
        Marques Marque; // The brand to modify or null if the user want to add a new brand
        ControllerView_Brand ControllerBrand;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="ControllerBrand">Controller to use</param>
        /// <param name="Marque">The brand to modify or null if none</param>
        public AddUpdateBrand(ControllerView_Brand ControllerBrand, Marques Marque = null)
        {
            this.ControllerBrand = ControllerBrand;
            this.Marque = Marque;
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
            if (Marque != null) //View Update
            {
                this.Text = "Modification";
                Btn_Valider.Text = "Modifier";
                Tbx_Marque.Text = Marque.Nom;
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

            if (Tbx_Marque.TextLength == 0)
            {
                Graphics graph = Tbx_Marque.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Tbx_Marque.ClientRectangle);
                IsValid = false;
            }
            return IsValid;
        }

        /// <summary>
        /// Add of modify the brand when the user has completed the form
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
                    if (Marque == null)//ajout
                    {
                        NameMessage = "L'ajout ";
                        Marque = new Marques(
                            Tbx_Marque.Text
                        );

                        ControllerBrand.AddElement(Marque);
                    }
                    else//modification
                    {
                        NameMessage = "La modification ";
                        Marque.Nom = Tbx_Marque.Text;

                        ControllerBrand.ChangeElement(Marque);
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
