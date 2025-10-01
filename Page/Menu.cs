using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace Restaurant_System.Page
{
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        void LoadData()

        {
            using (var db = new HovSedhepDatabaseEntities())
            {
                var menuItems = db.MenuItems
                    .Select(o => new
                    {
                        o.MenuItemID,
                        o.Name,
                        Price = "Rp. " + o.Price,
                        o.Description,
                        o.Category.CategoryID,
                        Category = o.Category.Name
                    })
                    .ToList();
                menuItemBindingSource.DataSource = menuItems;

                var categories = db.Categories
                    .Select(c => new
                    {
                        c.CategoryID,
                        c.Name
                    })
                    .ToList();
                comboBox1.DataSource = categories;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "CategoryID";
                comboBox1.SelectedIndex = -1;
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        void ApplyFilter()
        {
            int SelectedCategoryID = (int)(comboBox1.SelectedValue ?? -1);
            string searchText = textBox2.Text.ToLower();

            using (var db = new HovSedhepDatabaseEntities())
            { 
                var filteredMenuItems = db.MenuItems
                    .Where(o => (SelectedCategoryID == -1 || o.Category.CategoryID == SelectedCategoryID) &&
                                (string.IsNullOrEmpty(searchText) || o.Name.ToLower().Contains(searchText)))
                    .Select(o => new
                    {
                        o.MenuItemID,
                        o.Name,
                        Price = "Rp. " + o.Price,
                        o.Description,
                        o.Category.CategoryID,
                        Category = o.Category.Name
                    })
                    .ToList();
                menuItemBindingSource.DataSource = filteredMenuItems;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox2.Clear();
            ApplyFilter();
        }
    }
}
