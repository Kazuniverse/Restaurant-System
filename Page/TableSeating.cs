using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_System.Page
{
    public partial class TableSeating : UserControl
    {
        public TableSeating()
        {
            InitializeComponent();
        }

        private void TableSeating_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            AsignForm asignForm = new AsignForm(btn.Text);
            asignForm.StartPosition = FormStartPosition.CenterParent;
            asignForm.ShowDialog();
        }
    }
}
