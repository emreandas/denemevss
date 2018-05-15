using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace denemevss
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="admin" && textBox2.Text=="sistem2018")
            {
                AnaEkran goster = new AnaEkran();
                goster.Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Giriş Başarısız! Eksik veya Hatalı Giriş Yaptınız!", "..::HATA ::..", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}
