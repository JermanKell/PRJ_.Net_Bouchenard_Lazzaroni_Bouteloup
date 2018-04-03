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
    public partial class VueArticle : Form
    {
        Articles Article;

        public VueArticle(Articles article = null)
        {
            Article = article;
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

            }
            else //View Insert
            {
                Lab_Title.Text = "Nouvel article";
                Btn_Valider.Text = "Ajouter";
            }
        }

        private void InitializeCbxFamilles()
        {
            // --- To Move ---//
            DBManager dbm = new DBManager();
            //////////////////////////

            Cbx_Famille.Items.Clear();
            Dictionary<int, string> dictionaryFamilles = dbm.getAllFamilles().ToDictionary(x => x.Id, x => x.Nom);
            //dictionaryFamilles.Add(2, "un");
            if(dictionaryFamilles.Count > 0)
            {
                Cbx_Famille.DataSource = new BindingSource(dictionaryFamilles, null);
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
            Dictionary<int, string> dictionarySousFamilles = dbm.getAllSousFamilles().ToDictionary(x => x.Id, x => x.Nom);
            //dictionarySousFamilles.Add(2, "un");
            if(dictionarySousFamilles.Count > 0)
            {
                Cbx_SousFamille.DataSource = new BindingSource(dictionarySousFamilles, null);
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
            Dictionary<int, string> dictionaryMarques = dbm.getAllMarques().ToDictionary(x => x.Id, x => x.Nom);
            //dictionaryMarques.Add(2, "un");
            if(dictionaryMarques.Count > 0)
            {
                Cbx_Marque.DataSource = new BindingSource(dictionaryMarques, null);
                Cbx_Marque.DisplayMember = "Value";
                Cbx_Marque.ValueMember = "Key";
            }
            Cbx_Marque.SelectedIndex = -1;
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
