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
    public partial class FormWon : Form
    {
        private Form1 parentForm;
        private Counter counter;
        private List<Turn> turns;
        public FormWon(Form1 parentForm, Counter counter)
        {
            InitializeComponent();
            this.parentForm = parentForm;
            labelSuccessfulTurns.Text = counter.NumberOfSuccessfulTurns.ToString();
            labelTurns.Text = counter.NumberOfTurns.ToString();
            this.counter = counter;
            turns = counter.Turns;
            for (int i = 0; i < turns.Count; i++)
            {
                textBoxTurns.Text += turns[i].ToString();
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Hide();
            parentForm.QuitGame();
        }

        private void buttonNewGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            parentForm.NewGame();
        }
    }
}
