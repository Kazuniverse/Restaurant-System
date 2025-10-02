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
            if (tableId <= 4) numericUpDown1.Maximum = 2;
            else if (tableId <= 6) numericUpDown1.Maximum = 4;
            else numericUpDown1.Maximum = 6;

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

            int pax = (int)numericUpDown1.Value;

            int adjustTable;
            if (pax <= 2) adjustTable = 2;
            else if (pax <= 4) adjustTable = 4;
            else adjustTable = 6;

            int selectedEmp = (int)(comboBox1.SelectedValue ?? -1);

            using (var db = new HovSedhepDatabaseEntities())
            {
                var table = db.RestaurantTables.FirstOrDefault(t => t.TableID == tableId);
                if (table != null) table.Capacity = adjustTable;

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
