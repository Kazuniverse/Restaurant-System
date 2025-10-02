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
    public partial class DetailForm : Form
    {
        private int transId;
        public DetailForm(int transId, string btnName)
        {
            InitializeComponent();
            this.transId = transId;
            this.Text = $"Detail Table - {btnName}";
        }
    }
}
