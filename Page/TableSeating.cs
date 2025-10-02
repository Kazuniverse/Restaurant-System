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
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int tableId = (int)btn.Tag;
            string btnName = btn.Text;

            using (var db = new HovSedhepDatabaseEntities())
            {
                var activeTransaction = db.Transactions
                    .FirstOrDefault(tr => tr.TableID == tableId && tr.Status == "Ongoing");

                if (activeTransaction == null)
                {
                    var formAsign = new AsignForm(tableId, btnName);
                    formAsign.StartPosition = FormStartPosition.CenterScreen;
                    if (formAsign.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
                else
                {
                    var formDetail = new DetailForm(activeTransaction.TransactionID, btnName);
                    formDetail.StartPosition = FormStartPosition.CenterScreen;
                    if (formDetail.ShowDialog() == DialogResult.OK)
                    {
                        LoadData();
                    }
                }
            }
        }

        void LoadData()
        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                var tables = db.RestaurantTables.ToList();

                foreach (var btn in panel1.Controls.OfType<Button>())
                {
                    var table = tables.FirstOrDefault(t => t.Name == btn.Text);
                    if (table == null) continue;

                    bool isOccupied = db.Transactions
                        .Any(tr => tr.TableID == table.TableID && tr.Status == "Ongoing");

                    btn.Tag = table.TableID;
                    btn.BackColor = isOccupied ? Color.Gray : Color.White;
                }
            }
        }
    }
}
