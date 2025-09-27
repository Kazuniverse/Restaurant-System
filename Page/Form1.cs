using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant_System.Page;

namespace Restaurant_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            LoadPage(new TableSeating());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadPage(new Page.Menu());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadPage(new History());
        }
        public void LoadPage(UserControl halaman)
        {
            panelContainer.Visible = false;

            panelContainer.Controls.Clear();
            Dock = DockStyle.Fill;
            

            panelContainer.Controls.Add(halaman);

            panelContainer.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPage(new TableSeating());
        }
    }
}
