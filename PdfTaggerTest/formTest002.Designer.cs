namespace PdfTaggerTest
{
    partial class formTest002
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
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.btOpen = new System.Windows.Forms.Button();
            this.grdRegions = new System.Windows.Forms.DataGridView();
            this.Page = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RegText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdRegions)).BeginInit();
            this.SuspendLayout();
            // 
            // dlgOpen
            // 
            this.dlgOpen.Filter = "Archivos pdf|*pdf";
            // 
            // btOpen
            // 
            this.btOpen.Location = new System.Drawing.Point(12, 9);
            this.btOpen.Name = "btOpen";
            this.btOpen.Size = new System.Drawing.Size(75, 23);
            this.btOpen.TabIndex = 0;
            this.btOpen.Text = "Abrir";
            this.btOpen.UseVisualStyleBackColor = true;
            this.btOpen.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // grdRegions
            // 
            this.grdRegions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRegions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Page,
            this.RegText});
            this.grdRegions.Location = new System.Drawing.Point(12, 39);
            this.grdRegions.Name = "grdRegions";
            this.grdRegions.RowHeadersVisible = false;
            this.grdRegions.Size = new System.Drawing.Size(610, 253);
            this.grdRegions.TabIndex = 1;
            // 
            // Page
            // 
            this.Page.HeaderText = "Page";
            this.Page.Name = "Page";
            // 
            // RegText
            // 
            this.RegText.HeaderText = "RegText";
            this.RegText.Name = "RegText";
            this.RegText.Width = 500;
            // 
            // formTest002
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 303);
            this.Controls.Add(this.grdRegions);
            this.Controls.Add(this.btOpen);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formTest002";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VISUALIZA REGIONES DE UN PDF";
            ((System.ComponentModel.ISupportInitialize)(this.grdRegions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.Button btOpen;
        private System.Windows.Forms.DataGridView grdRegions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Page;
        private System.Windows.Forms.DataGridViewTextBoxColumn RegText;
    }
}