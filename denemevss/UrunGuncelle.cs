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
    public partial class UrunGuncelle : Form
    {
        public UrunGuncelle()
        {
            InitializeComponent();
        }

        private void UrunGuncelle_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'nOOBSYSDataSet1.urunler' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.urunlerTableAdapter.Fill(this.nOOBSYSDataSet1.urunler);
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True");


        // BARKOD BILGILERI BUTONU
        private void button4_Click(object sender, EventArgs e)
        {
            bilgileriGetirBarkod();
        }

        // BARKOD BILGILERI METOT
        void bilgileriGetirBarkod()
        {
            cnn.Open();

            string barkod = textBox1.Text;

            SqlCommand cmd = new SqlCommand("Select barkod,ad,miktar,alis,satis from urunler WHERE barkod='" + barkod + "'", cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            while (oku.Read())
            {
                textBox2.Text = oku["ad"].ToString();
                numericUpDown1.Text = oku["miktar"].ToString();
                textBox3.Text = oku["alis"].ToString();
                textBox4.Text = oku["satis"].ToString();
            }

            cnn.Close();
        }


        // TEMIZLE BUTONU
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Text = "1";
            textBox3.Text = "";
            textBox4.Text = "";
        }

        // GUNCELLE BUTONU
        private void button1_Click(object sender, EventArgs e)
        {
            cnn.Open();

            string barkod = textBox1.Text;

            try
            {


                //string islem = "UPDATE urunler SET barkod = @barkod";
                string islem = "UPDATE urunler SET ad=@ad, miktar=@miktar, alis=@alis,satis=@satis WHERE barkod='" + barkod + "'";

                SqlCommand cmd = new SqlCommand(islem, cnn);

                cmd.Parameters.AddWithValue("@ad", textBox2.Text);
                cmd.Parameters.AddWithValue("@miktar", numericUpDown1.Text);
                cmd.Parameters.AddWithValue("@alis", Convert.ToDouble(textBox3.Text));
                cmd.Parameters.AddWithValue("@satis", Convert.ToDouble(textBox4.Text));

                cmd.ExecuteNonQuery();

                MessageBox.Show("Güncelleme İşlemi Gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

            textBox1.Text = String.Empty;
            numericUpDown1.Text = "1";
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            
            cnn.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();

                string barkod = textBox1.Text;

                string islem = "DELETE FROM urunler WHERE barkod='" + barkod + "'";

                SqlCommand cmd = new SqlCommand(islem, cnn);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Ürün Kaydı Başarıyla Kaldırıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

            cnn.Close();
            
        }
    }
}
