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
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();
        }

        void LoadData()
        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                var transactions = db.Transactions
                    .Select(t => new
                    {
                        t.TransactionID,
                        RestaurantTable = t.RestaurantTable.Name,
                        t.TransactionDate,
                        t.CustomerName,
                        t.Status,
                        Orders = "Rp. " + (t.Orders
                            .SelectMany(o => o.OrderDetails)
                            .Sum(od => (decimal?)od.Quantity * od.MenuItem.Price) ?? 0)
                    })
                    .ToList();
                dataGridView1.DataSource = transactions;

                var dropTrans = db.RestaurantTables
                    .Select(t => new {
                        t.TableID,
                        t.Name
                    })
                    .ToList();
                comboBox1.DataSource = dropTrans;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "TableID";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadOrder();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Long;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";
            comboBox1.SelectedIndex = -1;
            LoadData();
        }

        void LoadOrder()
        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                int transactionID = (int)dataGridView1.CurrentRow.Cells["transactionIDDataGridViewTextBoxColumn"].Value;

                var orders = db.Orders
                    .Where(o => o.TransactionID == transactionID)
                    .Select(od => new
                    {
                        od.OrderID,
                        od.TransactionID,
                        od.OrderTime,
                        Employee = od.Employee.Name,
                        OrderDetails = od.OrderDetails.Count
                    })
                    .ToList();
                dataGridView2.DataSource = orders;
            }
        }

        void LoadDetail()
        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                int orderID = (int)dataGridView2.CurrentRow.Cells["orderIDDataGridViewTextBoxColumn"].Value;
                var orderDetails = db.OrderDetails
                    .Where(od => od.OrderID == orderID)
                    .Select(od => new
                    {
                        od.OrderDetailID,
                        od.OrderID,
                        MenuItem = od.MenuItem.Name,
                        Price = "Rp. " + od.MenuItem.Price,
                        od.Quantity,
                    })
                    .ToList();
                dataGridView3.DataSource = orderDetails;
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadDetail();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value.Date;
            int selectedStatus = (int)(comboBox1.SelectedValue ?? -1);

            using (var db = new HovSedhepDatabaseEntities())
            {
                var fiter = db.Transactions
                    .Where(t => (dateTimePicker1.Format != DateTimePickerFormat.Custom ? t.TransactionDate == selectedDate : true) &&
                                (selectedStatus == -1 || t.RestaurantTable.TableID == selectedStatus))
                    .Select(t => new
                    {
                        t.TransactionID,
                        RestaurantTable = t.RestaurantTable.Name,
                        t.TransactionDate,
                        t.CustomerName,
                        t.Status,
                        Orders = "Rp. " + (t.Orders
                            .SelectMany(o => o.OrderDetails)
                            .Sum(od => (decimal?)od.Quantity * od.MenuItem.Price) ?? 0)
                    })
                    .ToList();
                dataGridView1.DataSource = fiter;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == "Status" && e.Value != null)
            {
                string latest = e.Value.ToString();
                if (latest == "Ongoing") dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                else if (latest == "Completed") dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                else if (latest == "Cancelled") dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
            }
        }
    }
}
