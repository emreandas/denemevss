using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.SqlClient;

namespace denemevss
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True");


        // ÜRÜN EKLEME BUTONU
        private void button1_Click(object sender, EventArgs e)
        {
            UrunKaydi goster1 = new UrunKaydi();
            goster1.ShowDialog();
        }

        
        private void TarihveSaat()
        {
            //saniye.Text = DateTime.Now.Second.ToString();     // sadece saniye
            //dakika.Text = DateTime.Now.Minute.ToString();     // sadece dakika
            //saat.Text = DateTime.Now.Hour.ToString();         // sadece saat

            //gun.Text = DateTime.Now.Day.ToString();           // sadece gün
            //ay.Text = DateTime.Now.Month.ToString();          // sadece ay
            //yil.Text = DateTime.Now.Year.ToString();          // sadece yıl

            //label1.Text = DateTime.Now.ToString();            // tarih ve saat
            //label2.Text = DateTime.Now.ToLongTimeString();    // sadece saat
            //label1.Text = DateTime.Now.ToLongDateString();    // sadece tarih

            label1.Text = DateTime.Now.ToShortDateString();
        }

        private void AnaEkran_Load(object sender, EventArgs e)
        {
            TarihveSaat();

            veriGetir();
        }

        // ÜRÜN SATIŞ BUTONU
        private void button2_Click(object sender, EventArgs e)
        {
            UrunSatis goster2 = new UrunSatis();
            goster2.ShowDialog();
        }

        // ÜRÜN GUNCELLE BUTONU
        private void button3_Click(object sender, EventArgs e)
        {
            UrunGuncelle goster3 = new UrunGuncelle();
            goster3.ShowDialog();
        }

        // RELOAD BUTONU
        private void button4_Click(object sender, EventArgs e)
        {
            AnaEkran goster = new AnaEkran();
            goster.Show();
            this.Hide();
        }

        // VERI GETIR METODU
        public void veriGetir()
        {
            cnn.Open();

            string date = dateTimePicker1.Value.ToShortDateString();

            SqlCommand cmd = new SqlCommand("Select * from islemler WHERE date='" + date + "'", cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["ad"].ToString();
                ekle.SubItems.Add(oku["barkod"].ToString());

                ekle.SubItems.Add(oku["miktar"].ToString());
                int miktar = Convert.ToInt32(oku["miktar"]);

                ekle.SubItems.Add(oku["satis"].ToString());
                double satis = Convert.ToDouble(oku["satis"]);

                ekle.SubItems.Add((miktar * satis).ToString());

                listView1.Items.Add(ekle);

            }

            cnn.Close();
        }

        // TIME PICKER
        private void button6_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string date = dateTimePicker1.Value.ToShortDateString();
            string day = dateTimePicker1.Value.Day.ToString();
            string month = dateTimePicker1.Value.Month.ToString();
            string dayofweek = dateTimePicker1.Value.DayOfWeek.ToString();

            string islem = "Select barkod,ad,miktar,satis from islemler WHERE date='" + date + "'";

            SqlCommand cmd = new SqlCommand(islem, cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            listView1.Items.Clear();

            while (oku.Read())
            {

                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["ad"].ToString();
                ekle.SubItems.Add(oku["barkod"].ToString());

                ekle.SubItems.Add(oku["miktar"].ToString());
                int miktar = Convert.ToInt32(oku["miktar"]);

                ekle.SubItems.Add(oku["satis"].ToString());
                double satis = Convert.ToDouble(oku["satis"]);

                ekle.SubItems.Add((miktar*satis).ToString());

                listView1.Items.Add(ekle);

            }

            cnn.Close();
        }

        // RAPOR BUTONU
        private void button5_Click(object sender, EventArgs e)
        {
            Rapor goster = new Rapor();
            goster.ShowDialog();
        }

        // STOKTAKI BUTUN URUNLERI GOSTER BUTONU
        private void button7_Click(object sender, EventArgs e)
        {
            Urunler goster = new Urunler();
            goster.ShowDialog();
        }

        // AYARLAR BUTONU
        private void button8_Click(object sender, EventArgs e)
        {
            Silme goster = new Silme();
            goster.ShowDialog();
        }
    }
}
