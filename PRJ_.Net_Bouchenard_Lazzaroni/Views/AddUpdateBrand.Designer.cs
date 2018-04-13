namespace PRJ_.Net_Bouchenard_Lazzaroni
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
            this.Lab_Title = new System.Windows.Forms.Label();
            this.Btn_Valider = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tbx_Marque
            // 
            this.Tbx_Marque.Location = new System.Drawing.Point(175, 74);
            this.Tbx_Marque.Name = "Tbx_Marque";
            this.Tbx_Marque.Size = new System.Drawing.Size(235, 20);
            this.Tbx_Marque.TabIndex = 11;
            // 
            // Lab_Marque
            // 
            this.Lab_Marque.AutoSize = true;
            this.Lab_Marque.Location = new System.Drawing.Point(70, 77);
            this.Lab_Marque.Name = "Lab_Marque";
            this.Lab_Marque.Size = new System.Drawing.Size(99, 13);
            this.Lab_Marque.TabIndex = 10;
            this.Lab_Marque.Text = "Nom de la marque :";
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
            this.Btn_Valider.Click += new System.EventHandler(this.Btn_Valider_Click);
            // 
            // AddUpdateBrand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 148);
            this.Controls.Add(this.Btn_Valider);
            this.Controls.Add(this.Tbx_Marque);
            this.Controls.Add(this.Lab_Marque);
            this.Controls.Add(this.Lab_Title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AddUpdateBrand";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Marque";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tbx_Marque;
        private System.Windows.Forms.Label Lab_Marque;
        private System.Windows.Forms.Label Lab_Title;
        private System.Windows.Forms.Button Btn_Valider;
    }
}