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
    partial class AddUpdateBrand : Form
    {
        Marques Marque;
        ControllerView_Brand ControllerBrand;

        public AddUpdateBrand(ControllerView_Brand ControllerBrand, Marques Marque = null)
        {
            this.ControllerBrand = ControllerBrand;
            this.Marque = Marque;
            InitializeComponent();
            InitializeGraphics();
        }

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
                    MessageBox.Show(NameMessage + "de la marque " + Tbx_Marque.Text + " a bien été fait", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Une erreur est survenue lors de " + NameMessage.ToLower() + "avec le message suivant:\n" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
