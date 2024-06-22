using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IlhanAydinOdev
{
    public partial class Form1 : Form
    {
        Matematik mat =new Matematik();
        public int tempSayi;
        public string tempStr;
        public string tempBir,tempIki;
        public Form1()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtGiris.Text = tempSayi.ToString();
            txtGiris.Text = tempStr;
            if (!txtAralikBir.Text.Equals("") || !txtAralikIki.Text.Equals(""))
            {
                txtAralikBir.Text = tempBir;
                txtAralikIki.Text = tempIki;
            }

        }

        private void btnMutlak_Click(object sender, EventArgs e)
        {
            tempSayi = Convert.ToInt32(txtGiris.Text);
            tempStr = txtGiris.Text;
            txtGiris.Text = Convert.ToString(mat.mutlakdeger(tempSayi)) ;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            tempStr = txtGiris.Text;
            string[] girisDizisi = txtGiris.Text.Split(',');
            txtGiris.Text = Convert.ToString(mat.MaxHesapla(girisDizisi));
        }

        private void btnKarakok_Click(object sender, EventArgs e)
        {
            tempStr = txtGiris.Text;    
            tempSayi = Convert.ToInt32(txtGiris.Text);
            txtGiris.Text = Convert.ToString(mat.KarekokAl(tempSayi)); 
            

        }

        private void btnSin_Click(object sender, EventArgs e)
        {
            double deger = double.Parse(txtGiris.Text);
            tempStr = txtGiris.Text;    
            // Hesaplanan sinüs değerini TextBox'a yaz
            txtGiris.Text = Convert.ToString(mat.sinHesapla(deger));
        }
        
        private void btnCos_Click(object sender, EventArgs e)
        {
            tempStr = txtGiris.Text;
            double deger = double.Parse(txtGiris.Text);

            txtGiris.Text = Convert.ToString(mat.cosHesapla(deger)) ; // 14 basamağa yuvarla

        }

        private void button7_Click(object sender, EventArgs e)
        {
            tempBir = txtAralikBir.Text;
            tempIki = txtAralikIki.Text;
            // Kullanıcı girişlerini doğrulama
            if (!double.TryParse(txtAralikBir.Text, out double baslangicDerece) ||
                !double.TryParse(txtAralikIki.Text, out double bitisDerece))
            {
                MessageBox.Show("Geçerli başlangıç ve bitiş değerleri girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Başlangıç ve bitiş değerlerini radyana dönüştür
            double baslangicRadyan = baslangicDerece * (Math.PI / 180.0);
            double bitisRadyan = bitisDerece * (Math.PI / 180.0);

            // CheckBox kontrolü
            bool sin = checkBoxSin.Checked;
            bool cos = checkBoxCos.Checked;

            if (sin && cos || !sin && !cos)
            {
                MessageBox.Show("Lütfen sadece birini seçin", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Yeni form ve PictureBox oluştur
            Form cizimFormu = new Form();
            cizimFormu.Width = 800;
            cizimFormu.Height = 600;
            PictureBox pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            cizimFormu.Controls.Add(pictureBox);

            // Bitmap ve Graphics nesneleri oluştur
            Bitmap bmp = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White); // Arka planı beyaz yap

            // Eksenleri çiz
            Pen axisPen = new Pen(Color.DarkGray, 2);
            g.DrawLine(axisPen, 0, pictureBox.Height / 2, pictureBox.Width, pictureBox.Height / 2);
            g.DrawLine(axisPen, pictureBox.Width / 2, 0, pictureBox.Width / 2, pictureBox.Height);

            // Grafiği çiz
            Pen graphPen = new Pen(Color.Blue, 2);
            Point? oncekiNokta = null;
            double yScale = pictureBox.Height / 2.5; // Y ölçeklendirme faktörünü artırıyoruz
            for (double x = baslangicRadyan; x <= bitisRadyan; x += (bitisRadyan - baslangicRadyan) / 1000) // Daha fazla nokta ile daha pürüzsüz bir çizim
            {
                double y = (sin ? Math.Sin(x) : Math.Cos(x)) * yScale + (pictureBox.Height / 2);
                Point suankiNokta = new Point((int)((x - baslangicRadyan) / (bitisRadyan - baslangicRadyan) * pictureBox.Width), (int)y);

                if (oncekiNokta != null)
                {
                    g.DrawLine(graphPen, oncekiNokta.Value, suankiNokta);
                }
                oncekiNokta = suankiNokta;
            }

            // Bitmap'i PictureBox'a ata ve formu göster
            pictureBox.Image = bmp;
            cizimFormu.ShowDialog();
        }
        }
}
