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
            }
        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
