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
    public partial class Silme : Form
    {
        public Silme()
        {
            InitializeComponent();
        }

        private void Silme_Load(object sender, EventArgs e)
        {
            // LOAD
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cnn.Open();

                string islem = "TRUNCATE Table islemler";

                SqlCommand cmd = new SqlCommand(islem, cnn);

                DialogResult dialogResult = MessageBox.Show("Bütün veritabanı verileri silinecek! Devam etmek istiyor musunuz?", "Uyarı!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.OK)
                {
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Bütün Veritabanı Verileri Silindi!", "Bilgi!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    goto git;
                }
                
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

            git:

            cnn.Close();
        }

    }
}
