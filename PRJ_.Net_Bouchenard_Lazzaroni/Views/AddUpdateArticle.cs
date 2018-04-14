using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    /// <summary>
    /// View to add or modify an article
    /// </summary>
    partial class AddUpdateArticle : Form
    {
        Articles Article; // The article to modify or null if the user want to add a new article
        ControllerViewArticle ControllerArticles;
        Dictionary<int, string> DictionaryFamilles; // Useful for combobox
        Dictionary<int, string> DictionarySousFamilles; // Useful for combobox
        Dictionary<int, string> DictionaryMarques; // Useful for combobox

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="ControllerArticles">Controller to use</param>
        /// <param name="Article">The article to modify or null if none</param>
        public AddUpdateArticle(ControllerViewArticle ControllerArticles, Articles Article = null)
        {
            this.ControllerArticles = ControllerArticles;
            this.Article = Article;
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
            InitializeCbxFamilles();
            InitializeCbxSousFamilles();
            InitializeCbxMarques();


            if (Article != null) //View Update
            {
                this.Text = "Modification";
                Btn_Valider.Text = "Modifier";

                Tbx_Reference.Text = Article.Reference;
                Tbx_Description.Text = Article.Description;
                Tbx_Prix.Text = Article.PrixHT.ToString();
                Tbx_Quantite.Text = Article.Quantite.ToString();

                Tbx_Reference.Enabled = false;

                KeyValuePair<int, string> PairFamille = new KeyValuePair<int, string>(Article.IdFamille, DictionaryFamilles[Article.IdFamille]);
                Cbx_Famille.SelectedIndex = Cbx_Famille.Items.IndexOf(PairFamille);

                KeyValuePair<int, string> PairSousFamille = new KeyValuePair<int, string>(Article.IdSousFamille, DictionarySousFamilles[Article.IdSousFamille]);
                Cbx_SousFamille.SelectedIndex = Cbx_SousFamille.Items.IndexOf(PairSousFamille);

                KeyValuePair<int, string> PairMarque = new KeyValuePair<int, string>(Article.IdMarque, DictionaryMarques[Article.IdMarque]);
                Cbx_Marque.SelectedIndex = Cbx_Marque.Items.IndexOf(PairMarque);
            }
            else //View Insert
            {
                this.Text = "Ajout";
                Btn_Valider.Text = "Ajouter";

                Tbx_Reference.Text = "F";
            }      
        }

        /// <summary>
        /// Init the family dictionnary for the combo box
        /// </summary>
        private void InitializeCbxFamilles()
        {
            Cbx_Famille.Items.Clear();
            DictionaryFamilles = ControllerArticles.GetAllFamilles().ToDictionary(x => x.Key, x => x.Value.Nom);
            if(DictionaryFamilles.Count > 0)
            {
                Cbx_Famille.DataSource = new BindingSource(DictionaryFamilles, null);
                Cbx_Famille.DisplayMember = "Value";
                Cbx_Famille.ValueMember = "Key";
            }
            Cbx_Famille.SelectedIndex = -1;
        }

        /// <summary>
        /// Init the sub family dictionnary for the combo box
        /// </summary>
        private void InitializeCbxSousFamilles()
        {
            Cbx_SousFamille.Items.Clear();
            DictionarySousFamilles = ControllerArticles.GetAllSousFamilles().ToDictionary(x => x.Key, x => x.Value.Nom);
            if(DictionarySousFamilles.Count > 0)
            {
                Cbx_SousFamille.DataSource = new BindingSource(DictionarySousFamilles, null);
                Cbx_SousFamille.DisplayMember = "Value";
                Cbx_SousFamille.ValueMember = "Key";
            }
            Cbx_SousFamille.SelectedIndex = -1;
        }

        /// <summary>
        /// Init the brand dictionnary for the combo box
        /// </summary>
        private void InitializeCbxMarques()
        {
            Cbx_Marque.Items.Clear();   
            DictionaryMarques = ControllerArticles.GetAllMarques().ToDictionary(x => x.Key, x => x.Value.Nom);
            if(DictionaryMarques.Count > 0)
            {
                Cbx_Marque.DataSource = new BindingSource(DictionaryMarques, null);
                Cbx_Marque.DisplayMember = "Value";
                Cbx_Marque.ValueMember = "Key";
            }
            Cbx_Marque.SelectedIndex = -1;
        }

        /// <summary>
        /// Check if all entries has been completed
        /// </summary>
        /// <returns>True if ok, else false</returns>
        private bool CheckEntries()
        {
            bool IsValid = true;

            if (Tbx_Reference.TextLength == 0)
            {
                Graphics graph = Tbx_Reference.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Tbx_Reference.ClientRectangle);
                IsValid = false;
            }

            if (Cbx_Famille.SelectedIndex == -1)
            {
                Graphics graph = Cbx_Famille.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Cbx_Famille.ClientRectangle);
                IsValid = false;
            }

            if (Cbx_SousFamille.SelectedIndex == -1)
            {
                Graphics graph = Cbx_SousFamille.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Cbx_SousFamille.ClientRectangle);
                IsValid = false;
            }

            if (Cbx_Marque.SelectedIndex == -1)
            {
                Graphics graph = Cbx_Marque.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Cbx_Marque.ClientRectangle);
                IsValid = false;
            }

            if (Tbx_Prix.TextLength == 0)
            {
                Graphics graph = Tbx_Prix.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Tbx_Prix.ClientRectangle);
                IsValid = false;
            }

            if (Tbx_Quantite.TextLength == 0)
            {
                Graphics graph = Tbx_Quantite.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Tbx_Quantite.ClientRectangle);
                IsValid = false;
            }

            return IsValid;
        }

        /// <summary>
        /// Add of modify the article when the user has completed the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Valider_Click(object sender, EventArgs e)
        {
            if(!CheckEntries())
            {
                MessageBox.Show("Certains champs ne sont pas valides !", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string NameMessage = "";
                try
                {
                    if (Article == null)//ajout
                    {
                        NameMessage = "L'ajout ";
                        Article = new Articles(
                            Tbx_Reference.Text,
                            Tbx_Description.Text,
                            ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key,
                            ((KeyValuePair<int, string>)Cbx_SousFamille.SelectedItem).Key,
                            ((KeyValuePair<int, string>)Cbx_Marque.SelectedItem).Key,
                            Convert.ToDouble(Tbx_Prix.Text),
                            Convert.ToInt32(Tbx_Quantite.Text)
                        );

                        ControllerArticles.AddElement(Article);
                    }
                    else//modification
                    {
                        NameMessage = "La modification ";
                        Article.Description = Tbx_Description.Text;
                        Article.IdFamille = ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key;
                        Article.IdSousFamille = ((KeyValuePair<int, string>)Cbx_SousFamille.SelectedItem).Key;
                        Article.IdMarque = ((KeyValuePair<int, string>)Cbx_Marque.SelectedItem).Key;
                        Article.PrixHT = Convert.ToDouble(Tbx_Prix.Text);
                        Article.Quantite = Convert.ToInt32(Tbx_Quantite.Text);

                        ControllerArticles.ChangeElement(Article);
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
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
