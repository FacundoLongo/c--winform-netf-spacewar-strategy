using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // ---- CONFIG BÁSICA ----
        private const int Lanes = 5;
        private const int Spawn0 = 1500;   // ms
        private const int EnemySpeed0 = 4;      // px/tick
        private const int BulletSpeed = 15;
        private const int TotalKm = 300;    // recorrido total

        // Rangos muy cortos (en km)
        private const int KmCorto = 5;
        private const int KmMedio = 50;
        private const int KmLargo = 200;

        private const int MaxLives = 3;

        // ---- estado ----
        private int _lives = MaxLives;
        private int _enemySpeed = EnemySpeed0;
        private int _playerLane;
        private int _hits;
        private double _kmPorPixel;                // se calcula en Load

        private readonly Random _rnd = new Random();
        private readonly List<PictureBox> _enemies = new List<PictureBox>();
        private readonly List<PictureBox> _bullets = new List<PictureBox>();

        private PictureBox _sel; Color _oldBack;

        private int LaneH => (pnlGame.Height - 40) / Lanes;
        public Form1() => InitializeComponent();

        // ================= LOAD =================
        private void Form1_Load(object sender, EventArgs e)
        {
            // sprite base (fallback color)

            picBase.Image = Properties.Resources.ship_friend;  
            if (picBase.Image == null) picBase.BackColor = Color.Cyan;

            _playerLane = Lanes / 2;
            UpdateBasePos();


            // calculamos km por pixel (300 km en todo el recorrido)
            int pxRecorrido =
                pnlGame.Width - (picBase.Left + picBase.Width) - 10; // margen spawn
            _kmPorPixel = (double)TotalKm / pxRecorrido;

            tmrMove.Start();
            tmrSpawn.Interval = Spawn0;
            tmrSpawn.Start();

            lblLives.Text = "Vidas: " + _lives;
        }

        // ================= SPAWN =================
        private void tmrSpawn_Tick(object sender, EventArgs e)
        {
            int lane = _rnd.Next(Lanes);

            var en = new PictureBox
            {
                Size = new Size(48, 48),
                BackColor = Color.Transparent,
                SizeMode = PictureBoxSizeMode.Zoom,
                Tag = lane,
                Image = Properties.Resources.ship_enemy
            };
            en.Image = Properties.Resources.ResourceManager.GetObject("Enemy") as Image;
            if (en.Image == null) en.BackColor = Color.Red;

            en.Left = pnlGame.Width - en.Width - 10;
            en.Top = LaneToY(lane) + (LaneH - en.Height) / 2;

            pnlGame.Controls.Add(en);
            pnlGame.Controls.SetChildIndex(en, 0);
            _enemies.Add(en);

            UpdateSel();
        }

        // ================ TICK PRINCIPAL ================
        private void tmrMove_Tick(object sender, EventArgs e)
        {
            // mover enemigos
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                var en = _enemies[i];
                en.Left -= _enemySpeed;
                if (en.Left <= picBase.Right) { RemoveEnemy(en); LoseLife(); }
                else if (en.Right < 0) RemoveEnemy(en);
            }
            // mover balas
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var b = _bullets[i]; b.Left += BulletSpeed;
                var hit = _enemies.FirstOrDefault(en =>
                       (int)en.Tag == (int)b.Tag && b.Bounds.IntersectsWith(en.Bounds));
                if (hit != null) { RemoveEnemy(hit); RemoveBullet(b); RegHit(); }
                else if (b.Left > pnlGame.Width) RemoveBullet(b);
            }
            UpdateSel(); // refresca arma cada ~5 px (~2 km)
        }

        // ================ INPUT ================
        private void Form1_KeyDown(object s, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && _playerLane > 0)
            { _playerLane--; UpdateBasePos(); }
            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && _playerLane < Lanes - 1)
            { _playerLane++; UpdateBasePos(); }
            else if (e.KeyCode == Keys.Space) Fire();
        }
        private void btnFire_Click(object s, EventArgs e) => Fire();

        // ================ DISPARO =================
        private void Fire()
        {
            if (_sel == null) return;
            double km = DistKm(_sel);
            if (km > KmLargo) return;                             // fuera de rango
            if (_bullets.Any(b => (int)b.Tag == _playerLane)) return;// bala activa

            var bullet = new PictureBox
            {
                Size = new Size(12, 4),
                BackColor = Color.Yellow,
                Tag = _playerLane
            };
            bullet.Left = picBase.Right;
            bullet.Top = LaneToY(_playerLane) + (LaneH - bullet.Height) / 2;

            pnlGame.Controls.Add(bullet);
            pnlGame.Controls.SetChildIndex(bullet, 0);
            _bullets.Add(bullet);
        }

        // ================ SELECCIÓN / HUD =================
        private void UpdateSel()
        {
            if (_sel != null) { _sel.BorderStyle = 0; _sel.BackColor = _oldBack; _sel = null; }

            var Near = _enemies.Where(en => (int)en.Tag == _playerLane)
                             .OrderBy(en => en.Left).FirstOrDefault();
            if (Near == null) { lblWeapon.Text = "Arma: —"; lblDistance.Text = "Distancia: — km"; return; }

            _sel = Near; _oldBack = Near.BackColor; double km = DistKm(Near);

            string arma; Color c;
            if (km < KmCorto) { arma = "Cañón Corto"; c = Color.Lime; }
            else if (km <= KmMedio) { arma = "Ultrasónico"; c = Color.DodgerBlue; }
            else if (km <= KmLargo) { arma = "Láser"; c = Color.Red; }
            else { arma = "Fuera de rango"; c = Color.Gray; }

            lblWeapon.Text = "Arma: " + arma;
            lblDistance.Text = $"Distancia: {km:0} km";

            Near.BorderStyle = BorderStyle.FixedSingle;
            Near.BackColor = c;
        }

        // ================ HIT / DIFICULTAD =================
        private void RegHit()
        {
            _hits++; lblScore.Text = "Aciertos: " + _hits;
            if (_hits % 5 != 0) return;
            if (_enemySpeed < 12) _enemySpeed++;
            if (tmrSpawn.Interval > 400) tmrSpawn.Interval -= 100;
        }

        // ================ VIDA =================
        private void LoseLife()
        {
            _lives--; lblLives.Text = "Vidas: " + _lives;
            if (_lives > 0) return;

            tmrMove.Stop(); tmrSpawn.Stop();
            if (MessageBox.Show("¡Game Over!\n¿Reiniciar?", "Fin",
                 MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ResetGame();
        }
        private void ResetGame()
        {
            foreach (var en in _enemies.ToList()) RemoveEnemy(en);
            foreach (var b in _bullets.ToList()) RemoveBullet(b);

            _lives = MaxLives; _enemySpeed = EnemySpeed0; _hits = 0;
            lblLives.Text = "Vidas: " + _lives; lblScore.Text = "Aciertos: 0";
            tmrSpawn.Interval = Spawn0; tmrMove.Start(); tmrSpawn.Start();
            UpdateSel();
        }

        // ================ HELPERS =================
        private void RemoveEnemy(PictureBox en)
        {
            if (en == _sel) _sel = null;
            pnlGame.Controls.Remove(en); _enemies.Remove(en); en.Dispose();
        }
        private void RemoveBullet(PictureBox b)
        {
            pnlGame.Controls.Remove(b); _bullets.Remove(b); b.Dispose();
        }

        private void UpdateBasePos()
        {
            picBase.Left = 20;
            picBase.Top = LaneToY(_playerLane) + (LaneH - picBase.Height) / 2;
            UpdateSel();
        }
        private int LaneToY(int lane) => 20 + lane * LaneH;
        private double DistKm(PictureBox en) =>
            (en.Left - (picBase.Left + picBase.Width)) * _kmPorPixel;

        // ================ PINTA LÍNEAS =================
        private void pnlGame_Paint(object s, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.DimGray, 1)
            {
                DashStyle =
                System.Drawing.Drawing2D.DashStyle.Dash
            })
                for (int i = 0; i <= Lanes; i++)
                    e.Graphics.DrawLine(pen, 0, 20 + i * LaneH, pnlGame.Width, 20 + i * LaneH);
        }
    }
}
