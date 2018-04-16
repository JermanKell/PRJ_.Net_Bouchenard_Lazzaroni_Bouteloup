namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    partial class AddUpdateArticle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Cbx_Marque = new System.Windows.Forms.ComboBox();
            this.Cbx_Famille = new System.Windows.Forms.ComboBox();
            this.Cbx_SousFamille = new System.Windows.Forms.ComboBox();
            this.Lab_Reference = new System.Windows.Forms.Label();
            this.Lab_Description = new System.Windows.Forms.Label();
            this.Lab_Prix = new System.Windows.Forms.Label();
            this.Lab_Quantite = new System.Windows.Forms.Label();
            this.Tbx_Reference = new System.Windows.Forms.TextBox();
            this.Tbx_Description = new System.Windows.Forms.TextBox();
            this.Lab_Famille = new System.Windows.Forms.Label();
            this.Lab_SousFamille = new System.Windows.Forms.Label();
            this.Lab_Marque = new System.Windows.Forms.Label();
            this.Tbx_Prix = new System.Windows.Forms.TextBox();
            this.Tbx_Quantite = new System.Windows.Forms.TextBox();
            this.Btn_Valider = new System.Windows.Forms.Button();
            this.Btn_Annuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Cbx_Marque
            // 
            this.Cbx_Marque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_Marque.FormattingEnabled = true;
            this.Cbx_Marque.Location = new System.Drawing.Point(155, 222);
            this.Cbx_Marque.Name = "Cbx_Marque";
            this.Cbx_Marque.Size = new System.Drawing.Size(235, 21);
            this.Cbx_Marque.TabIndex = 4;
            // 
            // Cbx_Famille
            // 
            this.Cbx_Famille.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_Famille.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Cbx_Famille.FormattingEnabled = true;
            this.Cbx_Famille.Location = new System.Drawing.Point(155, 138);
            this.Cbx_Famille.Name = "Cbx_Famille";
            this.Cbx_Famille.Size = new System.Drawing.Size(235, 21);
            this.Cbx_Famille.TabIndex = 2;
            this.Cbx_Famille.SelectedIndexChanged += new System.EventHandler(this.Cbx_Famille_SelectedIndexChanged);
            // 
            // Cbx_SousFamille
            // 
            this.Cbx_SousFamille.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cbx_SousFamille.FormattingEnabled = true;
            this.Cbx_SousFamille.Location = new System.Drawing.Point(155, 178);
            this.Cbx_SousFamille.Name = "Cbx_SousFamille";
            this.Cbx_SousFamille.Size = new System.Drawing.Size(235, 21);
            this.Cbx_SousFamille.TabIndex = 3;
            // 
            // Lab_Reference
            // 
            this.Lab_Reference.AutoSize = true;
            this.Lab_Reference.Location = new System.Drawing.Point(17, 15);
            this.Lab_Reference.Name = "Lab_Reference";
            this.Lab_Reference.Size = new System.Drawing.Size(83, 13);
            this.Lab_Reference.TabIndex = 4;
            this.Lab_Reference.Text = "Nom reference :";
            // 
            // Lab_Description
            // 
            this.Lab_Description.AutoSize = true;
            this.Lab_Description.Location = new System.Drawing.Point(17, 53);
            this.Lab_Description.Name = "Lab_Description";
            this.Lab_Description.Size = new System.Drawing.Size(66, 13);
            this.Lab_Description.TabIndex = 5;
            this.Lab_Description.Text = "Description :";
            // 
            // Lab_Prix
            // 
            this.Lab_Prix.AutoSize = true;
            this.Lab_Prix.Location = new System.Drawing.Point(17, 263);
            this.Lab_Prix.Name = "Lab_Prix";
            this.Lab_Prix.Size = new System.Drawing.Size(67, 13);
            this.Lab_Prix.TabIndex = 6;
            this.Lab_Prix.Text = "Prix unitaire :";
            // 
            // Lab_Quantite
            // 
            this.Lab_Quantite.AutoSize = true;
            this.Lab_Quantite.Location = new System.Drawing.Point(17, 307);
            this.Lab_Quantite.Name = "Lab_Quantite";
            this.Lab_Quantite.Size = new System.Drawing.Size(53, 13);
            this.Lab_Quantite.TabIndex = 7;
            this.Lab_Quantite.Text = "Quantité :";
            // 
            // Tbx_Reference
            // 
            this.Tbx_Reference.Location = new System.Drawing.Point(155, 12);
            this.Tbx_Reference.Name = "Tbx_Reference";
            this.Tbx_Reference.Size = new System.Drawing.Size(235, 20);
            this.Tbx_Reference.TabIndex = 0;
            // 
            // Tbx_Description
            // 
            this.Tbx_Description.Location = new System.Drawing.Point(155, 50);
            this.Tbx_Description.Multiline = true;
            this.Tbx_Description.Name = "Tbx_Description";
            this.Tbx_Description.Size = new System.Drawing.Size(235, 72);
            this.Tbx_Description.TabIndex = 1;
            // 
            // Lab_Famille
            // 
            this.Lab_Famille.AutoSize = true;
            this.Lab_Famille.Location = new System.Drawing.Point(17, 138);
            this.Lab_Famille.Name = "Lab_Famille";
            this.Lab_Famille.Size = new System.Drawing.Size(97, 13);
            this.Lab_Famille.TabIndex = 10;
            this.Lab_Famille.Text = "Choisir une famille :";
            // 
            // Lab_SousFamille
            // 
            this.Lab_SousFamille.AutoSize = true;
            this.Lab_SousFamille.Location = new System.Drawing.Point(17, 178);
            this.Lab_SousFamille.Name = "Lab_SousFamille";
            this.Lab_SousFamille.Size = new System.Drawing.Size(122, 13);
            this.Lab_SousFamille.TabIndex = 11;
            this.Lab_SousFamille.Text = "Choisir une sous famille :";
            // 
            // Lab_Marque
            // 
            this.Lab_Marque.AutoSize = true;
            this.Lab_Marque.Location = new System.Drawing.Point(17, 222);
            this.Lab_Marque.Name = "Lab_Marque";
            this.Lab_Marque.Size = new System.Drawing.Size(103, 13);
            this.Lab_Marque.TabIndex = 12;
            this.Lab_Marque.Text = "Choisir une marque :";
            // 
            // Tbx_Prix
            // 
            this.Tbx_Prix.Location = new System.Drawing.Point(155, 263);
            this.Tbx_Prix.Name = "Tbx_Prix";
            this.Tbx_Prix.Size = new System.Drawing.Size(235, 20);
            this.Tbx_Prix.TabIndex = 5;
            this.Tbx_Prix.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbx_Prix_KeyPress);
            // 
            // Tbx_Quantite
            // 
            this.Tbx_Quantite.Location = new System.Drawing.Point(155, 307);
            this.Tbx_Quantite.Name = "Tbx_Quantite";
            this.Tbx_Quantite.Size = new System.Drawing.Size(235, 20);
            this.Tbx_Quantite.TabIndex = 6;
            this.Tbx_Quantite.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Tbx_Quantite_KeyPress);
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Location = new System.Drawing.Point(289, 352);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(101, 27);
            this.Btn_Valider.TabIndex = 7;
            this.Btn_Valider.Text = "*";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            this.Btn_Valider.Click += new System.EventHandler(this.Btn_Valider_Click);
            // 
            // Btn_Annuler
            // 
            this.Btn_Annuler.Location = new System.Drawing.Point(182, 352);
            this.Btn_Annuler.Name = "Btn_Annuler";
            this.Btn_Annuler.Size = new System.Drawing.Size(101, 27);
            this.Btn_Annuler.TabIndex = 8;
            this.Btn_Annuler.Text = "Annuler";
            this.Btn_Annuler.UseVisualStyleBackColor = true;
            this.Btn_Annuler.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddUpdateArticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 394);
            this.Controls.Add(this.Btn_Annuler);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Quantite);
            this.Controls.Add(this.Tbx_Prix);
            this.Controls.Add(this.Lab_Marque);
            this.Controls.Add(this.Lab_SousFamille);
            this.Controls.Add(this.Lab_Famille);
            this.Controls.Add(this.Tbx_Description);
            this.Controls.Add(this.Tbx_Reference);
            this.Controls.Add(this.Lab_Quantite);
            this.Controls.Add(this.Lab_Prix);
            this.Controls.Add(this.Lab_Description);
            this.Controls.Add(this.Lab_Reference);
            this.Controls.Add(this.Cbx_SousFamille);
            this.Controls.Add(this.Cbx_Famille);
            this.Controls.Add(this.Cbx_Marque);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddUpdateArticle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Article";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox Cbx_Marque;
        private System.Windows.Forms.ComboBox Cbx_Famille;
        private System.Windows.Forms.ComboBox Cbx_SousFamille;
        private System.Windows.Forms.Label Lab_Reference;
        private System.Windows.Forms.Label Lab_Description;
        private System.Windows.Forms.Label Lab_Prix;
        private System.Windows.Forms.Label Lab_Quantite;
        private System.Windows.Forms.TextBox Tbx_Reference;
        private System.Windows.Forms.TextBox Tbx_Description;
        private System.Windows.Forms.Label Lab_Famille;
        private System.Windows.Forms.Label Lab_SousFamille;
        private System.Windows.Forms.Label Lab_Marque;
        private System.Windows.Forms.TextBox Tbx_Prix;
        private System.Windows.Forms.TextBox Tbx_Quantite;
        private System.Windows.Forms.Button Btn_Valider;
        private System.Windows.Forms.Button Btn_Annuler;
    }
}