namespace PdfTaggerTest
{
    partial class formTest001
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbInfo = new System.Windows.Forms.Label();
            this.fdBrw = new System.Windows.Forms.FolderBrowserDialog();
            this.txDirSource = new System.Windows.Forms.TextBox();
            this.lbDirSource = new System.Windows.Forms.Label();
            this.btDirSource = new System.Windows.Forms.Button();
            this.btDirResult = new System.Windows.Forms.Button();
            this.lbDirResult = new System.Windows.Forms.Label();
            this.txDirResult = new System.Windows.Forms.TextBox();
            this.pgbFile = new System.Windows.Forms.ProgressBar();
            this.lbProgress = new System.Windows.Forms.Label();
            this.btExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbInfo
            // 
            this.lbInfo.Location = new System.Drawing.Point(12, 9);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(307, 57);
            this.lbInfo.TabIndex = 0;
            this.lbInfo.Text = "En esta demo se leen todos los archivos pdf del directorio seleccionado y se guar" +
    "dan en el directorio de destino los xml con los datos extraídos mediante iText.";
            // 
            // txDirSource
            // 
            this.txDirSource.Enabled = false;
            this.txDirSource.Location = new System.Drawing.Point(171, 69);
            this.txDirSource.Name = "txDirSource";
            this.txDirSource.Size = new System.Drawing.Size(225, 20);
            this.txDirSource.TabIndex = 1;
            // 
            // lbDirSource
            // 
            this.lbDirSource.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbDirSource.AutoSize = true;
            this.lbDirSource.Location = new System.Drawing.Point(12, 72);
            this.lbDirSource.Name = "lbDirSource";
            this.lbDirSource.Size = new System.Drawing.Size(153, 13);
            this.lbDirSource.TabIndex = 2;
            this.lbDirSource.Text = "Seleccione directorio de origen";
            // 
            // btDirSource
            // 
            this.btDirSource.Location = new System.Drawing.Point(400, 69);
            this.btDirSource.Name = "btDirSource";
            this.btDirSource.Size = new System.Drawing.Size(30, 20);
            this.btDirSource.TabIndex = 3;
            this.btDirSource.Text = "...";
            this.btDirSource.UseVisualStyleBackColor = true;
            this.btDirSource.Click += new System.EventHandler(this.btDirSource_Click);
            // 
            // btDirResult
            // 
            this.btDirResult.Location = new System.Drawing.Point(400, 104);
            this.btDirResult.Name = "btDirResult";
            this.btDirResult.Size = new System.Drawing.Size(30, 20);
            this.btDirResult.TabIndex = 6;
            this.btDirResult.Text = "...";
            this.btDirResult.UseVisualStyleBackColor = true;
            this.btDirResult.Click += new System.EventHandler(this.btDirResult_Click);
            // 
            // lbDirResult
            // 
            this.lbDirResult.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lbDirResult.AutoSize = true;
            this.lbDirResult.Location = new System.Drawing.Point(12, 107);
            this.lbDirResult.Name = "lbDirResult";
            this.lbDirResult.Size = new System.Drawing.Size(158, 13);
            this.lbDirResult.TabIndex = 5;
            this.lbDirResult.Text = "Seleccione directorio de destino";
            // 
            // txDirResult
            // 
            this.txDirResult.Enabled = false;
            this.txDirResult.Location = new System.Drawing.Point(171, 104);
            this.txDirResult.Name = "txDirResult";
            this.txDirResult.Size = new System.Drawing.Size(225, 20);
            this.txDirResult.TabIndex = 4;
            // 
            // pgbFile
            // 
            this.pgbFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pgbFile.Location = new System.Drawing.Point(12, 182);
            this.pgbFile.Name = "pgbFile";
            this.pgbFile.Size = new System.Drawing.Size(439, 23);
            this.pgbFile.TabIndex = 7;
            this.pgbFile.Visible = false;
            // 
            // lbProgress
            // 
            this.lbProgress.AutoSize = true;
            this.lbProgress.Location = new System.Drawing.Point(15, 166);
            this.lbProgress.Name = "lbProgress";
            this.lbProgress.Size = new System.Drawing.Size(0, 13);
            this.lbProgress.TabIndex = 8;
            this.lbProgress.Visible = false;
            // 
            // btExecute
            // 
            this.btExecute.Location = new System.Drawing.Point(347, 143);
            this.btExecute.Name = "btExecute";
            this.btExecute.Size = new System.Drawing.Size(83, 23);
            this.btExecute.TabIndex = 9;
            this.btExecute.Text = "Extraer datos";
            this.btExecute.UseVisualStyleBackColor = true;
            this.btExecute.Click += new System.EventHandler(this.btExecute_Click);
            // 
            // formTest001
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 217);
            this.Controls.Add(this.btExecute);
            this.Controls.Add(this.lbProgress);
            this.Controls.Add(this.pgbFile);
            this.Controls.Add(this.btDirResult);
            this.Controls.Add(this.lbDirResult);
            this.Controls.Add(this.txDirResult);
            this.Controls.Add(this.btDirSource);
            this.Controls.Add(this.lbDirSource);
            this.Controls.Add(this.txDirSource);
            this.Controls.Add(this.lbInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formTest001";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TEST001: PRUEBA EXTRACCIÓN DATOS PDF CON ITEXT";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.FolderBrowserDialog fdBrw;
        private System.Windows.Forms.TextBox txDirSource;
        private System.Windows.Forms.Label lbDirSource;
        private System.Windows.Forms.Button btDirSource;
        private System.Windows.Forms.Button btDirResult;
        private System.Windows.Forms.Label lbDirResult;
        private System.Windows.Forms.TextBox txDirResult;
        private System.Windows.Forms.ProgressBar pgbFile;
        private System.Windows.Forms.Label lbProgress;
        private System.Windows.Forms.Button btExecute;
    }
}

