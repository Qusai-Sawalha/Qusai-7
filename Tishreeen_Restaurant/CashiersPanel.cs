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
    public partial class CashiersPanel : Form
    {
        DataClassDataContext db = new DataClassDataContext();
        List<NumericUpDown> numericUps = new List<NumericUpDown>();
        List<Label> labels = new List<Label>();
         int updated_ID;
        static Invoice invoice;
        public CashiersPanel()
        {
            InitializeComponent();
        }

        private void CashiersPanel_Load(object sender, EventArgs e)
        {
            var SubCat = db.Meals.Select(x => x).ToList();
            int key = 0;
            foreach (var pro in SubCat)
            {
                NumericUpDown upDown1 = new NumericUpDown();
                upDown1.Width = 100;
                Label l = new Label();
                l.Text = pro.Name;
                l.AutoSize = true;
                l.Top = 30 + key * 25;
                l.Font = new Font("Calibre", 12);
                upDown1.Left = l.Left + l.Width + 50;
                upDown1.Parent = panel1;
                upDown1.Top = l.Top;
                panel1.Controls.Add(l);
                numericUps.Add(upDown1);
                labels.Add(l);
                key++;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
             invoice = new Invoice();
            db.Invoices.InsertOnSubmit(invoice);
            db.SubmitChanges();
            for (int i = 0; i < numericUps.Count; i++)
            {
                if (numericUps.ElementAt(i).Value > 0)

                {
                    Order order = new Order();
                    order.Invoice_Id = invoice.Id;
                    order.Quantity = (int)numericUps.ElementAt(i).Value;
                    order.Meals_Id = int.Parse(db.Meals.Where(x => x.Name == labels.ElementAt(i).Text).Select(x => x.Id).FirstOrDefault().ToString());
                    db.Orders.InsertOnSubmit(order);
                    db.SubmitChanges();
                }
            }
            
            dataGridView1.DataSource = db.Orders.Where(x => x.Invoice.Id ==invoice.Id).Select(x => new {Id= x.Id, Product = x.Meal.Name, Quantitiy = x.Quantity, TotalPrice = x.Quantity * x.Meal.Price }).ToList();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
           
            if (updated_ID>0)
            {
                db.Orders.DeleteOnSubmit(db.Orders.Where(x => x.Id == updated_ID).Select(x => x).FirstOrDefault());
                db.SubmitChanges();
                dataGridView1.DataSource = db.Orders.Where(x => x.Invoice.Id == invoice.Id).Select(x => new { Id = x.Id, Product = x.Meal.Name, Quantitiy = x.Quantity, TotalPrice = x.Quantity * x.Meal.Price }).ToList();
            }

        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if(updated_ID>0)
            {
                numericUpDown1.Visible = true;
                label3.Visible = button2.Visible = true;
                numericUpDown1.Value = db.Orders.Where(x => x.Id == updated_ID).Select(x => x.Quantity).FirstOrDefault();
                numericUpDown1.Value = db.Orders.Where(x => x.Id == updated_ID).Select(x => x.Quantity).FirstOrDefault();
                label3.Text = db.Orders.Where(x => x.Id == updated_ID).Select(x => x.Meal.Name).FirstOrDefault();
            }
        }

       


        private void button2_Click(object sender, EventArgs e)
        {
            Order order = db.Orders.Where(x => x.Id == updated_ID).Select(x => x).FirstOrDefault();
            order.Quantity = (int)numericUpDown1.Value;
            
            db.SubmitChanges();
            dataGridView1.DataSource = db.Orders.Where(x => x.Invoice.Id == invoice.Id).Select(x => new { Id = x.Id, Product = x.Meal.Name, Quantitiy = x.Quantity, TotalPrice = x.Quantity * x.Meal.Price }).ToList();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            invoice.Relase_Date = DateTime.Now;
            double sum = 0;
            var q= db.Orders.Where(x => x.Invoice.Id == invoice.Id).Select(x => x);
            foreach (var o in q)
            {
                sum += o.Meal.Price * o.Quantity;
            }
            invoice.Fee = sum;
            db.Invoices.InsertOnSubmit(invoice);
            db.SubmitChanges();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            updated_ID = int.Parse(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString());
            menuStrip1.Visible = true;
        }
    }
}
