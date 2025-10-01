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
    public partial class AsignForm : Form
    {
        public AsignForm(string btnTitle)
        {
            InitializeComponent();
            this.Text = "Asign Table - " + btnTitle;
        }

        private void AsignForm_Load(object sender, EventArgs e)
        {
        }
    }
}
