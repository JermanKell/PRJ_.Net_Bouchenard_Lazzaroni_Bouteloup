namespace PRJ_.Net_Bouchenard_Lazzaroni
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.listViewArticle = new System.Windows.Forms.ListView();
            this.selectXMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.rightclickMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(677, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // listViewArticle
            // 
            this.listViewArticle.AllowColumnReorder = true;
            this.listViewArticle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewArticle.FullRowSelect = true;
            this.listViewArticle.Location = new System.Drawing.Point(12, 27);
            this.listViewArticle.MultiSelect = false;
            this.listViewArticle.Name = "listViewArticle";
            this.listViewArticle.Size = new System.Drawing.Size(653, 509);
            this.listViewArticle.TabIndex = 3;
            this.listViewArticle.UseCompatibleStateImageBehavior = false;
            this.listViewArticle.View = System.Windows.Forms.View.Details;
            this.listViewArticle.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewArticle_ColumnClick);
            this.listViewArticle.DoubleClick += new System.EventHandler(this.listViewArticle_DoubleClick);
            this.listViewArticle.KeyUp += new System.Windows.Forms.KeyEventHandler(this.listViewArticle_KeyUp);
            this.listViewArticle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listViewArticle_MouseUp);
            this.listViewArticle.Resize += new System.EventHandler(this.listViewArticle_Resize);
            // 
            // selectXMLToolStripMenuItem
            // 
            this.selectXMLToolStripMenuItem.Name = "selectXMLToolStripMenuItem";
            this.selectXMLToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.selectXMLToolStripMenuItem.Text = "Select XML";
            this.selectXMLToolStripMenuItem.Click += new System.EventHandler(this.selectXMLToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectXMLToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(677, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // rightclickMenuStrip
            // 
            this.rightclickMenuStrip.Name = "rightclickMenuStrip";
            this.rightclickMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.rightclickMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.rightclickMenuStrip_ItemClicked);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 561);
            this.Controls.Add(this.listViewArticle);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Main";
            this.Text = "Main";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Main_KeyUp);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ListView listViewArticle;
        private System.Windows.Forms.ToolStripMenuItem selectXMLToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ContextMenuStrip rightclickMenuStrip;
    }
}

