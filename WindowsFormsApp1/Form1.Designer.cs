using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    partial class Form1
    {
        private IContainer components = null;

        private Panel pnlGame;
        private PictureBox picBase;
        private Button btnFire;
        private Label lblWeapon;
        private Label lblDistance;
        private Label lblScore;
        private Timer tmrMove;
        private Timer tmrSpawn;
        private Label lblLives;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlGame = new System.Windows.Forms.Panel();
            this.picBase = new System.Windows.Forms.PictureBox();
            this.btnFire = new System.Windows.Forms.Button();
            this.lblWeapon = new System.Windows.Forms.Label();
            this.lblDistance = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.tmrMove = new System.Windows.Forms.Timer(this.components);
            this.tmrSpawn = new System.Windows.Forms.Timer(this.components);
            this.lblLives = new System.Windows.Forms.Label();
            this.pnlGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBase)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGame
            // 
            this.pnlGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.pnlGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlGame.Controls.Add(this.picBase);
            this.pnlGame.Location = new System.Drawing.Point(12, 12);
            this.pnlGame.Name = "pnlGame";
            this.pnlGame.Size = new System.Drawing.Size(800, 600);
            this.pnlGame.TabIndex = 1;
            this.pnlGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGame_Paint);
            // 
            // picBase
            // 
            this.picBase.BackColor = System.Drawing.Color.Transparent;
            this.picBase.Location = new System.Drawing.Point(20, 20);
            this.picBase.Name = "picBase";
            this.picBase.Size = new System.Drawing.Size(64, 64);
            this.picBase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBase.TabIndex = 0;
            this.picBase.TabStop = false;
            // 
            // btnFire
            // 
            this.btnFire.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnFire.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnFire.Location = new System.Drawing.Point(830, 550);
            this.btnFire.Name = "btnFire";
            this.btnFire.Size = new System.Drawing.Size(180, 60);
            this.btnFire.TabIndex = 2;
            this.btnFire.Text = "DISPARAR";
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);
            // 
            // lblWeapon
            // 
            this.lblWeapon.AutoSize = true;
            this.lblWeapon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWeapon.ForeColor = System.Drawing.Color.White;
            this.lblWeapon.Location = new System.Drawing.Point(830, 50);
            this.lblWeapon.Name = "lblWeapon";
            this.lblWeapon.Size = new System.Drawing.Size(75, 21);
            this.lblWeapon.TabIndex = 3;
            this.lblWeapon.Text = "Arma: —";
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDistance.ForeColor = System.Drawing.Color.White;
            this.lblDistance.Location = new System.Drawing.Point(830, 80);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(108, 19);
            this.lblDistance.TabIndex = 4;
            this.lblDistance.Text = "Distancia: — km";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblScore.ForeColor = System.Drawing.Color.Lime;
            this.lblScore.Location = new System.Drawing.Point(830, 110);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(80, 19);
            this.lblScore.TabIndex = 5;
            this.lblScore.Text = "Aciertos: 0";
            // 
            // tmrMove
            // 
            this.tmrMove.Interval = 25;
            this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);
            // 
            // tmrSpawn
            // 
            this.tmrSpawn.Interval = 1500;
            this.tmrSpawn.Tick += new System.EventHandler(this.tmrSpawn_Tick);
            // 
            // lblLives
            // 
            this.lblLives.AutoSize = true;
            this.lblLives.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLives.ForeColor = System.Drawing.Color.Orange;
            this.lblLives.Location = new System.Drawing.Point(830, 140);
            this.lblLives.Name = "lblLives";
            this.lblLives.Size = new System.Drawing.Size(61, 19);
            this.lblLives.TabIndex = 0;
            this.lblLives.Text = "Vidas: 3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1033, 681);
            this.Controls.Add(this.lblLives);
            this.Controls.Add(this.pnlGame);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.lblWeapon);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Spacewar Shooter with Strategy :D";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.pnlGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
    }
}
