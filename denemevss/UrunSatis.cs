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

namespace denemevss
{
    public partial class UrunSatis : Form
    {
        public UrunSatis()
        {
            InitializeComponent();
        }

        private void UrunSatis_Load(object sender, EventArgs e)
        {
            // LOAD
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True; MultipleActiveResultSets = True");

        // TEMIZLE BUTONU
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            label7.Text = String.Empty;
            label8.Text = String.Empty;
            label9.Text = String.Empty;
            label4.Text = String.Empty;
            numericUpDown1.Text = "1";
        }

        // STOKTAN DÜŞ BUTONU
        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string barkod = textBox1.Text;

            int belirlenen = Convert.ToInt32(numericUpDown1.Value);
            
            string islem1 = "Select barkod,ad,miktar,satis from urunler WHERE barkod='" + barkod + "'";

            

            SqlCommand cmd1 = new SqlCommand(islem1 , cnn);

            SqlDataReader oku1 = cmd1.ExecuteReader();

            while (oku1.Read())
            {
                label7.Text = oku1["ad"].ToString();
                label8.Text = oku1["miktar"].ToString();
                label9.Text = oku1["satis"].ToString();

                if (belirlenen > Convert.ToInt32(oku1["miktar"]))
                {
                    MessageBox.Show("Yetersiz Stok Sayısı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    goto basla;
                }
                
            }

            try
            {
                string islem2 = "UPDATE urunler SET miktar -= @miktar WHERE barkod='" + barkod + "'";

                SqlCommand cmd2 = new SqlCommand(islem2, cnn);

                cmd2.Parameters.AddWithValue("@miktar", Convert.ToString(belirlenen));

                cmd2.ExecuteNonQuery();

                MessageBox.Show("Stoktan Düşüm İşlemi Gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                
            }

            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

            // ISLEMLER BASLANGIC
           
            string kayit = "INSERT INTO islemler(day,month,dayofweek,date,barkod,ad,miktar,satis) VALUES (@day,@month,@dayofweek,@date,@barkod,@ad,@miktar,@satis)";

            SqlCommand komut = new SqlCommand(kayit, cnn);

            // Asıl olay burası.
            // ektra tarih eklemesi hemen altta
            komut.Parameters.AddWithValue("@day", DateTime.Now.Day.ToString());
            komut.Parameters.AddWithValue("@month", DateTime.Now.Month.ToString());
            komut.Parameters.AddWithValue("@dayofweek", DateTime.Now.DayOfWeek.ToString());
            komut.Parameters.AddWithValue("@date", DateTime.Now.ToShortDateString());

            komut.Parameters.AddWithValue("@barkod", textBox1.Text);
            komut.Parameters.AddWithValue("@ad", label7.Text);
            komut.Parameters.AddWithValue("@miktar", Convert.ToInt32(numericUpDown1.Value));
            komut.Parameters.AddWithValue("@satis", Convert.ToDouble(label9.Text));

            komut.ExecuteNonQuery();

            // ISLEMLER SONU

            textBox1.Text = String.Empty;
            label7.Text = String.Empty;
            label8.Text = String.Empty;
            label9.Text = String.Empty;
            label4.Text = String.Empty;
            numericUpDown1.Text = "1";

        basla:

            cnn.Close();
            
        }

        // OKUNAN BARKODUN BILGILERINI GETIREN BUTON
        private void button4_Click(object sender, EventArgs e)
        {
            bilgileriGetir();
        }

        // BILGILERI GETIR METODU
        void bilgileriGetir()
        {

            cnn.Open();

            string barkod = textBox1.Text;

            SqlCommand cmd3 = new SqlCommand("Select barkod,ad,miktar,satis from urunler WHERE barkod='" + barkod + "'", cnn);

            SqlDataReader oku2 = cmd3.ExecuteReader();

            while (oku2.Read())
            {
                label7.Text = oku2["ad"].ToString();
                label8.Text = oku2["miktar"].ToString();
                label9.Text = oku2["satis"].ToString();
            }

            cnn.Close();
        }

        // ALINACAK MIKTAR BELIRLENDIKTEN SONRA SATIS BEDELI GOSTERECEK BUTON
        private void button3_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string barkod = textBox1.Text;
            int adet = Convert.ToInt32(numericUpDown1.Text);

            SqlCommand cmd4 = new SqlCommand("Select barkod,ad,miktar,satis from urunler WHERE barkod='" + barkod + "'", cnn);

            SqlDataReader oku3 = cmd4.ExecuteReader();

            while (oku3.Read())
            {
                //int miktar = Convert.ToInt32(oku["miktar"]); EGER SATILACAK MIKTAR STOKTA YOKSA HATA VER
                double birim = Convert.ToDouble(oku3["satis"]);

                double sonuc = (adet * birim);
                
                label4.Text = sonuc.ToString();
                
            }

            cnn.Close();
        }

        //void islemlereKayit()
        //{
        //    try
        //    {
        //        if (cnn.State == ConnectionState.Closed)
        //            cnn.Open();

        //        // tarih eklemesi
        //        string kayit = "INSERT INTO urunler(day,month,dayofweek,date,barkod,ad,miktar,alis,satis) VALUES (@day,@month,@dayofweek,@date,@barkod,@ad,@miktar,@alis,@satis)";

        //        SqlCommand komut = new SqlCommand(kayit, cnn);

        //        // Asıl olay burası.
        //        // ektra tarih eklemesi hemen altta
        //        komut.Parameters.AddWithValue("@gun", DateTime.Now.Day.ToString());
        //        komut.Parameters.AddWithValue("@ay", DateTime.Now.Month.ToString());
        //        komut.Parameters.AddWithValue("@haftaismi", DateTime.Now.DayOfWeek.ToString());
        //        komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToShortDateString());

        //        komut.Parameters.AddWithValue("@barkod", textBox1.Text);
        //        komut.Parameters.AddWithValue("@ad", label2.Text);
        //        komut.Parameters.AddWithValue("@miktar", numericUpDown1.Text);
        //        komut.Parameters.AddWithValue("@alis", Convert.ToDouble(textBox3.Text));
        //        komut.Parameters.AddWithValue("@satis", Convert.ToDouble(textBox4.Text));

        //        komut.ExecuteNonQuery();

        //        MessageBox.Show("Kayıt İşlemi Gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //        // İşlemler sorunsuz tamamlandıktan sonra formu sıfırlıyoruz.

        //        textBox1.Text = String.Empty;
        //        textBox2.Text = String.Empty;
        //        numericUpDown1.Text = "1";
        //        textBox3.Text = String.Empty;
        //        textBox4.Text = String.Empty;
        //    }

        //    catch (Exception hata)
        //    {
        //        MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
        //    }

        //    // Kural dışı girdiler sonunda gidilecek yer. (İşlemleri atladık.)
        //    basla:

        //    cnn.Close();
        //}
    }
}
