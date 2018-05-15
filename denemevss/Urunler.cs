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
    public partial class Urunler : Form
    {
        public Urunler()
        {
            InitializeComponent();
        }

        private void Urunler_Load(object sender, EventArgs e)
        {
            bilgiGetir();
        }

        SqlConnection cnn = new SqlConnection("Data Source=EMREANDAS\\SQLEXPRESS;Initial Catalog=NOOBSYS;Integrated Security=True");

        // BILGI GETIR METODU
        void bilgiGetir()
        {
            cnn.Open();

            SqlCommand cmd = new SqlCommand("Select * from urunler", cnn);

            SqlDataReader oku = cmd.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["ad"].ToString();
                ekle.SubItems.Add(oku["barkod"].ToString());
                ekle.SubItems.Add(oku["miktar"].ToString());
                ekle.SubItems.Add(oku["alis"].ToString());
                ekle.SubItems.Add(oku["satis"].ToString());

                listView1.Items.Add(ekle);
            }

        }
    }
}
