using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UCQLSV ucqlsv = new UCQLSV();
            ucqlsv.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Controls.Add(ucqlsv);
            quảnLýLớpHọcToolStripMenuItem.Font = new Font(quảnLýLớpHọcToolStripMenuItem.Font, FontStyle.Bold);
        }

        private void quảnLýLớpHọcToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UCQLLH ucqllh = new UCQLLH();
            ucqllh.Dock = DockStyle.Fill;

            panel1.Controls.Clear();
            panel1.Controls.Add(ucqllh);
            quảnLýLớpHọcToolStripMenuItem1.Font = new Font(quảnLýLớpHọcToolStripMenuItem1.Font, FontStyle.Bold);
            quảnLýLớpHọcToolStripMenuItem.Font = new Font(quảnLýLớpHọcToolStripMenuItem.Font, FontStyle.Regular);


        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCQLSV ucqlsv = new UCQLSV();
            ucqlsv.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(ucqlsv);
            quảnLýLớpHọcToolStripMenuItem.Font = new Font(quảnLýLớpHọcToolStripMenuItem.Font, FontStyle.Bold);
            quảnLýLớpHọcToolStripMenuItem1.Font = new Font(quảnLýLớpHọcToolStripMenuItem1.Font, FontStyle.Regular);


        }
    }
}
