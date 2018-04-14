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
    partial class AddUpdateSubFamily : Form
    {
        SousFamilles SubFamily;
        ControllerView_SubFamily ControllerSubFamilly;
        Dictionary<int, string> DictionaryFamilies;

        public AddUpdateSubFamily(ControllerView_SubFamily ControllerSubFamilly, SousFamilles SubFamily = null)
        {
            this.ControllerSubFamilly = ControllerSubFamilly;
            this.SubFamily = SubFamily;
            InitializeComponent();
            InitializeGraphics();

            Btn_Valider.DialogResult = DialogResult.OK;
            Btn_Annuler.DialogResult = DialogResult.Cancel;
        }

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
            DictionaryFamilies = ControllerSubFamilly.getAllFamilies().ToDictionary(x => x.Key, x => x.Value.Nom);
            if (DictionaryFamilies.Count > 0)
            {
                Cbx_Famille.DataSource = new BindingSource(DictionaryFamilies, null);
                Cbx_Famille.DisplayMember = "Value";
                Cbx_Famille.ValueMember = "Key";
            }
            Cbx_Famille.SelectedIndex = -1;
        }

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
            if (Cbx_Famille.SelectedIndex == -1)
            {
                Graphics graph = Cbx_Famille.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Cbx_Famille.ClientRectangle);
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
                    if (SubFamily == null)//ajout
                    {
                        NameMessage = "L'ajout ";
                        SubFamily = new SousFamilles(
                            ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key,
                            Tbx_Famille.Text
                        );

                        ControllerSubFamilly.AddElement(SubFamily);
                    }
                    else//modification
                    {
                        NameMessage = "La modification ";
                        SubFamily.Nom = Tbx_Famille.Text;
                        SubFamily.IdFamille = ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key;
                        MessageBox.Show(SubFamily.Id.ToString());
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

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
