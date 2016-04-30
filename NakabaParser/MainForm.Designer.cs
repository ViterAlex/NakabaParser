using System.Drawing;

namespace SiteParser
{
    sealed partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pauseButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.loadAnnoncesButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.parsingStatusStrip = new System.Windows.Forms.StatusStrip();
            this.pageImageStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentPageNumStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.totalPagesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.imgAnnonceStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.currentAnnonceNumStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fromStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.totalAnnoncesStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.parsingProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.exportStatusStrip = new System.Windows.Forms.StatusStrip();
            this.exportMessageStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.exportProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.parsingStatusStrip.SuspendLayout();
            this.exportStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 57);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(641, 201);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.14065F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.urlTextBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
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
            // urlTextBox
            // 
            this.urlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urlTextBox.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.urlTextBox.Location = new System.Drawing.Point(3, 3);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(641, 22);
            this.urlTextBox.TabIndex = 2;
            this.urlTextBox.Text = "http://market.nakaba.ru/search-results/?action=search&username[equal]=Relin&listi" +
    "ngs_per_page=10";
            this.urlTextBox.TextChanged += new System.EventHandler(this.urlTextBox_TextChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.pauseButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.exportButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.loadAnnoncesButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.stopButton, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(647, 26);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // pauseButton
            // 
            this.pauseButton.AutoSize = true;
            this.pauseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pauseButton.Location = new System.Drawing.Point(78, 3);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(48, 23);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Пауза";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.AutoSize = true;
            this.exportButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exportButton.Location = new System.Drawing.Point(179, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(97, 23);
            this.exportButton.TabIndex = 3;
            this.exportButton.Text = "Экспорт в Word";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // loadAnnoncesButton
            // 
            this.loadAnnoncesButton.AutoSize = true;
            this.loadAnnoncesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.loadAnnoncesButton.Location = new System.Drawing.Point(3, 3);
            this.loadAnnoncesButton.Name = "loadAnnoncesButton";
            this.loadAnnoncesButton.Size = new System.Drawing.Size(69, 23);
            this.loadAnnoncesButton.TabIndex = 1;
            this.loadAnnoncesButton.Text = "Загрузить";
            this.loadAnnoncesButton.UseVisualStyleBackColor = true;
            this.loadAnnoncesButton.Click += new System.EventHandler(this.loadAnnoncesButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.AutoSize = true;
            this.stopButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stopButton.Location = new System.Drawing.Point(132, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(41, 23);
            this.stopButton.TabIndex = 3;
            this.stopButton.Text = "Стоп";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // parsingStatusStrip
            // 
            this.parsingStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageImageStatusLabel,
            this.currentPageNumStatusLabel,
            this.toolStripStatusLabel3,
            this.totalPagesStatusLabel,
            this.imgAnnonceStatusLabel,
            this.currentAnnonceNumStatusLabel,
            this.fromStatusLabel,
            this.totalAnnoncesStatusLabel,
            this.parsingProgressBar});
            this.parsingStatusStrip.Location = new System.Drawing.Point(0, 239);
            this.parsingStatusStrip.Name = "parsingStatusStrip";
            this.parsingStatusStrip.Size = new System.Drawing.Size(647, 22);
            this.parsingStatusStrip.TabIndex = 3;
            this.parsingStatusStrip.Text = "statusStrip1";
            this.parsingStatusStrip.Visible = false;
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
            // parsingProgressBar
            // 
            this.parsingProgressBar.Name = "parsingProgressBar";
            this.parsingProgressBar.Size = new System.Drawing.Size(350, 16);
            // 
            // exportStatusStrip
            // 
            this.exportStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportMessageStatusLabel,
            this.exportProgressBar});
            this.exportStatusStrip.Location = new System.Drawing.Point(0, 239);
            this.exportStatusStrip.Name = "exportStatusStrip";
            this.exportStatusStrip.Size = new System.Drawing.Size(647, 22);
            this.exportStatusStrip.TabIndex = 4;
            this.exportStatusStrip.Text = "statusStrip1";
            this.exportStatusStrip.Visible = false;
            // 
            // exportMessageStatusLabel
            // 
            this.exportMessageStatusLabel.Name = "exportMessageStatusLabel";
            this.exportMessageStatusLabel.Size = new System.Drawing.Size(146, 17);
            this.exportMessageStatusLabel.Text = "exportMessageStatusLabel";
            this.exportMessageStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exportProgressBar
            // 
            this.exportProgressBar.Name = "exportProgressBar";
            this.exportProgressBar.Size = new System.Drawing.Size(100, 16);
            this.exportProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // bindingSource
            // 
            this.bindingSource.AllowNew = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(196)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(647, 261);
            this.Controls.Add(this.parsingStatusStrip);
            this.Controls.Add(this.exportStatusStrip);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "nakaba.ru";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.parsingStatusStrip.ResumeLayout(false);
            this.parsingStatusStrip.PerformLayout();
            this.exportStatusStrip.ResumeLayout(false);
            this.exportStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.StatusStrip parsingStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar parsingProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel pageImageStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentPageNumStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel totalPagesStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel imgAnnonceStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel currentAnnonceNumStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel fromStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel totalAnnoncesStatusLabel;
        private System.Windows.Forms.StatusStrip exportStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel exportMessageStatusLabel;
        private System.Windows.Forms.ToolStripProgressBar exportProgressBar;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button loadAnnoncesButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.BindingSource bindingSource;
    }
}

