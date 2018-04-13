namespace PRJ_.Net_Bouchenard_Lazzaroni.Views
{
    partial class Brand
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
            this.Tbx_Reference = new System.Windows.Forms.TextBox();
            this.Labl_Reference = new System.Windows.Forms.Label();
            this.Lab_Title = new System.Windows.Forms.Label();
            this.Btn_Valider = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tbx_Reference
            // 
            this.Tbx_Reference.Location = new System.Drawing.Point(175, 74);
            this.Tbx_Reference.Name = "Tbx_Reference";
            this.Tbx_Reference.Size = new System.Drawing.Size(235, 20);
            this.Tbx_Reference.TabIndex = 11;
            // 
            // Labl_Reference
            // 
            this.Labl_Reference.AutoSize = true;
            this.Labl_Reference.Location = new System.Drawing.Point(86, 77);
            this.Labl_Reference.Name = "Labl_Reference";
            this.Labl_Reference.Size = new System.Drawing.Size(83, 13);
            this.Labl_Reference.TabIndex = 10;
            this.Labl_Reference.Text = "Nom reference :";
            // 
            // Lab_Title
            // 
            this.Lab_Title.AutoSize = true;
            this.Lab_Title.Location = new System.Drawing.Point(204, 9);
            this.Lab_Title.Name = "Lab_Title";
            this.Lab_Title.Size = new System.Drawing.Size(11, 13);
            this.Lab_Title.TabIndex = 9;
            this.Lab_Title.Text = "*";
            // 
            // Btn_Valider
            // 
            this.Btn_Valider.Location = new System.Drawing.Point(150, 109);
            this.Btn_Valider.Name = "Btn_Valider";
            this.Btn_Valider.Size = new System.Drawing.Size(157, 27);
            this.Btn_Valider.TabIndex = 16;
            this.Btn_Valider.Text = "*";
            this.Btn_Valider.UseVisualStyleBackColor = true;
            // 
            // Brand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 148);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Reference);
            this.Controls.Add(this.Labl_Reference);
            this.Controls.Add(this.Lab_Title);
            this.Name = "Brand";
            this.Text = "Brand";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tbx_Reference;
        private System.Windows.Forms.Label Labl_Reference;
        private System.Windows.Forms.Label Lab_Title;
        private System.Windows.Forms.Button Btn_Valider;
    }
}