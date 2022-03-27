using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing; // windows cizim kutuphanesi
using System.Drawing.Drawing2D; // windows 2d cizim kutuphanesi
using System.Drawing.Imaging; // windows resim kutuphanesi
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gorsel_programlamaya_grs
{
    public partial class Form1 : Form
    {

       
        Point lastLocation = Point.Empty; // Bos Nokta sınıfı olustruluyor yani son kordinat alınması için
        Pen pen = new Pen(Color.Black); // varsayılan olarak kalem rengi siyah olarak ayarladım
        bool isMouseDown = new Boolean(); //  bir bool instance aldım fareye tıklanıp tıklanmadıgını anlamak için

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) // picturebox mousemove event handler olusturdum
        {
            if (isMouseDown == true) // eger fareye  tıklanıyorsa
            {
                if (lastLocation != null)  // eger son kordinat null değilse 
                {
                    if (checkBox1.Checked ==false && checkBox2.Checked == false) // eger 2 checkboxa checklenmediyse
                    {
                        if (pictureBox1.Image == null) // picturebox image properties null ise
                        {
                            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height); // bitmap sınıfı olusturup picturebox yukseklik ve genislik degeri bmp instance edilir
                            pictureBox1.Image = bmp; // picturebox image propertiese bmp aktarılır
                        }
                        using (Graphics g = Graphics.FromImage(pictureBox1.Image)) // graphic classından fromImage prop picturbox baz alınarak olsuturulur
                        {

                            var point1 = new Point(234, 118); // tamamen rasgele bir kordinat
                            var point2 = new Point(293, 228); // tamamen rasgele bir kordinat
                            g.DrawLine(pen, lastLocation, e.Location); // graphic DrawLine prob kalem son kordinat ve mevcut mouse kordinatını eş zamanlı olarak verdim
                        }
                        pictureBox1.Invalidate(); // picturebox yeniden çizilmesini sağlar
                        lastLocation = e.Location; // mouse ile aldıgımız location bilgisini son koordinata aktarılır
                    }
                   
                }
                
            }
           
           
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // picturebox mousdown event handler olusturdum
        {
            lastLocation = e.Location; // mouse ile aldıgımız location bilgisini son koordinata aktarılır
            isMouseDown = true; // fareye tiklandı
            if (checkBox1.Checked == true && checkBox2.Checked !=true) // checkbox check olursa ve check2 check olmasa
            {
                if (pictureBox1.Image == null) // picturebox image properties null ise
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height); // bitmap sınıfı olusturup picturebox yukseklik ve genislik degeri bmp instance edilir
                    pictureBox1.Image = bmp;// picturebox image propertiese bmp aktarılır
                }
                using (Graphics g = Graphics.FromImage(pictureBox1.Image)) // graphic classından fromImage prop picturbox baz alınarak olsuturulur
                {
                    Rectangle rect = new Rectangle(lastLocation, (Size)e.Location); // bir tane dikdortgen instane aldık paramtre olarak mevcut koordinati verdik ve son koordinatı verdim
                    g.DrawRectangle(pen, rect); // dikdortgen cizdirdik
                }
                pictureBox1.Invalidate(); // picturebox yeniden çizilmesini sağlar
                lastLocation = e.Location; // mouse ile aldıgımız location bilgisini son koordinata aktarılır
            }
            if (checkBox2.Checked == true && checkBox1.Checked !=true)
            {
                if (pictureBox1.Image == null) // picturebox image properties null ise
                {
                    Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height); // bitmap sınıfı olusturup picturebox yukseklik ve genislik degeri bmp instance edilir
                    pictureBox1.Image = bmp;// picturebox image propertiese bmp aktarılır
                }
                using (Graphics g = Graphics.FromImage(pictureBox1.Image)) // graphic classından fromImage prop picturbox baz alınarak olsuturulur
                {
                    Rectangle rect = new Rectangle(lastLocation, (Size)e.Location); // bir tane dikdortgen instane aldık paramtre olarak mevcut koordinati verdik ve son koordinatı verdim
                    g.DrawEllipse(pen,rect); // cember cizdirdik
                }
                pictureBox1.Invalidate(); // picturebox yeniden çizilmesini sağlar
                lastLocation = e.Location; // mouse ile aldıgımız location bilgisini son koordinata aktarılır
            }

        }

        private void button1_Click(object sender, EventArgs e)// button click   event handler olusturdum
        {
            if (pictureBox1.Image != null)// picturebox image properties null degil ise

            {

                pictureBox1.Image = null;// picturebox image properties null ise

                pictureBox1.Invalidate(); // picturebox yeniden çizilmesini sağlar

            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e) // picturebox mouseup event handler olusturdum
        {
            
            isMouseDown = false; // mouse tiklanmadı

            lastLocation = Point.Empty; // son kordinatı bosaltık
        }

        private void button2_Click(object sender, EventArgs e) // button click 2 event handler olsuturuldu
        {
           
            if(colorDialog1.ShowDialog() == DialogResult.OK) // colordialog.show onaylandı ise pen colora colordialog color ata
            {
                pen.Color = colorDialog1.Color;
            }

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e) // menustrip open click event handler olsuturuluyor
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog(); // open file dialog instance
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png"; // image uzantılarını filtrelemk için
            if (open.ShowDialog() == DialogResult.OK)// open.showDialgo onaylandı ise pictureboxa yeni seçilan dosyayı bitmap tipinde aktar
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(open.FileName);
               
               
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)  // menustrip save click event handler olsuturuluyor
        {
            SaveFileDialog save = new SaveFileDialog();// save file dialog instance
            save.Filter = "Images|*.png;*.bmp;*.jpg";// image uzantılarını filtrelemk için
            ImageFormat format = ImageFormat.Png; // image png formatında kontrol için
            if (save.ShowDialog() == DialogResult.OK) // save.showDialgo onaylandı ise 
            {
                string ext = Path.GetExtension(save.FileName); // dosyanın uzantısı alınır
                switch (ext) // ext kontrol edilir
                {
                    case ".jpg": // jpg ise formatı picturebox gore ayarla
                        format = ImageFormat.Jpeg;
                        break;
                    case ".bmp":// bmp ise formatı picturebox gore ayarla
                        format = ImageFormat.Bmp;
                        break;
                    case ".png": // png ise formatı picturebox gore ayarla
                        format = ImageFormat.Png;
                        break;
                }
                pictureBox1.Image.Save(save.FileName, format); // picturebox dosyayı kaydet
            }
        }

    }
}
