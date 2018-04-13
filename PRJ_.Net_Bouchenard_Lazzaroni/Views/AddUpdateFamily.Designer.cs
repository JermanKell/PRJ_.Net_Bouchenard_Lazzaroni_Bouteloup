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
            this.Lab_Title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Location = new System.Drawing.Point(150, 109);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(157, 27);
            this.Btn_Valider.TabIndex = 20;
            this.Btn_Valider.Text = "*";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            // 
            // Tbx_Famille
            // 
            this.Tbx_Famille.Location = new System.Drawing.Point(175, 74);
            this.Tbx_Famille.Name = "Tbx_Famille";
            this.Tbx_Famille.Size = new System.Drawing.Size(235, 20);
            this.Tbx_Famille.TabIndex = 19;
            // 
            // Lab_Famille
            // 
            this.Lab_Famille.AutoSize = true;
            this.Lab_Famille.Location = new System.Drawing.Point(87, 77);
            this.Lab_Famille.Name = "Lab_Famille";
            this.Lab_Famille.Size = new System.Drawing.Size(82, 13);
            this.Lab_Famille.TabIndex = 18;
            this.Lab_Famille.Text = "Nom de famille :";
            // 
            // Lab_Title
            // 
            this.Lab_Title.AutoSize = true;
            this.Lab_Title.Location = new System.Drawing.Point(204, 9);
            this.Lab_Title.Name = "Lab_Title";
            this.Lab_Title.Size = new System.Drawing.Size(11, 13);
            this.Lab_Title.TabIndex = 17;
            this.Lab_Title.Text = "*";
            // 
            // AddUpdateFamily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 148);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Famille);
            this.Controls.Add(this.Lab_Famille);
            this.Controls.Add(this.Lab_Title);
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
        private System.Windows.Forms.Label Lab_Title;
    }
}