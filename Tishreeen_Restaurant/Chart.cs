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
    public partial class Chart : Form
    {
        DataClassDataContext db = new DataClassDataContext();
        public Chart()
        {
            InitializeComponent();
        }
        private void filldata()
        {
            var list = db.Invoices.Where(x => x.Relase_Date.Value > dateTimePicker1.Value && x.Relase_Date.Value < dateTimePicker2.Value).GroupBy(x => x.Relase_Date).Select(x => new { RealeaseDate = x.Key, Count = x.Count() });
            foreach (var item in list)
            {
                chart1.Series["Sales"].Points.AddXY(item.RealeaseDate, item.Count);

            }

            //chart title  
            chart1.Titles.Add("Sales Chart");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            filldata();
        }
    }
}
