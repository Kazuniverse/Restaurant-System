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
                        o.Price,
                        o.Description,
                        CategoryID = o.Category.CategoryID,
                        Category = o.Category.Name
                    })
                    .ToList();
                menuItemBindingSource.DataSource = menuItems;
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
