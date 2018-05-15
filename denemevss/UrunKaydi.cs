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
    public partial class UrunKaydi : Form
    {
        public UrunKaydi()
        {
            InitializeComponent();
        }

        private void UrunKaydi_Load(object sender, EventArgs e)
        {
            //LOAD
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {

            // Yanlış veya boş veriler girilmemesi için belli zorunluluklar.
            // Hata durumunda goto ile işlemleri atlıyoruz.
            if (Convert.ToString(textBox1.Text) == "" || Convert.ToString(textBox2.Text) == "" || Convert.ToString(textBox3.Text) == "" || Convert.ToString(textBox4.Text) == "")
            {
                MessageBox.Show("Lütfen bütün alanları doldurunuz!", "Eksik Bilgi Girdiniz!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto basla;
            }

            try
            {
                if (cnn.State == ConnectionState.Closed)
                    cnn.Open();

                string kayit = "INSERT INTO urunler(barkod,ad,miktar,alis,satis) VALUES (@barkod,@ad,@miktar,@alis,@satis)";

                SqlCommand komut = new SqlCommand(kayit, cnn);

                // Asıl olay burası.

                komut.Parameters.AddWithValue("@barkod", textBox1.Text);
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@miktar", numericUpDown1.Text);
                komut.Parameters.AddWithValue("@alis", Convert.ToDouble(textBox3.Text));
                komut.Parameters.AddWithValue("@satis",Convert.ToDouble(textBox4.Text));

                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt İşlemi Gerçekleşti.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // İşlemler sorunsuz tamamlandıktan sonra formu sıfırlıyoruz.
                
                textBox1.Text = String.Empty;
                textBox2.Text = String.Empty;
                numericUpDown1.Text = "1";
                textBox3.Text = String.Empty;
                textBox4.Text = String.Empty;
            }

            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        // Kural dışı girdiler sonunda gidilecek yer. (İşlemleri atladık.)
        basla:

            cnn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            numericUpDown1.Text = "1";
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
        }
    }
}
