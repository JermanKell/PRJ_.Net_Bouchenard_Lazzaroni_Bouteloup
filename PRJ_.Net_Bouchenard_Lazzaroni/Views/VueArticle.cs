using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    public partial class VueArticle : Form
    {
        Articles Article;
        Dictionary<int, string> DictionaryFamilles;
        Dictionary<int, string> DictionarySousFamilles;
        Dictionary<int, string> DictionaryMarques;

        public VueArticle(Articles Article = null)
        {
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
                Lab_Title.Text = "Mise à jour";
                Btn_Valider.Text = "Modifier";

                Tbx_Reference.Text = Article.Reference;
                Tbx_Description.Text = Article.Description;
                Tbx_Prix.Text = Article.PrixHT.ToString();
                Tbx_Quantite.Text = Article.Quantite.ToString();

                Tbx_Reference.Enabled = false;

                KeyValuePair<int, string> PairFamille = new KeyValuePair<int, string>(2, "Ecriture & Correction" /*Article.IdFamille, DictionaryFamilles[Article.IdFamille]*/);
                Cbx_Famille.SelectedIndex = Cbx_Famille.Items.IndexOf(PairFamille);

                KeyValuePair<int, string> PairSousFamille = new KeyValuePair<int, string>(Article.IdSousFamille, DictionarySousFamilles[Article.IdSousFamille]);
                Cbx_SousFamille.SelectedIndex = Cbx_SousFamille.Items.IndexOf(PairSousFamille);

                KeyValuePair<int, string> PairMarque = new KeyValuePair<int, string>(Article.IdMarque, DictionaryMarques[Article.IdMarque]);
                Cbx_Marque.SelectedIndex = Cbx_Marque.Items.IndexOf(PairMarque);
            }
            else //View Insert
            {
                Lab_Title.Text = "Nouvel article";
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
            DictionaryFamilles = dbm.getAllFamilles().ToDictionary(x => x.Id, x => x.Nom);
            //dictionaryFamilles.Add(2, "un");
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
            DictionarySousFamilles = dbm.getAllSousFamilles().ToDictionary(x => x.Id, x => x.Nom);
            //dictionarySousFamilles.Add(2, "un");
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
            DictionaryMarques = dbm.getAllMarques().ToDictionary(x => x.Id, x => x.Nom);
            //dictionaryMarques.Add(2, "un");
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

            if(Cbx_Famille.SelectedIndex == -1)
            {
                /*Graphics graph = Cbx_Famille.CreateGraphics();
                Pen pen = new Pen(Brushes.Red, 2.0f);
                graph.DrawRectangle(pen, Cbx_Famille.ClientRectangle);*/
            }
            return IsValid;
        }

        private void Btn_Valider_Click(object sender, EventArgs e)
        {
            if(Cbx_Famille.SelectedIndex != -1)
            {
                int key = ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Key;
                string value = ((KeyValuePair<int, string>)Cbx_Famille.SelectedItem).Value;
                MessageBox.Show(key + "   " + value);
            }
        }
    }
}
