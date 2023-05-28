using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tishreeen_Restaurant
{
    public partial class AdminPanel : Form
    {
        DataClassDataContext db = new DataClassDataContext();
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Controls.Clear();
            panel1.Font = new Font(FontFamily.GenericSerif, 11.7f,FontStyle.Bold);

            NumericUpDown upDown1 = new NumericUpDown();
            upDown1.Width = 100;
            Label l1 = new Label();
            l1.Text = "User Role";
            Label l2 = new Label();
            l2.Text = "User Name";
            Label l3 = new Label();
            l3.Text = "Password";
            panel1.Controls.Add(l1);
            panel1.Controls.Add(l2);
            panel1.Controls.Add(l3);
            l1.Left =l3.Left = l2.Left = 25;
            l1.Top = 30 + 0*40;
            l2.Top = 30 + 1*40;
            l3.Top = 30 + 2*40;
            ComboBox combo = new ComboBox();
            TextBox text1 = new TextBox();
            TextBox text2 = new TextBox();
           
            text1.Left = combo.Left = text2.Left = 165;
            text1.Top = 30 + 1 * 40;
            text2.Top = 30 + 2 * 40;
            combo.Top = 30 + 0 * 40;
            combo.Width = text2.Width = text1.Width = 100;
            combo.DataSource = new List<String>() { "User", "Admin" };
            panel1.Controls.Add(text1);
            panel1.Controls.Add(text2);
          
            panel1.Controls.Add(combo);
            Button button = new Button();
            button.Font= new Font(FontFamily.GenericSerif, 13.7f, FontStyle.Bold);
            panel1.Controls.Add(button);
            button.Text = " Add New Employee";
            button.Top = 30 + 4 * 40;
            button.Width = 200;
            button.Left = l1.Right - 60;
            button.Height = 30;
            button.Click += (s, ev) =>
            {
                User user = new User();
                user.UName = text1.Text;
                user.UPassword = text2.Text;
                user.URole = combo.SelectedItem.ToString() == "User" ? false : true;
                db.Users.InsertOnSubmit(user);
                db.SubmitChanges();
                MessageBox.Show("User " + user.UName + " Has been added");
            };
            
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            employeesToolStripMenuItem_Click(sender, e);
            
          }

        private void mealsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Font = new Font(FontFamily.GenericSerif, 11.7f, FontStyle.Bold);
            panel1.ForeColor = Color.CornflowerBlue;
            NumericUpDown upDown1 = new NumericUpDown();
            upDown1.Width = 100;
            Label l1 = new Label();
            l1.Text = "Meal";
            Label l2 = new Label();
            l2.Text = "Price";

            panel1.Controls.Add(l1);
            panel1.Controls.Add(l2);
          
            l1.Left  = l2.Left = 25;
            l1.Top = 30 + 0 * 40;
            l2.Top = 30 + 1 * 40;
           
         
            TextBox text1 = new TextBox();
            TextBox text2 = new TextBox();

            text1.Left  = text2.Left = 165;
            text1.Top = 30 + 0 * 40;
            text2.Top = 30 + 1 * 40;

            panel1.Controls.Add(text1);
            panel1.Controls.Add(text2);
            text2.Width = text1.Width = 100;
            Button button = new Button();
            button.Text = " Add New Meal";
            button.Top = 30 +2 * 40;
            button.Width = 200;
            button.Left = l1.Right - 60;
            button.Height = 30; 
            panel1.Controls.Add(button);

            panel1.Visible = true;
           
            button.Click += (s, ev) =>
            {
                Meal meal = new Meal();
                meal.Name = text1.Text;
                meal.Price = double.Parse(text2.Text);
               
                db.Meals.InsertOnSubmit(meal);
                db.SubmitChanges();
                MessageBox.Show("Meal " + meal.Name + " Has been added");
            };

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panel1.Font = new Font(FontFamily.GenericSerif, 11.7f, FontStyle.Bold);

            NumericUpDown upDown1 = new NumericUpDown();
            upDown1.Width = 100;
            Label l1 = new Label();
            l1.Text = "Start Date";
            Label l2 = new Label();
            l2.Text = " End Date";
            DataGridView gridView = new DataGridView();
            panel1.Controls.Add(l1);
            panel1.Controls.Add(l2);

            gridView.Left= l1.Left = l2.Left = 25;
            l1.Top = 30 + 0 * 40;
            l2.Top = 30 + 1 * 40;
           

            DateTimePicker date1 = new DateTimePicker();
            DateTimePicker date2 = new DateTimePicker();
           
            date1.Left = date2.Left = 165;
            date1.Top = 30 + 0 * 40;
            date2.Top = 30 + 1 * 40;
            gridView.Top = 30 + 4 * 40;
            gridView.Width = 450;
            panel1.Controls.Add(gridView);
            panel1.Controls.Add(date2);
            panel1.Controls.Add(date1);
            date1.Width = date2.Width = 200;
            Button button = new Button();
            button.Text = " Search";
            button.Top = 30 + 2 * 40;
            button.Width = 200;
            button.Left = l1.Right - 60;
            button.Height = 30;
            panel1.Controls.Add(button);

            panel1.Visible = true;

            button.Click += (s, ev) =>
            {
                gridView.DataSource = db.Invoices.Where(x => x.Relase_Date.Value >= date1.Value && x.Relase_Date.Value <= date2.Value).Select(x => x).ToList();
                
               
            };

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void chartsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chart r = new Chart();
            r.Show();
        }
    }
}
