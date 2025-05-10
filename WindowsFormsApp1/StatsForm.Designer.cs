namespace WindowsFormsApp1
{
    partial class StatsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblGames;
        private System.Windows.Forms.Label lblHits;
        private System.Windows.Forms.Label lblShots;
        private System.Windows.Forms.Label lblAcc;
        private System.Windows.Forms.Label lblHiScore;
        private System.Windows.Forms.DataGridView dgvAll;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblGames = new System.Windows.Forms.Label();
            this.lblHits = new System.Windows.Forms.Label();
            this.lblShots = new System.Windows.Forms.Label();
            this.lblAcc = new System.Windows.Forms.Label();
            this.lblHiScore = new System.Windows.Forms.Label();
            this.dgvAll = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAll)).BeginInit();
            this.SuspendLayout();
            // 
            // -- etiquetas jugador ----------------------------------------
            //
            var top = 15; var left = 15; var sep = 22;
            this.lblPlayer.SetBounds(left, top + sep * 0, 350, 20);
            this.lblGames.SetBounds(left, top + sep * 1, 350, 20);
            this.lblHits.SetBounds(left, top + sep * 2, 350, 20);
            this.lblShots.SetBounds(left, top + sep * 3, 350, 20);
            this.lblAcc.SetBounds(left, top + sep * 4, 350, 20);
            this.lblHiScore.SetBounds(left, top + sep * 5, 350, 20);

            foreach (var lbl in new[] { lblPlayer,lblGames,lblHits,
                                        lblShots,lblAcc,lblHiScore })
            {
                lbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                lbl.ForeColor = System.Drawing.Color.Gold;
                this.Controls.Add(lbl);
            }
            // 
            // dgvAll  
            // 
            this.dgvAll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top
                                      | System.Windows.Forms.AnchorStyles.Bottom)
                                      | System.Windows.Forms.AnchorStyles.Left)
                                      | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAll.Location = new System.Drawing.Point(15, top + sep * 7);
            this.dgvAll.Name = "dgvAll";
            this.dgvAll.Size = new System.Drawing.Size(760, 280);
            this.dgvAll.TabIndex = 100;
            this.Controls.Add(this.dgvAll);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Text = "Cerrar";
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.Location = new System.Drawing.Point(335, 370);
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.Controls.Add(this.btnClose);
            // 
            // StatsForm 
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.ClientSize = new System.Drawing.Size(790, 420);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Estadísticas";
            this.Load += new System.EventHandler(this.StatsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAll)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
    }
}
