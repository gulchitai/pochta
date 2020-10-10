using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Kliche
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            pictureBox1.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\1.jpg").Bitmap;
            pictureBox2.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\2.jpg").Bitmap;
            pictureBox3.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\3.jpg").Bitmap;
            pictureBox4.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\4.jpg").Bitmap;
            pictureBox5.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\5.jpg").Bitmap;
            pictureBox6.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\6.jpg").Bitmap;
            pictureBox7.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\7.jpg").Bitmap;
            pictureBox8.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\8.jpg").Bitmap;
            pictureBox9.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\9.jpg").Bitmap;
            pictureBox10.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\0.jpg").Bitmap;

            pictureBox32.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\1.jpg"));
            pictureBox31.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\2.jpg"));
            pictureBox30.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\3.jpg"));
            pictureBox29.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\4.jpg"));
            pictureBox28.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\5.jpg"));
            pictureBox27.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\6.jpg"));
            pictureBox26.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\7.jpg"));
            pictureBox25.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\8.jpg"));
            pictureBox24.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\9.jpg"));
            pictureBox23.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\0.jpg"));

            pictureBox20.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\A.jpg").Bitmap;
            pictureBox19.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\B.jpg").Bitmap;
            pictureBox18.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\C.jpg").Bitmap;
            pictureBox17.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\E.jpg").Bitmap;
            pictureBox16.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\H.jpg").Bitmap;
            pictureBox15.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\K.jpg").Bitmap;
            pictureBox14.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\M.jpg").Bitmap;
            pictureBox13.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\O.jpg").Bitmap;
            pictureBox12.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\P.jpg").Bitmap;
            pictureBox11.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\T.jpg").Bitmap;
            pictureBox21.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\X.jpg").Bitmap;
            pictureBox22.BackgroundImage = new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\Y.jpg").Bitmap;

            pictureBox44.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\A.jpg"));
            pictureBox43.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\B.jpg"));
            pictureBox42.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\C.jpg"));
            pictureBox41.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\E.jpg"));
            pictureBox40.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\H.jpg"));
            pictureBox39.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\K.jpg"));
            pictureBox38.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\M.jpg"));
            pictureBox37.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\O.jpg"));
            pictureBox36.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\P.jpg"));
            pictureBox35.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\T.jpg"));
            pictureBox34.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\X.jpg"));
            pictureBox33.BackgroundImage = Kliche.Graphics.GetBitmap(new Emgu.CV.Image<Bgr, byte>("DigitLetter2\\Y.jpg"));
        }
    }
}
