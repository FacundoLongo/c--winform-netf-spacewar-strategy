using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL;  
using BE;   

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //---------------- parámetros gráficos ----------------
        private const int Lanes = 5;
        private const int SpawnInterval = 1500;  // ms
        private const int EnemyPixelVel = 4;     // px / tick
        private const int BulletPixelVel = 15;
        private const int TotalKm = 300;

        //---------------- motor ----------------
        private readonly GameEngine _engine = new GameEngine();
        private string _playerNick = "Jugador";

        //---------------- sprites ----------------
        private readonly Dictionary<int, PictureBox> _enemySprites =
            new Dictionary<int, PictureBox>();      // id → sprite

        private PictureBox _bullet;                 // solo UNA bala
        private int _bulletTargetId;

        //---------------- estado UI ----------------
        private readonly Random _rnd = new Random();
        private int _playerLane;
        private int _laneH;
        private double _kmPerPixel;

        // ---- control de pausa ----------------------
        private bool _paused;

        // ---- high‑score del jugador ---------------
        private int _hiScore;


        public Form1() => InitializeComponent();

        //======================================================
        //                      LOAD
        //======================================================
        private void Form1_Load(object sender, EventArgs e)
        {
            // 1) jugador
            _playerNick = AskNick();
            _engine.StartGame(_playerNick);

            // 2) sprite base
            picBase.Image = Properties.Resources.ship_friend;
            picBase.BackColor = Color.Transparent;

            _playerLane = Lanes / 2;
            _laneH = (pnlGame.Height - 40) / Lanes;

            int pxRecorrido = pnlGame.Width - (picBase.Left + picBase.Width) - 10;
            _kmPerPixel = (double)TotalKm / pxRecorrido;
            _engine.KmPerPixel = _kmPerPixel;

            _engine.StateChanged += Redraw;
            UpdateBasePos();

            _hiScore = _engine.GetHighScore(_playerNick);
            lblHi.Text = $"High‑score: {_hiScore}";

            // 3) timers
            tmrMove.Interval = 25;
            tmrSpawn.Interval = SpawnInterval;
            tmrMove.Start();
            tmrSpawn.Start();
        }

        //======================================================
        //              Diálogo para nombre de jugador
        //======================================================
        private string AskNick()
        {
            using (Form dlg = new Form())
            {
                dlg.Text = "Jugador";
                dlg.FormBorderStyle = FormBorderStyle.FixedDialog;
                dlg.StartPosition = FormStartPosition.CenterParent;
                dlg.ClientSize = new Size(260, 130);
                dlg.MinimizeBox = dlg.MaximizeBox = false;

                Label lbl = new Label { Left = 10, Top = 15, Width = 240, Text = "Ingresa tu nombre:" };
                TextBox txt = new TextBox { Left = 10, Top = 45, Width = 240 };
                Button ok = new Button
                {
                    Left = 70,
                    Top = 80,
                    Width = 110,
                    Text = "OK",
                    DialogResult = DialogResult.OK
                };

                dlg.Controls.AddRange(new Control[] { lbl, txt, ok });
                dlg.AcceptButton = ok;

                return dlg.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text)
                     ? txt.Text.Trim()
                     : "Jugador";
            }
        }

        //======================================================
        //                  SPAWNER
        //======================================================
        private void tmrSpawn_Tick(object sender, EventArgs e)
        {
            _engine.Spawn(_rnd.Next(Lanes), TotalKm);
        }

        //======================================================
        //                  LOOP PRINCIPAL
        //======================================================
        private void tmrMove_Tick(object sender, EventArgs e)
        {
            _engine.Tick(EnemyPixelVel);
            MoveBullet();
        }

        //======================================================
        //                      INPUT
        //======================================================
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Up || e.KeyCode == Keys.W) && _playerLane > 0)
            {
                _playerLane--; UpdateBasePos();
            }
            else if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.S) && _playerLane < Lanes - 1)
            {
                _playerLane++; UpdateBasePos();
            }
            else if (e.KeyCode == Keys.Space)
            {
                Fire();
            }
        }
        private void btnFire_Click(object s, EventArgs e) => Fire();

        //======================================================
        //                      DISPARO
        //======================================================
        private void Fire()
        {
            if (_bullet != null) return;                     // ya hay bala en vuelo

            Shot shot = _engine.Fire(_playerLane);           // motor decide
            if (shot == null || shot.Weapon == WeaponType.None) return; // fuera de rango

            _bulletTargetId = shot.Target.Id;

            _bullet = new PictureBox
            {
                Size = new Size(12, 4),
                BackColor = Color.Yellow
            };
            _bullet.Left = picBase.Right;
            _bullet.Top = LaneToY(_playerLane) + (_laneH - _bullet.Height) / 2;

            pnlGame.Controls.Add(_bullet);
            pnlGame.Controls.SetChildIndex(_bullet, 0);
        }

        //======================================================
        //              REDRAW – callback del motor
        //======================================================
        private void Redraw()
        {
            //---------------- sincroniza sprites enemigos ----------------
            foreach (int id in _enemySprites.Keys.Except(_engine.Targets.Select(t => t.Id)).ToList())
            {
                pnlGame.Controls.Remove(_enemySprites[id]);
                _enemySprites[id].Dispose();
                _enemySprites.Remove(id);
            }

            foreach (Target t in _engine.Targets)
            {
                PictureBox pb;
                if (!_enemySprites.TryGetValue(t.Id, out pb))
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

                //---------------- marcado ----------------
                bool isNearest = t.Lane == _playerLane &&
                                 Math.Abs(t.DistanceKm - _engine.Targets
                                                          .Where(x => x.Lane == _playerLane)
                                                          .Min(x => x.DistanceKm)) < 0.01;

                if (isNearest)
                {
                    bool inRange = t.DistanceKm <= 200;
                    pb.BorderStyle = BorderStyle.FixedSingle;
                    pb.BackColor = inRange ? Color.Lime : Color.Red;   // ← verde / rojo
                }
                else
                {
                    pb.BorderStyle = BorderStyle.None;
                    pb.BackColor = Color.Transparent;
                }
            }

            //---------------- HUD ----------------
            lblLives.Text = $"Vidas: {_engine.Lives}";
            lblScore.Text = $"Aciertos: {_engine.Hits}";

            Target nearest = _engine.Targets.Where(t => t.Lane == _playerLane)
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

            //---------------- GAME OVER ----------------
            if (_engine.Lives <= 0)
            {
                _engine.EndGame();
                tmrMove.Stop(); tmrSpawn.Stop();

                DialogResult res = MessageBox.Show("¡Game Over!\n¿Reiniciar?",
                                                   "Fin",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    foreach (PictureBox s in _enemySprites.Values) s.Dispose();
                    _enemySprites.Clear();
                    _bullet?.Dispose(); _bullet = null;

                    _engine.StartGame(_playerNick);
                    tmrMove.Start(); tmrSpawn.Start();
                }
                else
                {
                    Close();
                }
            }
            _hiScore = Math.Max(_hiScore, _engine.Hits);
            lblHi.Text = $"High‑score: {_hiScore}";
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            _paused = !_paused;
            tmrMove.Enabled = !_paused;
            tmrSpawn.Enabled = !_paused;
            btnPause.Text = _paused ? "CONTINUAR" : "PAUSA";
        }

        private void btnStats_Click(object sender, EventArgs e)
        {
            using (var frm = new StatsForm(_engine, _playerNick))
                frm.ShowDialog(this);
        }

        //======================================================
        //                MOVIMIENTO DE LA BALA
        //======================================================
        private void MoveBullet()
        {
            if (_bullet == null) return;

            _bullet.Left += BulletPixelVel;

            // impacto
            PictureBox enemySprite;
            if (_enemySprites.TryGetValue(_bulletTargetId, out enemySprite) &&
                _bullet.Bounds.IntersectsWith(enemySprite.Bounds))
            {
                _engine.RegisterHit(_bulletTargetId);

                pnlGame.Controls.Remove(enemySprite);
                enemySprite.Dispose();
                _enemySprites.Remove(_bulletTargetId);

                pnlGame.Controls.Remove(_bullet);
                _bullet.Dispose();
                _bullet = null;
                return;
            }

            // fuera de pantalla
            if (_bullet.Left > pnlGame.Width)
            {
                pnlGame.Controls.Remove(_bullet);
                _bullet.Dispose();
                _bullet = null;
            }
        }

        //======================================================
        //                       HELPERS
        //======================================================
        private void UpdateBasePos()
        {
            picBase.Left = 20;
            picBase.Top = LaneToY(_playerLane) + (_laneH - picBase.Height) / 2;
        }
        private int LaneToY(int lane) => 20 + lane * _laneH;

        private void pnlGame_Paint(object s, PaintEventArgs e)
        {
            using (Pen pen = new Pen(Color.DimGray)
            { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
            {
                for (int i = 0; i <= Lanes; i++)
                    e.Graphics.DrawLine(pen, 0, 20 + i * _laneH, pnlGame.Width, 20 + i * _laneH);
            }
        }
    }
}
