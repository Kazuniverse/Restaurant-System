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
        public DetailForm(int transID, string btnName)
        {
            InitializeComponent();
            this.transId = transID;
            this.Text = $"Detail Table - {btnName}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(var db = new HovSedhepDatabaseEntities())
            {
                var trans = db.Transactions.FirstOrDefault(p => p.TransactionID == transId);
                if (trans != null) trans.Status = "Cancelled";
                db.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void LoadDetail()
        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                var detail = db.Transactions
                    .Where(t => t.TransactionID == transId)
                    .FirstOrDefault();
                if (detail != null)
                {
                    comboBox1.DataSource = db.Employees.ToList();
                    comboBox1.DisplayMember = "Name";
                    comboBox1.ValueMember = "EmployeeID";
                }

                var order = detail.Orders.FirstOrDefault();
                if (order != null)
                {
                    comboBox1.SelectedValue = order.EmployeeID;
                }

                textBox2.Text = detail.CustomerName;
                numericUpDown1.Value = detail.RestaurantTable.Capacity;
            }
        }

        private void DetailForm_Load(object sender, EventArgs e)
        {
            LoadDetail();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using(var db = new HovSedhepDatabaseEntities())
            {
                var trans = db.Transactions.FirstOrDefault(t => t.TransactionID == transId);
                if (trans != null) trans.Status = "Completed";
                db.SaveChanges();
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
