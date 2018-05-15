using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Timers;

namespace denemevss
{
    public partial class Rapor : Form
    {
        public Rapor()
        {
            InitializeComponent();
        }

        private void Rapor_Load(object sender, EventArgs e)
        {
            // LOAD

            label1.Text = dateTimePicker1.Value.Month.ToString();
            label2.Text = dateTimePicker1.Value.ToShortDateString();
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True");


        // SECILEN TARIHE GORE YAPILAN SATISLAR (LISTVIEW)
        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string date = dateTimePicker1.Value.ToShortDateString();
            string day = dateTimePicker1.Value.Day.ToString();
            string month = dateTimePicker1.Value.Month.ToString();
            string dayofweek = dateTimePicker1.Value.DayOfWeek.ToString();

            

            string islem = "Select ad,miktar,satis from islemler WHERE date='" + date + "'";

            SqlCommand cmd = new SqlCommand(islem, cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            listView1.Items.Clear();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["ad"].ToString();
                //ekle.SubItems.Add(oku["barkod"].ToString());
                ekle.SubItems.Add(oku["miktar"].ToString());
                ekle.SubItems.Add(oku["satis"].ToString());
                //ekle.SubItems.Add(oku["satistoplam"].ToString());
                listView1.Items.Add(ekle);
            }

            cnn.Close();
        }

        // BUGUN KASAYA GIREN
        private void button2_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string date = DateTime.Now.ToShortDateString();
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string dayofweek = DateTime.Now.DayOfWeek.ToString();

            string islem = "Select miktar,satis from islemler WHERE date='" + date + "'";

            SqlCommand cmd = new SqlCommand(islem, cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            double satistoplam = 0;

            while (oku.Read())
            {
                int miktar = Convert.ToInt32(oku["miktar"]);
                double satis = Convert.ToDouble(oku["satis"]);

                satistoplam = satistoplam + (miktar * satis);
            }

            MessageBox.Show("= " + satistoplam + " ₺", "Bugün Kasaya Giren", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cnn.Close();
        }

        // BU AY KASAYA GIREN
        private void button4_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string date = DateTime.Now.ToShortDateString();
            string day = DateTime.Now.Day.ToString();
            string month = DateTime.Now.Month.ToString();
            string dayofweek = DateTime.Now.DayOfWeek.ToString();

            string islem = "Select miktar,satis from islemler WHERE month='" + month + "'";

            SqlCommand cmd = new SqlCommand(islem, cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            double satistoplam = 0;

            while (oku.Read())
            {
                int miktar = Convert.ToInt32(oku["miktar"]);
                double satis = Convert.ToDouble(oku["satis"]);

                satistoplam = satistoplam + (miktar * satis);
            
            }

            MessageBox.Show("= " + satistoplam + " ₺", "Bugün Kasaya Giren", MessageBoxButtons.OK, MessageBoxIcon.Information);

            cnn.Close();
        }
    }
}
