﻿namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    partial class AddUpdateBrand
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
            this.Tbx_Marque = new System.Windows.Forms.TextBox();
            this.Lab_Marque = new System.Windows.Forms.Label();
            this.Btn_Valider = new System.Windows.Forms.Button();
            this.Btn_Annuler = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tbx_Marque
            // 
            this.Tbx_Marque.Location = new System.Drawing.Point(53, 9);
            this.Tbx_Marque.Name = "Tbx_Marque";
            this.Tbx_Marque.Size = new System.Drawing.Size(195, 20);
            this.Tbx_Marque.TabIndex = 0;
            // 
            // Lab_Marque
            // 
            this.Lab_Marque.AutoSize = true;
            this.Lab_Marque.Location = new System.Drawing.Point(12, 9);
            this.Lab_Marque.Name = "Lab_Marque";
            this.Lab_Marque.Size = new System.Drawing.Size(35, 13);
            this.Lab_Marque.TabIndex = 10;
            this.Lab_Marque.Text = "Nom :";
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Location = new System.Drawing.Point(157, 48);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(91, 27);
            this.Btn_Valider.TabIndex = 1;
            this.Btn_Valider.Text = "*";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            this.Btn_Valider.Click += new System.EventHandler(this.Btn_Valider_Click);
            // 
            // Btn_Annuler
            // 
            this.Btn_Annuler.Location = new System.Drawing.Point(60, 48);
            this.Btn_Annuler.Name = "Btn_Annuler";
            this.Btn_Annuler.Size = new System.Drawing.Size(91, 27);
            this.Btn_Annuler.TabIndex = 2;
            this.Btn_Annuler.Text = "Annuler";
            this.Btn_Annuler.UseVisualStyleBackColor = true;
            this.Btn_Annuler.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddUpdateBrand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 96);
            this.Controls.Add(this.Btn_Annuler);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Marque);
            this.Controls.Add(this.Lab_Marque);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AddUpdateBrand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Marque";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tbx_Marque;
        private System.Windows.Forms.Label Lab_Marque;
        private System.Windows.Forms.Button Btn_Valider;
        private System.Windows.Forms.Button Btn_Annuler;
    }
}