using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bugs
{
    public partial class Form1 : Form, IFind
    {
        private int numOfThreads = 1;
        private FormThreads formThreads;
        public Form1()
        {
            InitializeComponent();
            textBoxOutput.Text += "Welcome to Bug Finder Extreme Edition";
            textBoxOutput.Text += Environment.NewLine;
            textBoxOutput.Text += "By Tomáš Zahálka (2013)";
            textBoxOutput.Text += Environment.NewLine + Environment.NewLine;
            formThreads = new FormThreads(this);
            formThreads.ShowDialog();
        }

        private Thread thread;
        private void LoadWriteFileAsync()
        {
            textBoxOutput.Text += "Removing bugs started at " + DateTime.Now + " ...";
            textBoxOutput.Text += Environment.NewLine;
            thread = new Thread(new ThreadStart(this.LoadWriteFile));
            thread.Start();
        }

        private void LoadWriteFile()
        {
            FileParser.LoadWriteFile(@"bugy.txt", @"vystup.txt", numOfThreads, this);
        }

        void IFind.StartFinding(int numOfThreads)
        {
            this.numOfThreads = numOfThreads;
            LoadWriteFileAsync();
        }

        int loops = 0;
        void IFind.NextLoop()
        {
            /*this.Invoke((MethodInvoker)delegate
            {
                ++loops;
                textBoxOutput.Text += "Loop number " + loops;
                textBoxOutput.Text += Environment.NewLine;
            });*/
        }

        void IFind.FindingFinished()
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBoxOutput.Text += "Removing bugs finished at " + DateTime.Now;
                textBoxOutput.Text += Environment.NewLine;
                textBoxOutput.Text += "Saving file ...";
                textBoxOutput.Text += Environment.NewLine;
            });
        }

        void IFind.FileSavedAs(string filename)
        {
            this.Invoke((MethodInvoker)delegate
            {
                textBoxOutput.Text += "File saved as " + filename;
            });
        }
    }
}
