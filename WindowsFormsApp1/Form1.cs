using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BE;
using BLL;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //---------------- parámetros visuales ----------------
        private const int Lanes = 5;
        private const int SpawnInterval = 1500;   // ms
        private const int EnemyPixelVel = 4;      // px/tick
        private const int BulletPixelVel = 15;
        private const int TotalKm = 300;

        //---------------- motor ----------------
        private readonly GameEngine _engine = new GameEngine();   
        private string _playerNick;

        //---------------- sprites UI ----------------
        private readonly Dictionary<int, PictureBox> _enemySprites = new Dictionary<int, PictureBox>();
        private readonly List<PictureBox> _bullets = new List<PictureBox>();

        //---------------- estado UI ----------------
        private readonly Random _rnd = new Random();
        private int _playerLane;
        private int _laneH;
        private double _kmPerPixel;

        public Form1() => InitializeComponent();

        //================== LOAD ==================
        private void Form1_Load(object sender, EventArgs e)
        {
            // 1) Nickname
            _playerNick = AskNick();
            _engine.StartGame(_playerNick);        

            // 2) Sprites base
            picBase.Image = Properties.Resources.ship_friend;
            picBase.BackColor = Color.Transparent;

            _playerLane = Lanes / 2;
            _laneH = (pnlGame.Height - 40) / Lanes;

            int pxRecorrido = pnlGame.Width - (picBase.Left + picBase.Width) - 10;
            _kmPerPixel = (double)TotalKm / pxRecorrido;
            _engine.KmPerPixel = _kmPerPixel;

            _engine.StateChanged += Redraw;
            UpdateBasePos();

            // 3) Timers
            tmrMove.Interval = 25;
            tmrSpawn.Interval = SpawnInterval;
            tmrMove.Start();
            tmrSpawn.Start();
        }


        private string AskNick()
        {
            using (var dlg = new Form())
            {
                dlg.Text = "Jugador";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new Size(260, 130);
                dlg.MinimizeBox = dlg.MaximizeBox = false;

                var lbl = new Label
                {
                    Left = 10,
                    Top = 15,
                    Width = 240,
                    Text = "Ingresa tu nombre:"
                };
                var txt = new TextBox { Left = 10, Top = 45, Width = 240 };
                var ok = new Button
                {
                    Left = 70,
                    Top = 80,
                    Width = 110,
                    Text = "OK",
                    DialogResult = DialogResult.OK
                };

                dlg.Controls.AddRange(new Control[] { lbl, txt, ok });
                dlg.AcceptButton = ok;

                return dlg.ShowDialog(this) == DialogResult.OK &&
                       !string.IsNullOrWhiteSpace(txt.Text)
                       ? txt.Text.Trim()
                       : "Jugador";
            }
        }

        //================== SPAWNER ==================
        private void tmrSpawn_Tick(object sender, EventArgs e)
        {
            int lane = _rnd.Next(Lanes);
            _engine.Spawn(lane, TotalKm);           
        }

        //================== LOOP ==================
        private void tmrMove_Tick(object sender, EventArgs e)
        {
            _engine.Tick(EnemyPixelVel);           
            MoveBullets();                         
        }

        //================== INPUT ==================
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && _playerLane > 0)
            { _playerLane--; UpdateBasePos(); }

            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && _playerLane < Lanes - 1)
            { _playerLane++; UpdateBasePos(); }

            else if (e.KeyCode == Keys.Space) Fire();
        }
        private void btnFire_Click(object s, EventArgs e) => Fire();

        //================== DISPARO ==================
        private void Fire()
        {
            var shot = _engine.Fire(_playerLane);
            if (shot == null) return;               // nada en carril

            // bala amarilla (solo visual)
            var b = new PictureBox
            {
                Size = new Size(12, 4),
                BackColor = Color.Yellow,
                Tag = _playerLane
            };
            b.Left = picBase.Right;
            b.Top = LaneToY(_playerLane) + (_laneH - b.Height) / 2;
            pnlGame.Controls.Add(b);
            pnlGame.Controls.SetChildIndex(b, 0);
            _bullets.Add(b);
        }

        //================== REDRAW (callback BLL) ==================
        private void Redraw()
        {
            //--- sincroniza sprites enemigos ---
            foreach (var id in _enemySprites.Keys.Except(_engine.Targets.Select(t => t.Id)).ToList())
            {
                var pb = _enemySprites[id];
                pnlGame.Controls.Remove(pb);
                pb.Dispose();
                _enemySprites.Remove(id);
            }
            foreach (var t in _engine.Targets)
            {
                if (!_enemySprites.TryGetValue(t.Id, out var pb))
                {
                    pb = new PictureBox
                    {
                        Size = new Size(64, 64),
                        BackColor = Color.Transparent,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Properties.Resources.ship_enemy
                    };
                    pnlGame.Controls.Add(pb);
                    pnlGame.Controls.SetChildIndex(pb, 0);
                    _enemySprites.Add(t.Id, pb);
                }
                pb.Left = picBase.Right + (int)Math.Round(t.DistanceKm / _kmPerPixel);
                pb.Top = LaneToY(t.Lane) + (_laneH - pb.Height) / 2;
            }

            //--- HUD ---
            lblLives.Text = $"Vidas: {_engine.Lives}";
            lblScore.Text = $"Aciertos: {_engine.Hits}";

            var nearest = _engine.Targets
                                 .Where(t => t.Lane == _playerLane)
                                 .OrderBy(t => t.DistanceKm)
                                 .FirstOrDefault();

            if (nearest == null)
            {
                lblWeapon.Text = "Arma: —";
                lblDistance.Text = "Distancia: — km";
            }
            else
            {
                double km = nearest.DistanceKm;
                string arma = km < 10 ? "Cañón Corto"
                           : km <= 50 ? "Ultrasónico"
                           : km <= 200 ? "Láser"
                           : "Fuera de rango";
                lblWeapon.Text = $"Arma: {arma}";
                lblDistance.Text = $"Distancia: {km:0} km";
            }

            //--- game‑over ---
            if (_engine.Lives <= 0)
            {
                _engine.EndGame();                   // guarda en BD
                tmrMove.Stop(); tmrSpawn.Stop();

                if (MessageBox.Show("¡Game Over!\n¿Reiniciar?", "Fin",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var s in _enemySprites.Values) s.Dispose();
                    foreach (var b in _bullets) b.Dispose();
                    _enemySprites.Clear();
                    _bullets.Clear();

                    _engine.StartGame(_playerNick);  // nueva sesión
                    tmrMove.Start(); tmrSpawn.Start();
                }
                else
                {
                    Close();
                }
            }
        }

        //================== BULLET MOTION ==================
        private void MoveBullets()
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var b = _bullets[i];
                b.Left += BulletPixelVel;
                if (b.Left > pnlGame.Width)
                {
                    pnlGame.Controls.Remove(b);
                    _bullets.RemoveAt(i);
                    b.Dispose();
                }
            }
        }

        //================== HELPERS ==================
        private void UpdateBasePos()
        {
            picBase.Left = 20;
            picBase.Top = LaneToY(_playerLane) + (_laneH - picBase.Height) / 2;
        }
        private int LaneToY(int lane) => 20 + lane * _laneH;

        private void pnlGame_Paint(object s, PaintEventArgs e)
        {
            using (var pen = new Pen(Color.DimGray) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                for (int i = 0; i <= Lanes; i++)
                    e.Graphics.DrawLine(pen, 0, 20 + i * _laneH, pnlGame.Width, 20 + i * _laneH);
        }
    }
}
