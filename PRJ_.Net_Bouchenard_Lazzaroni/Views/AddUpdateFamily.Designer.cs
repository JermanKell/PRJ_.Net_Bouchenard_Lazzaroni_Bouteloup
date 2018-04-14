namespace PRJ_.Net_Bouchenard_Lazzaroni.Views
{
    partial class AddUpdateFamily
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
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Location = new System.Drawing.Point(157, 51);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(91, 27);
            this.Btn_Valider.TabIndex = 20;
            this.Btn_Valider.Text = "*";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            this.Btn_Valider.Click += new System.EventHandler(this.Btn_Valider_Click);
            // 
            // Tbx_Famille
            // 
            this.Tbx_Famille.Location = new System.Drawing.Point(53, 9);
            this.Tbx_Famille.Name = "Tbx_Famille";
            this.Tbx_Famille.Size = new System.Drawing.Size(195, 20);
            this.Tbx_Famille.TabIndex = 19;
            // 
            // Lab_Famille
            // 
            this.Lab_Famille.AutoSize = true;
            this.Lab_Famille.Location = new System.Drawing.Point(12, 9);
            this.Lab_Famille.Name = "Lab_Famille";
            this.Lab_Famille.Size = new System.Drawing.Size(35, 13);
            this.Lab_Famille.TabIndex = 18;
            this.Lab_Famille.Text = "Nom :";
            this.Lab_Famille.Click += new System.EventHandler(this.Lab_Famille_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(60, 51);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(91, 27);
            this.cancelButton.TabIndex = 21;
            this.cancelButton.Text = "Annuler";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // AddUpdateFamily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 96);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Famille);
            this.Controls.Add(this.Lab_Famille);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddUpdateFamily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Famille";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Valider;
        private System.Windows.Forms.TextBox Tbx_Famille;
        private System.Windows.Forms.Label Lab_Famille;
        private System.Windows.Forms.Button cancelButton;
    }
}