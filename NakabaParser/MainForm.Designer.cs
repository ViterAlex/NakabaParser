using System.Drawing;

namespace SiteParser
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.getAnnoncesButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.exportButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pageImageStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentPageNumStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.totalPagesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgAnnonceStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentAnnonceNumStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fromStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.totalAnnoncesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // getAnnoncesButton
            // 
            this.getAnnoncesButton.AutoSize = true;
            this.getAnnoncesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.getAnnoncesButton.Location = new System.Drawing.Point(3, 31);
            this.getAnnoncesButton.Name = "getAnnoncesButton";
            this.getAnnoncesButton.Size = new System.Drawing.Size(133, 23);
            this.getAnnoncesButton.TabIndex = 0;
            this.getAnnoncesButton.Text = "Загрузить объявления";
            this.getAnnoncesButton.UseVisualStyleBackColor = true;
            this.getAnnoncesButton.Click += new System.EventHandler(this.getAnnoncesButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 60);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(641, 198);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.exportButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.getAnnoncesButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.urlTextBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(647, 261);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // exportButton
            // 
            this.exportButton.AutoSize = true;
            this.exportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exportButton.Location = new System.Drawing.Point(142, 31);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(97, 23);
            this.exportButton.TabIndex = 3;
            this.exportButton.Text = "Экспорт в Word";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // urlTextBox
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.urlTextBox, 2);
            this.urlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.urlTextBox.Location = new System.Drawing.Point(3, 3);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(641, 22);
            this.urlTextBox.TabIndex = 2;
            this.urlTextBox.Text = "http://market.nakaba.ru/search-results/?action=search&username[equal]=Relin&listi" +
    "ngs_per_page=100";
            this.urlTextBox.TextChanged += new System.EventHandler(this.urlTextBox_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageImageStatusLabel,
            this.currentPageNumStatusLabel,
            this.toolStripStatusLabel3,
            this.totalPagesStatusLabel,
            this.imgAnnonceStatusLabel,
            this.currentAnnonceNumStatusLabel,
            this.fromStatusLabel,
            this.totalAnnoncesStatusLabel,
            this.ProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 239);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(647, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.Visible = false;
            // 
            // pageImageStatusLabel
            // 
            this.pageImageStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageImageStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("pageImageStatusLabel.Image")));
            this.pageImageStatusLabel.Name = "pageImageStatusLabel";
            this.pageImageStatusLabel.Size = new System.Drawing.Size(16, 17);
            this.pageImageStatusLabel.Text = "toolStripStatusLabel1";
            this.pageImageStatusLabel.ToolTipText = "Загружено страниц";
            // 
            // currentPageNumStatusLabel
            // 
            this.currentPageNumStatusLabel.Name = "currentPageNumStatusLabel";
            this.currentPageNumStatusLabel.Size = new System.Drawing.Size(13, 17);
            this.currentPageNumStatusLabel.Text = "2";
            this.currentPageNumStatusLabel.ToolTipText = "Загружено страниц";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(19, 17);
            this.toolStripStatusLabel3.Text = "из";
            this.toolStripStatusLabel3.ToolTipText = "Загружено страниц";
            // 
            // totalPagesStatusLabel
            // 
            this.totalPagesStatusLabel.Name = "totalPagesStatusLabel";
            this.totalPagesStatusLabel.Size = new System.Drawing.Size(19, 17);
            this.totalPagesStatusLabel.Text = "31";
            this.totalPagesStatusLabel.ToolTipText = "Загружено страниц";
            // 
            // imgAnnonceStatusLabel
            // 
            this.imgAnnonceStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imgAnnonceStatusLabel.Image = ((System.Drawing.Image)(resources.GetObject("imgAnnonceStatusLabel.Image")));
            this.imgAnnonceStatusLabel.Name = "imgAnnonceStatusLabel";
            this.imgAnnonceStatusLabel.Size = new System.Drawing.Size(16, 17);
            this.imgAnnonceStatusLabel.Text = "toolStripStatusLabel1";
            this.imgAnnonceStatusLabel.ToolTipText = "Загружено объявлений";
            // 
            // currentAnnonceNumStatusLabel
            // 
            this.currentAnnonceNumStatusLabel.Name = "currentAnnonceNumStatusLabel";
            this.currentAnnonceNumStatusLabel.Size = new System.Drawing.Size(13, 17);
            this.currentAnnonceNumStatusLabel.Text = "5";
            this.currentAnnonceNumStatusLabel.ToolTipText = "Загружено объявлений";
            // 
            // fromStatusLabel
            // 
            this.fromStatusLabel.Name = "fromStatusLabel";
            this.fromStatusLabel.Size = new System.Drawing.Size(19, 17);
            this.fromStatusLabel.Text = "из";
            this.fromStatusLabel.ToolTipText = "Загружено объявлений";
            // 
            // totalAnnoncesStatusLabel
            // 
            this.totalAnnoncesStatusLabel.Name = "totalAnnoncesStatusLabel";
            this.totalAnnoncesStatusLabel.Size = new System.Drawing.Size(19, 17);
            this.totalAnnoncesStatusLabel.Text = "40";
            this.totalAnnoncesStatusLabel.ToolTipText = "Загружено объявлений";
            // 
            // ProgressBar1
            // 
            this.ProgressBar1.Name = "ProgressBar1";
            this.ProgressBar1.Size = new System.Drawing.Size(350, 16);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(647, 261);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "nakaba.ru";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getAnnoncesButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel pageImageStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentPageNumStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel totalPagesStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel imgAnnonceStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentAnnonceNumStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel fromStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel totalAnnoncesStatusLabel;
    }
}

