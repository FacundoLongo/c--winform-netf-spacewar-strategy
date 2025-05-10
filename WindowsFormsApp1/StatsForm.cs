using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace WindowsFormsApp1
{
    public partial class StatsForm : Form
    {
        private readonly GameEngine _engine;  
        private readonly string _nick;    

        public StatsForm(GameEngine engine, string nick)
        {
            _engine = engine;
            _nick = nick;
            InitializeComponent();
        }

        private void StatsForm_Load(object sender, EventArgs e)
        {
            var me = _engine.GetPlayerStats(_nick);
            lblPlayer.Text = $"Jugador: {_nick}";
            lblGames.Text = $"Partidas:  {me.Games}";
            lblHits.Text = $"Aciertos:  {me.Hits}";
            lblShots.Text = $"Disparos:  {me.Shots}";
            lblAcc.Text = $"Precisión: {me.Accuracy:0.##} %";
            lblHiScore.Text = $"High‑score: {me.HighScore}";

            dgvAll.DataSource = _engine.GetStatsTable();
            dgvAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAll.ReadOnly = true;
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();
    }
}