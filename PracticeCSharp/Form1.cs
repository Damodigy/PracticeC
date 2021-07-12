using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeCSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void работаСФайламиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        void timer_Tick(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void проверкаБДToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String connection = "Database=db;Data Sourse=localhost;User Id=root;Password=;";
            MySqlConnection con = new MySqlConnection(connection);
            con.Open();
            MySqlDataAdapter data = new MySqlDataAdapter("Select * from slaves", con);
            DataSet ds_store = new DataSet("db");
            data.Fill(ds_store, "slaves");
        }
           
    }
}
