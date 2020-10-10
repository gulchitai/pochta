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

namespace PasportRecognition
{
    public partial class MainForm : Form
    {
        Recognize.Pasport pasport = new Recognize.Pasport();

        public MainForm()
        {
            InitializeComponent();
        }
       
        string file = @"image\";
        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 22; i++)
            {
                dataGridView1.Rows.Add();
            }
            dataGridView1.Rows[0].Cells[0].Value = "0";
            dataGridView1.Rows[1].Cells[0].Value = "1";
            dataGridView1.Rows[2].Cells[0].Value = "2";
            dataGridView1.Rows[3].Cells[0].Value = "3";
            dataGridView1.Rows[4].Cells[0].Value = "4";
            dataGridView1.Rows[5].Cells[0].Value = "5";
            dataGridView1.Rows[6].Cells[0].Value = "6";
            dataGridView1.Rows[7].Cells[0].Value = "7";
            dataGridView1.Rows[8].Cells[0].Value = "8";
            dataGridView1.Rows[9].Cells[0].Value = "9";
            dataGridView1.Rows[10].Cells[0].Value = "A";
            dataGridView1.Rows[11].Cells[0].Value = "B";
            dataGridView1.Rows[12].Cells[0].Value = "E";
            dataGridView1.Rows[13].Cells[0].Value = "K";
            dataGridView1.Rows[14].Cells[0].Value = "M";
            dataGridView1.Rows[15].Cells[0].Value = "H";
            dataGridView1.Rows[16].Cells[0].Value = "O";
            dataGridView1.Rows[17].Cells[0].Value = "P";
            dataGridView1.Rows[18].Cells[0].Value = "C";
            dataGridView1.Rows[19].Cells[0].Value = "T";
            dataGridView1.Rows[20].Cells[0].Value = "X";
            dataGridView1.Rows[21].Cells[0].Value = "Y";
            
            string[] files = System.IO.Directory.GetFiles(file, "*.bmp");
            Array.Sort<string>(files);
            for (int i = 0; i < files.Length; i++)
            {
                System.IO.FileInfo info = new System.IO.FileInfo(files[i]);
                listBox1.Items.Add(info.Name);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pasport.Clear();
            Image<Bgr, byte> image = new Image<Bgr, byte>(file + listBox1.SelectedItem.ToString());
            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Start();
            Image<Bgr, byte> image2 = pasport.SetImage(image);
            stop.Stop();
            label2.Text = String.Format("Время обработки: {0}", stop.ElapsedMilliseconds);
            pictureBox1.BackgroundImage = image.Bitmap;
            pictureBox2.BackgroundImage = image2.Bitmap;
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            for (int i = 0; i < pasport.Horizont.Count; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedIndex = 0;

            for (int i = 0; i < pasport.Plate.Count; i++)
            {
                comboBox2.Items.Add(i);
            }
            try
            {
                comboBox2.SelectedIndex = 0;
            }
            catch { }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = pasport.Horizont[comboBox1.SelectedIndex].original.Bitmap;
            pictureBox4.BackgroundImage = pasport.Horizont[comboBox1.SelectedIndex].sobel1.Bitmap;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = pasport.Original.Bitmap;
            pictureBox5.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].original.Bitmap;
            try
            {
                pictureBox6.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].rotate.Bitmap;
            }
            catch { }
            
            label1.Text = pasport.Plate[comboBox2.SelectedIndex].Alpha.ToString();
            try
            {
                pictureBox8.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].rotate1.Bitmap;
            }
            catch { }
            try
            {
                pictureBox9.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].rotate2.Bitmap;
            }
            catch { }

            try
            {
                pictureBox7.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[0].LD.Bitmap;
                
            }
            catch { }
            try
            {
                pictureBox10.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[1].LD.Bitmap;
                
            }
            catch { }
            try
            {
                pictureBox11.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[2].LD.Bitmap;
                
            }
            catch { }
            try
            {
                pictureBox12.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[3].LD.Bitmap;
                
            }
            catch { }
            try
            {
                pictureBox13.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[4].LD.Bitmap;
               
            }
            catch { }
            try
            {
                pictureBox14.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[5].LD.Bitmap;
               
            }
            catch { }
            try
            {
                pictureBox15.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[6].LD.Bitmap;
              
            }
            catch { }
            try
            {
                pictureBox16.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[7].LD.Bitmap;

            }
            catch { }
            try
            {
                pictureBox17.BackgroundImage = pasport.Plate[comboBox2.SelectedIndex].digit[8].LD.Bitmap;

            }
            catch { }
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int count = (pasport.Plate[comboBox2.SelectedIndex].digit.Count >8)?9 : 8 ;
                    for (int j = 0; j < count; j++)
                    {
                        dataGridView1.Rows[i].Cells[j + 1].Value = pasport.Plate[comboBox2.SelectedIndex].digit[j]._list[i];
                    }
                }
            }
            catch { }
            try
            {
                label3.Text = pasport.Plate[comboBox2.SelectedIndex].text ;
            }
            catch { }
        }


    }
}
