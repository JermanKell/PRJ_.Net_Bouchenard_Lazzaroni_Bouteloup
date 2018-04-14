using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    partial class VueArticle : Form
    {
        Articles Article;
        ControllerViewArticle ControllerArticles;
        Dictionary<int, string> DictionaryFamilles;
        Dictionary<int, string> DictionarySousFamilles;
        Dictionary<int, string> DictionaryMarques;

        public VueArticle(ControllerViewArticle ControllerArticles, Articles Article = null)
        {
            this.ControllerArticles = ControllerArticles;
            this.Article = Article;
            InitializeComponent();
            InitializeGraphics();
        }

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

        private void InitializeCbxFamilles()
        {
            // --- To Move ---//
            DBManager dbm = new DBManager();
            //////////////////////////

            Cbx_Famille.Items.Clear();
            DictionaryFamilles = dbm.getAllFamilles().ToDictionary(x => x.Key, x => x.Value.Nom);
            if(DictionaryFamilles.Count > 0)
            {
                Cbx_Famille.DataSource = new BindingSource(DictionaryFamilles, null);
                Cbx_Famille.DisplayMember = "Value";
                Cbx_Famille.ValueMember = "Key";
            }
            Cbx_Famille.SelectedIndex = -1;
        }

        private void InitializeCbxSousFamilles()
        {
            // --- To Move ---//
            DBManager dbm = new DBManager();
            //////////////////////////

            Cbx_SousFamille.Items.Clear();
            DictionarySousFamilles = dbm.getAllSousFamilles().ToDictionary(x => x.Key, x => x.Value.Nom);
            if(DictionarySousFamilles.Count > 0)
            {
                Cbx_SousFamille.DataSource = new BindingSource(DictionarySousFamilles, null);
                Cbx_SousFamille.DisplayMember = "Value";
                Cbx_SousFamille.ValueMember = "Key";
            }
            Cbx_SousFamille.SelectedIndex = -1;
        }

        private void InitializeCbxMarques()
        {
            // --- To Move ---//
            DBManager dbm = new DBManager();
            //////////////////////////

            Cbx_Marque.Items.Clear();   
            DictionaryMarques = dbm.getAllMarques().ToDictionary(x => x.Key, x => x.Value.Nom);
            if(DictionaryMarques.Count > 0)
            {
                Cbx_Marque.DataSource = new BindingSource(DictionaryMarques, null);
                Cbx_Marque.DisplayMember = "Value";
                Cbx_Marque.ValueMember = "Key";
            }
            Cbx_Marque.SelectedIndex = -1;
        }

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
                    MessageBox.Show(NameMessage + "de l'article de référence " + Tbx_Reference.Text + " a bien été fait", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
