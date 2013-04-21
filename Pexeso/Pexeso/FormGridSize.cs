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
    public partial class FormGridSize : Form
    {
        private Form1 parentForm;
        public FormGridSize(Form1 form)
        {
            InitializeComponent();
            this.parentForm = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButtonLarge.Checked)
                parentForm.InitGrid(Grid.LARGE, radioButtonAno.Checked);
            else
                parentForm.InitGrid(Grid.SMALL, radioButtonAno.Checked);
            this.Hide();
        }
    }
}
