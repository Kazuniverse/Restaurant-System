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
        private int tableId;

        public AsignForm(int tableID, string btnName)
        {
            InitializeComponent();
            this.tableId = tableID;
            this.Text = $"Asign Table - {btnName}";
        }

        private void AsignForm_Load(object sender, EventArgs e)
        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                comboBox1.DataSource = db.Employees.ToList();
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "EmployeeID";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cusNama = textBox2.Text.Trim();
            if (string.IsNullOrEmpty(cusNama))
            {
                MessageBox.Show("Masukkan Nama Customer!");
                return;
            }

            int selectedEmp = (int)(comboBox1.SelectedValue ?? -1);
            using (var db = new HovSedhepDatabaseEntities())
            {
                var trans = new Transaction
                {
                    TableID = tableId,
                    CustomerName = cusNama,
                    Status = "Ongoing",
                    TransactionDate = DateTime.Now,
                };
                db.Transactions.Add(trans);
                db.SaveChanges();
            }
                
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
