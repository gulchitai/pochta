using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Kliche
{
    public class Graphics
    {
        public static Bitmap GetBitmap(Emgu.CV.Image<Bgr, byte> image)
        {
            Emgu.CV.Image<Gray, byte> gray = image.Convert<Gray, byte>();
            int [] k = new int[gray.Width];
            for(int i=0; i<gray.Width; i++)
                for (int j = 0; j < gray.Height; j++)
                {
                    if (gray[j, i].Intensity <= 200)
                        k[i]++;
                }

            Bitmap image2 = new Bitmap(gray.Width, gray.Height);
            
            System.Drawing.Graphics p = System.Drawing.Graphics.FromImage(image2);
            p.Clear(Color.Black);
            for(int i=0; i<k.Length-3; i++){
                PointF p1 = new PointF(i, k[i]);
                PointF p2 = new PointF(i+1, k[i+1]);
                PointF p3 = new PointF(i+2, k[i+2]);
                PointF p4 = new PointF(i+3, k[i+3]);
                p.DrawBezier(new Pen(Brushes.White), p1, p2, p3, p4);
            }

            return image2;
        }
    }
}
