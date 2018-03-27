namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    partial class SelectXml
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btnIntegrate = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Update_XML = new System.Windows.Forms.RadioButton();
            this.Integration_XML = new System.Windows.Forms.RadioButton();
            this.DocOpen_Window = new System.Windows.Forms.OpenFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.File_path = new System.Windows.Forms.Label();
            this.lab_FName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(52, 383);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(533, 28);
            this.progressBar.TabIndex = 0;
            // 
            // btnIntegrate
            // 
            this.btnIntegrate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIntegrate.Location = new System.Drawing.Point(417, 345);
            this.btnIntegrate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnIntegrate.Name = "btnIntegrate";
            this.btnIntegrate.Size = new System.Drawing.Size(168, 30);
            this.btnIntegrate.TabIndex = 1;
            this.btnIntegrate.Text = "Integrate";
            this.btnIntegrate.UseVisualStyleBackColor = true;
            this.btnIntegrate.Click += new System.EventHandler(this.btnIntegrate_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.Update_XML);
            this.panel1.Controls.Add(this.Integration_XML);
            this.panel1.Location = new System.Drawing.Point(52, 345);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 31);
            this.panel1.TabIndex = 2;
            // 
            // Update_XML
            // 
            this.Update_XML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Update_XML.AutoSize = true;
            this.Update_XML.Location = new System.Drawing.Point(144, 6);
            this.Update_XML.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Update_XML.Name = "Update_XML";
            this.Update_XML.Size = new System.Drawing.Size(138, 21);
            this.Update_XML.TabIndex = 1;
            this.Update_XML.Text = "Update database";
            this.Update_XML.UseVisualStyleBackColor = true;
            // 
            // Integration_XML
            // 
            this.Integration_XML.AutoSize = true;
            this.Integration_XML.Location = new System.Drawing.Point(4, 4);
            this.Integration_XML.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Integration_XML.Name = "Integration_XML";
            this.Integration_XML.Size = new System.Drawing.Size(127, 21);
            this.Integration_XML.TabIndex = 0;
            this.Integration_XML.Text = "New integration";
            this.Integration_XML.UseVisualStyleBackColor = true;
            // 
            // DocOpen_Window
            // 
            this.DocOpen_Window.FileName = "openFileDialog1";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFile.Location = new System.Drawing.Point(417, 308);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(168, 30);
            this.btnOpenFile.TabIndex = 3;
            this.btnOpenFile.Text = "Open a file";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // File_path
            // 
            this.File_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.File_path.Location = new System.Drawing.Point(53, 292);
            this.File_path.MaximumSize = new System.Drawing.Size(300, 100);
            this.File_path.Name = "File_path";
            this.File_path.Size = new System.Drawing.Size(300, 20);
            this.File_path.TabIndex = 4;
            this.File_path.Text = "File selected";
            // 
            // lab_FName
            // 
            this.lab_FName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lab_FName.AutoSize = true;
            this.lab_FName.Location = new System.Drawing.Point(53, 321);
            this.lab_FName.Name = "lab_FName";
            this.lab_FName.Size = new System.Drawing.Size(0, 17);
            this.lab_FName.TabIndex = 5;
            // 
            // SelectXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 422);
            this.Controls.Add(this.lab_FName);
            this.Controls.Add(this.File_path);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnIntegrate);
            this.Controls.Add(this.progressBar);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "SelectXml";
            this.Text = "SelectXml";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btnIntegrate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton Update_XML;
        private System.Windows.Forms.RadioButton Integration_XML;
        private System.Windows.Forms.OpenFileDialog DocOpen_Window;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label File_path;
        private System.Windows.Forms.Label lab_FName;
    }
}