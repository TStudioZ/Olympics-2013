using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Form1 : Form
    {
        private FormGridSize formSize;
        private Grid grid;
        public void EnableTimerHideNotWonCards()
        {
            this.timerHideCards.Enabled = true;
        }
        public void DisableTimerHideNotWonCards()
        {
            this.timerHideCards.Enabled = false;
        }
        public void EnableTimerShowingCards()
        {
            this.timerShowingAll.Enabled = true;
        }
        public void DisableTimerShowingCards()
        {
            this.timerShowingAll.Enabled = false;
        }
        public Form1()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            formSize = new FormGridSize(this);
            NewGame();
        }

        public void ShowWonDialog(Counter counter)
        {
            new FormWon(this, counter).ShowDialog();
        }

        public void InitGrid(int size, bool showUpwards)
        {
            grid = new Grid(this, size, showUpwards);
            this.Invalidate();
        }

        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {
            if (grid != null)
                grid.Paint(e.Graphics);
            labelSuccessfulTurns.Text = grid.GameCounter.NumberOfSuccessfulTurns.ToString();
            labelTurns.Text = grid.GameCounter.NumberOfTurns.ToString();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            grid.MouseClick(e.X, e.Y);
        }

        private void timerHideCards_Tick(object sender, EventArgs e)
        {
            grid.HideNotWonCards();
        }

        private void timerShowingAll_Tick(object sender, EventArgs e)
        {
            grid.HideAll();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        public void NewGame()
        {
            formSize.ShowDialog();
        }

        public void QuitGame()
        {
            Application.Exit();
        }
    }
}
