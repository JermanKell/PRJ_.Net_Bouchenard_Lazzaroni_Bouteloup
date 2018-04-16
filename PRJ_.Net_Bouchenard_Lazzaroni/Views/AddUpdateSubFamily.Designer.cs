namespace PRJ_.Net_Bouchenard_Lazzaroni.Views
{
    partial class AddUpdateSubFamily
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
            this.Btn_Valider = new System.Windows.Forms.Button();
            this.Tbx_Famille = new System.Windows.Forms.TextBox();
            this.Lab_Famille = new System.Windows.Forms.Label();
            this.Btn_Annuler = new System.Windows.Forms.Button();
            this.label_famille = new System.Windows.Forms.Label();
            this.Cbx_Famille = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Location = new System.Drawing.Point(157, 82);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(91, 27);
            this.Btn_Valider.TabIndex = 2;
            this.Btn_Valider.Text = "*";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            this.Btn_Valider.Click += new System.EventHandler(this.Btn_Valider_Click);
            // 
            // Tbx_Famille
            // 
            this.Tbx_Famille.Location = new System.Drawing.Point(53, 42);
            this.Tbx_Famille.Name = "Tbx_Famille";
            this.Tbx_Famille.Size = new System.Drawing.Size(195, 20);
            this.Tbx_Famille.TabIndex = 1;
            // 
            // Lab_Famille
            // 
            this.Lab_Famille.AutoSize = true;
            this.Lab_Famille.Location = new System.Drawing.Point(12, 42);
            this.Lab_Famille.Name = "Lab_Famille";
            this.Lab_Famille.Size = new System.Drawing.Size(35, 13);
            this.Lab_Famille.TabIndex = 18;
            this.Lab_Famille.Text = "Nom :";
            // 
            // Btn_Annuler
            // 
            this.Btn_Annuler.Location = new System.Drawing.Point(60, 82);
            this.Btn_Annuler.Name = "Btn_Annuler";
            this.Btn_Annuler.Size = new System.Drawing.Size(91, 27);
            this.Btn_Annuler.TabIndex = 3;
            this.Btn_Annuler.Text = "Annuler";
            this.Btn_Annuler.UseVisualStyleBackColor = true;
            this.Btn_Annuler.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label_famille
            // 
            this.label_famille.AutoSize = true;
            this.label_famille.Location = new System.Drawing.Point(12, 9);
            this.label_famille.Name = "label_famille";
            this.label_famille.Size = new System.Drawing.Size(39, 13);
            this.label_famille.TabIndex = 22;
            this.label_famille.Text = "Famille";
            // 
            // Cbx_Famille
            // 
            this.Cbx_Famille.FormattingEnabled = true;
            this.Cbx_Famille.Location = new System.Drawing.Point(53, 9);
            this.Cbx_Famille.Name = "Cbx_Famille";
            this.Cbx_Famille.Size = new System.Drawing.Size(195, 21);
            this.Cbx_Famille.TabIndex = 0;
            // 
            // AddUpdateSubFamily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 129);
            this.Controls.Add(this.Cbx_Famille);
            this.Controls.Add(this.label_famille);
            this.Controls.Add(this.Btn_Annuler);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Famille);
            this.Controls.Add(this.Lab_Famille);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddUpdateSubFamily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Famille";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Valider;
        private System.Windows.Forms.TextBox Tbx_Famille;
        private System.Windows.Forms.Label Lab_Famille;
        private System.Windows.Forms.Button Btn_Annuler;
        private System.Windows.Forms.Label label_famille;
        private System.Windows.Forms.ComboBox Cbx_Famille;
    }
}