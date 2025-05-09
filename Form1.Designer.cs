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
            this.components = new Container();
            this.pnlGame = new Panel();
            this.picBase = new PictureBox();
            this.btnFire = new Button();
            this.lblWeapon = new Label();
            this.lblDistance = new Label();
            this.lblScore = new Label();
            this.tmrMove = new Timer(this.components);
            this.tmrSpawn = new Timer(this.components);

            // -----------------------------------------------------------------
            // Form1
            // -----------------------------------------------------------------
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1033, 681);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Saraza S.A. – Lanes Shooter";
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.KeyPreview = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);

            // -----------------------------------------------------------------
            // pnlGame
            // -----------------------------------------------------------------
            this.pnlGame.BackColor = Color.FromArgb(20, 20, 20);
            this.pnlGame.BorderStyle = BorderStyle.FixedSingle;
            this.pnlGame.Location = new Point(12, 12);
            this.pnlGame.Size = new Size(800, 600);
            this.pnlGame.Paint += new PaintEventHandler(this.pnlGame_Paint);

            // -----------------------------------------------------------------
            // picBase
            // -----------------------------------------------------------------
            this.picBase.BackColor = Color.Transparent;
            this.picBase.Size = new Size(64, 64);
            this.picBase.Location = new Point(20, 20);        // se reubica en Load
            this.picBase.SizeMode = PictureBoxSizeMode.Zoom;

            this.pnlGame.Controls.Add(this.picBase);

            // -----------------------------------------------------------------
            // btnFire
            // -----------------------------------------------------------------
            this.btnFire.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.btnFire.Location = new Point(830, 550);
            this.btnFire.Size = new Size(180, 60);
            this.btnFire.Text = "DISPARAR";
            this.btnFire.Click += new System.EventHandler(this.btnFire_Click);

            // -----------------------------------------------------------------
            // lblWeapon
            // -----------------------------------------------------------------
            this.lblWeapon.AutoSize = true;
            this.lblWeapon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblWeapon.ForeColor = Color.White;
            this.lblWeapon.Location = new Point(830, 50);
            this.lblWeapon.Text = "Arma: —";

            // -----------------------------------------------------------------
            // lblDistance
            // -----------------------------------------------------------------
            this.lblDistance.AutoSize = true;
            this.lblDistance.Font = new Font("Segoe UI", 10F);
            this.lblDistance.ForeColor = Color.White;
            this.lblDistance.Location = new Point(830, 80);
            this.lblDistance.Text = "Distancia: — km";

            // -----------------------------------------------------------------
            // lblScore
            // -----------------------------------------------------------------
            this.lblScore.AutoSize = true;
            this.lblScore.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblScore.ForeColor = Color.Lime;
            this.lblScore.Location = new Point(830, 110);
            this.lblScore.Text = "Aciertos: 0";

            // lblLives
            this.lblLives = new Label();
            this.lblLives.AutoSize = true;
            this.lblLives.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblLives.ForeColor = Color.Orange;
            this.lblLives.Location = new Point(830, 140);
            this.lblLives.Text = "Vidas: 3";
            this.Controls.Add(this.lblLives);


            // -----------------------------------------------------------------
            // tmrMove & tmrSpawn
            // -----------------------------------------------------------------
            this.tmrMove.Interval = 25;     // movimiento
            this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);

            this.tmrSpawn.Interval = 1500;   // oleada
            this.tmrSpawn.Tick += new System.EventHandler(this.tmrSpawn_Tick);

            // -----------------------------------------------------------------
            // agregar controles
            // -----------------------------------------------------------------
            this.Controls.Add(this.pnlGame);
            this.Controls.Add(this.btnFire);
            this.Controls.Add(this.lblWeapon);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.lblScore);
        }
        #endregion
    }
}
