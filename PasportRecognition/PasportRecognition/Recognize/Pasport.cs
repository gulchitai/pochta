using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PasportRecognition.Recognize
{
    public class Pasport
    {
        public Image<Bgr, byte> Original;
        Image<Bgr, byte> Sobel;
        Image<Gray, float> SobelGray;
        public List<Horizontal> Horizont = new List<Horizontal>();
        public List<Plate> Plate = new List<Plate>();
        Image<Gray, byte>[] images = new Image<Gray, byte>[22];
        float[] u;

        List<double[]> Images = new List<double[]>();

        public void Clear()
        {
            Horizont.Clear();
            Plate.Clear();
        }

        public Pasport()
        {
            images[0] = new Image<Gray, byte>("DigitLetter2\\0.jpg");
            images[1] = new Image<Gray, byte>("DigitLetter2\\1.jpg");
            images[2] = new Image<Gray, byte>("DigitLetter2\\2.jpg");
            images[3] = new Image<Gray, byte>("DigitLetter2\\3.jpg");
            images[4] = new Image<Gray, byte>("DigitLetter2\\4.jpg");
            images[5] = new Image<Gray, byte>("DigitLetter2\\5.jpg");
            images[6] = new Image<Gray, byte>("DigitLetter2\\6.jpg");
            images[7] = new Image<Gray, byte>("DigitLetter2\\7.jpg");
            images[8] = new Image<Gray, byte>("DigitLetter2\\8.jpg");
            images[9] = new Image<Gray, byte>("DigitLetter2\\9.jpg");
            images[10] = new Image<Gray, byte>("DigitLetter2\\A.jpg");
            images[11] = new Image<Gray, byte>("DigitLetter2\\B.jpg");
            images[12] = new Image<Gray, byte>("DigitLetter2\\E.jpg");
            images[13] = new Image<Gray, byte>("DigitLetter2\\K.jpg");
            images[14] = new Image<Gray, byte>("DigitLetter2\\M.jpg");
            images[15] = new Image<Gray, byte>("DigitLetter2\\H.jpg");
            images[16] = new Image<Gray, byte>("DigitLetter2\\O.jpg");
            images[17] = new Image<Gray, byte>("DigitLetter2\\P.jpg");
            images[18] = new Image<Gray, byte>("DigitLetter2\\C.jpg");
            images[19] = new Image<Gray, byte>("DigitLetter2\\T.jpg");
            images[20] = new Image<Gray, byte>("DigitLetter2\\X.jpg");
            images[21] = new Image<Gray, byte>("DigitLetter2\\Y.jpg");
        }

        

        public Image<Bgr, byte> SetImage(Image<Bgr, byte> image)
        {

            Original = image;
            SobelGray = image.Convert<Gray, float>();
            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Start();
            Image<Gray, float> sobelx = SobelGray.Sobel(1, 0, 3);
            Image<Gray, float> sobely = SobelGray.Sobel(0, 1, 3);
            SobelGray = (sobelx.Pow(2) + sobely.Pow(2)).Pow(0.5);
            Sobel = SobelGray.Convert<Bgr, byte>();
            Horizontal();
            stop.Stop();
            return Sobel;
        }

        private float[] Horiz(Image<Gray, float> image)
        {
            float[] u = new float[image.Height];
            for (int i = 0; i < image.Height; i++)
            {
                float k = 0;
                for (int j = 1; j < image.Width - 1; j++)
                {
                    k += System.Convert.ToSingle(Math.Abs(image.Data[i, j - 1,0] - image.Data[i, j,0]));
                }
                u[i] = k;
            }
            return u;
        }

        public float[] Proizvodnij1(float [] u)
        {
            float[] u1 = new float[u.Length];
            u1[0] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6]) / 7;
            u1[1] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7])/8;
            u1[2] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7]+u[8]) / 9;
            u1[3] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7] + u[8] + u[9]) / 10;
            u1[4] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7] + u[8] + u[9]+u[10]) / 11;
            u1[5] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7] + u[8] + u[9] + u[10] + u[11]) / 12;
            u1[6] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7] + u[8] + u[9] + u[10] + u[11]+u[12]) / 13;
            for (int i = 7; i < u.Length - 7; i++)
            {
                u1[i] = (u[i - 6] + u[i - 5] + u[i - 4] + u[i - 3] + u[i - 2] + u[i - 1] + u[i] + u[i + 1] +
                    u[i + 2] + u[i + 3] + u[i + 4] + u[i + 5] + u[i + 6]) / 13;
            }
            u1[u.Length - 1] = (u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 7;
            u1[u.Length - 2] = (u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 8;
            u1[u.Length - 3] = (u[u.Length - 9] + u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 9;
            u1[u.Length - 4] = (u[u.Length-10] + u[u.Length - 9] + u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 10;
            u1[u.Length - 5] = (u[u.Length - 10] + u[u.Length - 9] + u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 11;
            u1[u.Length - 6] = (u[u.Length - 11] + u[u.Length - 10] + u[u.Length - 9] + u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 12;

            return u1;
        }

        public float[] Proizvodnij2(float[] u)
        {
            float[] u1 = new float[u.Length];
            u1[0] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6]) / 7;
            u1[1] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7]) / 8;
            u1[2] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7] + u[8]) / 9;
            u1[3] = (u[0] + u[1] + u[2] + u[3] + u[4] + u[5] + u[6] + u[7] + u[8] + u[9]) / 10;
            for (int i = 4; i < u.Length - 4; i++)
            {
                u1[i] = (u[i - 3] + u[i - 2] + u[i - 1] + u[i] + u[i + 1] +
                    u[i + 2] + u[i + 3]) / 7;
            }
            u1[u.Length - 1] = (u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 7;
            u1[u.Length - 2] = (u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 8;
            u1[u.Length - 3] = (u[u.Length - 9] + u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 9;
            u1[u.Length - 4] = (u[u.Length - 10] + u[u.Length - 9] + u[u.Length - 8] + u[u.Length - 7] + u[u.Length - 6] + u[u.Length - 5] + u[u.Length - 4] + u[u.Length - 3] + u[u.Length - 2] + u[u.Length - 1]) / 10;
            return u1;
        }

        public float MAX(float[] u)
        {
            float max = u[0];
            for (int i = 1; i < u.Length; i++)
            {
                if (max < u[i])
                    max = u[i];
            }
            return max;
        }

        public int MAX(int[] u)
        {
            int max = u[0];
            for (int i = 1; i < u.Length; i++)
            {
                if (max < u[i])
                    max = u[i];
            }
            return max;
        }

        public void DrawLine(float[] p, float MAX, Image<Bgr, byte> image)
        {
            for (int i = 0, x = 0; i < p.Length - 1; i++, x++)
            {
                float j = image.Width * p[i] / MAX;
                float j1 = image.Width * p[i + 1] / MAX;
                image.Draw(new LineSegment2DF(new System.Drawing.PointF(j, x), new System.Drawing.PointF(j1, x)), new Bgr(System.Drawing.Color.Red), 2);
            }
        }

        public float DrawMiddleLineHoriz(float[] p, float MAX, Image<Bgr, byte> image)
        {
            float sum = 0.0f;
            for (int i = 0; i < p.Length; i++)
            {
                sum += p[i];
            }
            sum /= p.Length;
            float h = Sobel.Width * sum / MAX;
            image.Draw(new LineSegment2DF(new System.Drawing.PointF(h, 0), new System.Drawing.PointF(h, Sobel.Height)), new Bgr(System.Drawing.Color.Aqua), 2);
            return sum;
        }

        public float DrawMiddleLineVertical(int index, float Max)
        {
            float sum1 = 0.0f;
            for (int j = 0; j < Horizont[index].p.Length; j++)
            {
                sum1 += Horizont[index].p[j];
            } 
            sum1 /= Horizont[index].p.Length;
            for (int h1 = 0, x = 0; h1 < Horizont[index].p.Length - 1; h1++, x++)
            {
                float j = Horizont[index].sobel.Height * Horizont[index].p[h1] / (Max);
                float j1 = Horizont[index].sobel.Height * Horizont[index].p[h1 + 1] / (Max);
                Horizont[index].sobel1.Draw(new LineSegment2DF(new System.Drawing.PointF(x, j), new System.Drawing.PointF(x, j1)), new Bgr(System.Drawing.Color.Red), 2);
            }
            return  sum1;
        }

        public List<int> SearchMaximums(float [] p, float Sum, float Max, Image<Bgr, byte> image, bool Vertical)
        {
            List<int> maxList = new List<int>();
            bool down = false, up = false;
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (p[i] > p[i + 1])
                {
                    down = true;
                    if (up)
                    {
                        if (p[i] > Sum)
                            maxList.Add(i);
                        up = false;
                    }
                }
                if (p[i] < p[i + 1])
                {
                    up = true;
                    if (down)
                    {
                        down = false;
                    }
                }
            }
            if (!Vertical)
            {
                for (int i = 0; i < maxList.Count; i++)
                    image.Draw(new CircleF(new System.Drawing.PointF(Convert.ToSingle(image.Width * p[maxList[i]] / Max), maxList[i]), 3), new Bgr(System.Drawing.Color.Yellow), 6);
            }
            else
            {
                for (int i = 0; i < maxList.Count; i++)
                {
                    image.Draw(new CircleF(new System.Drawing.PointF(Convert.ToSingle(image.Height * p[maxList[i]] / Max), maxList[i]), 3), new Bgr(System.Drawing.Color.Yellow), 6);
                }
            }
           
            return maxList;
        }

        public List<int> SearchMininum(float[] p, int index, float Max)
        {
            List<int> maxList = new List<int>();
            bool down = false, up = false;
            maxList.Add(0);
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (p[i] > p[i + 1])
                {
                    down = true;
                    if (up)
                    {

                        up = false;
                    }
                }
                if (p[i] < p[i + 1])
                {
                    up = true;
                    if (down)
                    {
                        maxList.Add(i);
                        down = false;
                    }
                }
            }
            maxList.Add(p.Length - 1);
          
                for (int i = 0; i < maxList.Count; i++)
                {
                    Plate[index].rotate2.Draw(new CircleF(new System.Drawing.PointF(maxList[i], Convert.ToSingle(Plate[index].rotate2.Height * p[maxList[i]] / Max)), 1), new Bgr(System.Drawing.Color.Yellow), 2);
                }
            

            return maxList;
        }

        public List<int> SearchMaximum2(float[] p, int index, float Max)
        {
            List<int> maxList = new List<int>();
            bool down = false, up = false;
            
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (p[i] > p[i + 1])
                {
                    down = true;
                    if (up)
                    {
                        maxList.Add(i);
                        up = false;
                    }
                }
                if (p[i] < p[i + 1])
                {
                    up = true;
                    if (down)
                    {
                        
                        down = false;
                    }
                }
            }
            
            for (int i = 0; i < maxList.Count; i++)
            {
                Plate[index].rotate2.Draw(new CircleF(new System.Drawing.PointF(maxList[i], Convert.ToSingle(Plate[index].rotate2.Height * p[maxList[i]] / Max)), 1), new Bgr(System.Drawing.Color.Yellow), 2);
            }


            return maxList;
        }

        public void Greenze(List<int> maximum, float[] p)
        {
            for (int i = 0; i < maximum.Count; i++)
            {
                Horizontal r = new Recognize.Horizontal();
                r.i = maximum[i];
                bool endUp = false;
                for (int j = maximum[i] + 1; j < p.Length - 1; j++)
                {
                    if (p[j] < p[j + 1])
                    {
                        endUp = true;
                        r.k2 = j;
                        break;
                    }
                }
                if (!endUp)
                {
                    r.k2 = p.Length - 1;
                }

                bool endDown = false;
                for (int j = maximum[i] - 1; j > 0; j--)
                {
                    if (p[j] < p[j - 1])
                    {
                        endDown = true;
                        r.k1 = j;
                        break;
                    }
                }
                if (!endDown)
                {
                    r.k1 = 1;
                }
                if (Math.Abs(r.k1 - r.k2) > 30)
                    Horizont.Add(r);
            }
        }

        public void Vertical(int index)
        {
             Horizont[index].p = new float[Horizont[index].sobel.Width];
             for (int k = 0; k < Horizont[index].sobel.Width; k++)
             {
                 float f = 0.0f;
                 for (int l = 1; l < Horizont[index].sobel.Height - 1; l++)
                 {
                     f += Convert.ToSingle(Math.Abs(Horizont[index].sobel.Data[l - 1, k,0] - Horizont[index].sobel.Data[l, k,0]));
                 }
                 Horizont[index].p[k] = f;
             }
        }

        public void GetPlate(int index, float sum1, float max)
        {
            bool input = false;
            int k1 = 0, k2 = 0;
            for (int w = 1; w < Horizont[index].p.Length - 1; w++)
            {
                if (Horizont[index].p[w] > sum1)
                {
                    if (!input)
                    {
                        k1 = w;
                        input = true;
                    }
                }
                if (Horizont[index].p[w + 1] < sum1 && input)
                {
                    input = false;
                    k2 = w;
                    if (k2 - k1 < 50)
                        continue;
                    //List<int> Maxumums = SearchMaximums(Horizont[index].p, sum1, max, Horizont[index].sobel1, true);
                    //if (Maxumums.Count < 4)
                    //    continue;
                    Plate plate = new Plate();
                    plate.x1 = k1;
                    plate.x2 = k2;
                    plate.y1 = Horizont[index].k1;
                    plate.y2 = Horizont[index].k2;

                    plate.original = Original.Copy(new System.Drawing.Rectangle(plate.x1, plate.y1, plate.x2 - plate.x1, plate.y2 - plate.y1));
                    float sum=0;
                    for(int hg=plate.y1; hg<plate.y2; hg++)
                        sum+=u[hg];
                    double alpha = 0.15 * (plate.y2 - plate.y1) + 0.25 / 0.5 + 0.4 / sum + 0.4*Math.Abs((Math.Abs(k1-k2)/Math.Abs(plate.y1 - plate.y2))-5);
                    if (!Ugol(plate))
                        Plate.Add(plate);
                    
                }
            }
        }

        public bool Ugol(Plate plate)
        {
            LineSegment2D[] lines = null;
            Image<Gray, byte> gray = plate.original.Convert<Gray, byte>();
            Image<Gray, float> sobel = gray.Sobel(0, 1, 3);
            CvInvoke.cvConvert(sobel, gray);
            try
            {
                lines = gray.HoughLinesBinary(1, Math.PI / 45, 50, sobel.Width / 3, 0)[0];
            }
            catch {  }
            if (lines == null || lines.Length == 0)
            {
                return true;
            }

            double angle = 0;
            LineSegment2D avr = new LineSegment2D();
            foreach (LineSegment2D seg in lines)
            {
                avr.P1 = new System.Drawing.Point(avr.P1.X + seg.P1.X, avr.P1.Y + seg.P1.Y);
                avr.P2 = new System.Drawing.Point(avr.P2.X + seg.P2.X, avr.P2.Y + seg.P2.Y);
            }
            avr.P1 = new System.Drawing.Point(avr.P1.X / lines.Length, avr.P1.Y / lines.Length);
            avr.P2 = new System.Drawing.Point(avr.P2.X / lines.Length, avr.P2.Y / lines.Length);
            LineSegment2D horizontal = new LineSegment2D(avr.P1, new System.Drawing.Point(avr.P2.X, avr.P1.Y));
            
            double c = horizontal.P2.X - horizontal.P1.X;
            double a = Math.Abs(horizontal.P2.Y - avr.P2.Y);
            double b = Math.Sqrt(c * c + a * a);
            angle = (a / b * (180 / Math.PI)) * (horizontal.P2.Y > avr.P2.Y ? 1 : -1);
            plate.Alpha = angle;
            plate.rotate = plate.original.Rotate(angle, new Bgr(255, 255, 255));
            
            return false;
        }
        
        public void Horizontal()
        {
            System.Diagnostics.Stopwatch stop = new System.Diagnostics.Stopwatch();
            stop.Start();

            u = Horiz(SobelGray);
            stop.Stop();
            u = Proizvodnij1(u);
            u = Proizvodnij2(u);

            
            float max = MAX(u);
            DrawLine(u, max, Sobel);
            float sum = DrawMiddleLineHoriz(u, max, Sobel);
            List<int> maximum = SearchMaximums(u, sum, max, Sobel, false);
            Greenze(maximum, u);
            
            
            for (int i = 0; i < Horizont.Count; i++)
            {
                //Sobel.Draw(new CircleF(new System.Drawing.PointF(Convert.ToSingle(Sobel.Width * u[Horizont[i].k1] / max), Horizont[i].k1), 3), new Bgr(System.Drawing.Color.Red), 3);
                //Sobel.Draw(new CircleF(new System.Drawing.PointF(Convert.ToSingle(Sobel.Width * u[Horizont[i].k2] / max), Horizont[i].k2), 3), new Bgr(System.Drawing.Color.Red), 3);
                Horizont[i].original = Original.Copy(new System.Drawing.Rectangle(0,Horizont[i].k1, Original.Width, Horizont[i].k2 - Horizont[i].k1));
                Horizont[i].sobel = Horizont[i].original.Convert<Gray, byte>();
                Horizont[i].sobel1 = Horizont[i].original.Convert<Bgr, byte>();
                Vertical(i);
                Horizont[i].p = Proizvodnij1(Horizont[i].p);
                Horizont[i].p = Proizvodnij2(Horizont[i].p);
                Horizont[i].p = Proizvodnij2(Horizont[i].p);
                float max1 = MAX(Horizont[i].p);
                float sum1 = DrawMiddleLineVertical(i, max1);
                float h = Horizont[i].sobel1.Height * sum1 / max1;
                Horizont[i].sobel1.Draw(new LineSegment2DF(new System.Drawing.PointF(0, h), new System.Drawing.PointF(Horizont[i].sobel1.Width, h)), new Bgr(System.Drawing.Color.Blue), 2);
                GetPlate(i, sum1, max1);
            }
            stop.Stop();
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            Int32 ret = 0;
            for (int i = 0; i < Plate.Count; i++)
            {
                Ugol(Plate[i]);
                Plate[i].rotate2 = Plate[i].rotate.Resize(260, 56, Emgu.CV.CvEnum.INTER.CV_INTER_AREA);
                //
             
                Plate[i].rotate1 =Plate[i].rotate2.Convert<Gray, byte>();
                CvInvoke.cvCLAHE(Plate[i].rotate1, 5, new System.Drawing.Size(8, 8), Plate[i].rotate1);
                //Plate[i].rotate1 = Threshold.GrayHCH(Plate[i].rotate1);
                CvInvoke.cvNormalize(Plate[i].rotate1, Plate[i].rotate1, 0, 255, Emgu.CV.CvEnum.NORM_TYPE.CV_MINMAX, Plate[i].rotate1);

                CvInvoke.cvAdaptiveThreshold(Plate[i].rotate1, Plate[i].rotate1, 255, Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_GAUSSIAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY_INV, 15, 13);
                List<System.Drawing.Rectangle> rec = new List<System.Drawing.Rectangle>();
               // Plate[i].rotate1 = Plate[i].rotate1.MorphologyEx(new StructuringElementEx(5, 5, 3, 3, Emgu.CV.CvEnum.CV_ELEMENT_SHAPE.CV_SHAPE_RECT), Emgu.CV.CvEnum.CV_MORPH_OP.CV_MOP_CLOSE, 1);
                using (Emgu.CV.MemStorage storage = new MemStorage())
                {
                    int il = 0;
                    for (Emgu.CV.Contour<System.Drawing.Point> contours = Plate[i].rotate1.Convert<Gray, byte>().FindContours( Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_CCOMP); contours != null; contours = contours.HNext)
                    {
                        Emgu.CV.Contour<System.Drawing.Point> currentContour = contours;

                        if (currentContour.BoundingRectangle.X > 3 && currentContour.BoundingRectangle.Width > 10 && currentContour.BoundingRectangle.Height > 10 && currentContour.BoundingRectangle.Height < 200 && currentContour.BoundingRectangle.Width < 50 && currentContour.BoundingRectangle.Height / currentContour.BoundingRectangle.Width <1.5)
                        {
                            il++;
                            Plate[i].rotate2.Draw(currentContour.BoundingRectangle, new Bgr(System.Drawing.Color.Red), 2);
                            rec.Add(currentContour.BoundingRectangle);
                        }
                    }
                    /*if (il < 1)
                    {
                        Plate.Remove(Plate[i]);
                        i--;
                        continue;
                    }*/
                }
                for (int io = rec.Count - 1; io >= 0; io--)
                {
                    for (int ip = 0; ip < io; ip++)
                    {
                        if (rec[ip].X > rec[ip + 1].X)
                        {
                            System.Drawing.Rectangle r = rec[ip];
                            rec[ip] = rec[ip + 1];
                            rec[ip+1] = r;
                        }
                    }
                }
                
                GetRect(i, rec);

                if(!IsPlate(i, rec))
                {
                    Plate.Remove(Plate[i]);
                    i--;
                    continue;
                }
                for (int ih = 0; ih < rec.Count; ih++)
                {
                    LetterDigit l = new LetterDigit();
                    l.LD = Plate[i].rotate1.Copy(rec[ih]);
                    ret++;
                    Plate[i].digit.Add(l);
                }

                Plate[i].px = new float[Plate[i].rotate1.Width];
                for (int kl = 0; kl < Plate[i].rotate1.Width; kl++)
                {
                    for (int kp = 0; kp< Plate[i].rotate1.Height; kp++)
                    {
                        if (Convert.ToInt32(Plate[i].rotate1[kp, kl].Intensity) == 255)
                            Plate[i].px[kl]++;
                    }
                }
                Plate[i].px = Proizvodnij1(Plate[i].px);
                Plate[i].px = Proizvodnij2(Plate[i].px);
                float maxpx = MAX(Plate[i].px);
                

                for (int h1 = 0, x = 0; h1 < Plate[i].px.Length - 1; h1++, x++)
                {
                    float j = Plate[i].rotate2.Height * Plate[i].px[h1] / (maxpx);
                    float j1 = Plate[i].rotate2.Height *Plate[i].px[h1 + 1] / (maxpx);
                    Plate[i].rotate2.Draw(new LineSegment2DF(new System.Drawing.PointF(x, j), new System.Drawing.PointF(x, j1)), new Bgr(System.Drawing.Color.Red), 2);
                }
                List<int> min1 = SearchMininum(Plate[i].px, i, maxpx);
                /* double sumraz = Plate[i].rotate1.Width / min1.Count - 10;
                 for (int il = 0; il < min1.Count-1; il++)
                 {
                     double ggg= Math.Abs( min1[il+1] - min1[il]);
                     if (ggg < sumraz)
                     {
                         min1.Remove(min1[il + 1]);
                         il--;
                         continue;
                     }
                    /* if (il % 2 == 0)
                         Plate[i].rotate2.Draw(new LineSegment2DF(new System.Drawing.PointF(min1[il], 10), new System.Drawing.PointF(Convert.ToSingle(min1[il+1]), 10)), new Bgr(System.Drawing.Color.Aqua), 2);
                     if (il % 2 == 1)
                         Plate[i].rotate2.Draw(new LineSegment2DF(new System.Drawing.PointF(min1[il], 15), new System.Drawing.PointF(Convert.ToSingle(min1[il+1]), 15)), new Bgr(System.Drawing.Color.Orange), 2); 
                 }*/
                for (int il = 0; il < Plate[i].digit.Count; il++)
                {
                    for(int gh = 0; gh<images.Length; gh++)
                    {
                        Image<Gray, byte> res = Plate[i].digit[il].LD.Resize(10,18, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC).Sub(images[gh]);
                                
                        Plate[i].digit[il]._list.Add(Value2(images[gh], res));                        
                    }
                }
                
                
                //xOOOxxOOO
                Plate[i].text = TextLetter(i, 0) + TextDigit(i, 1) + TextDigit(i, 2) + TextDigit(i, 3) + TextLetter(i, 4) + TextLetter(i, 5) + TextDigit(i, 6) + TextDigit(i, 7) + TextDigit(i, 8);
                Original.Draw(new System.Drawing.Rectangle(Plate[i].x1, Plate[i].y1, Plate[i].x2 - Plate[i].x1, Plate[i].y2 - Plate[i].y1), new Bgr(System.Drawing.Color.Red), 2);
            }
            st.Stop();
        }

        public List<System.Drawing.Rectangle> GetRect(int i, List<System.Drawing.Rectangle> rec)
        {
            float sum = 0.0f;
            for (int ij = 0; ij < rec.Count; ij++)
            {
                sum += rec[ij].Y + rec[ij].Height;
            }
            float sredY = sum / rec.Count;
            Plate[i].rotate2.Draw(new LineSegment2DF(new System.Drawing.PointF(0, sredY), new System.Drawing.PointF(Plate[i].rotate2.Width, sredY)), new Bgr(System.Drawing.Color.Green), 2);

            for (int kl = 0; kl < rec.Count; kl++)
            {
                if (System.Math.Abs(sredY - (rec[kl].Y + rec[kl].Height)) > 8)
                {
                    rec.Remove(rec[kl]);
                    
                }
            }

            float sredX = 0.0f;
            for (int kl = 0; kl < rec.Count; kl++)
            {
                sredX += rec[kl].Width;
            }
            sredX = sredX / rec.Count;

            for (int lp = 0; lp < rec.Count; lp++)
            {
                if (System.Math.Abs(sredX - rec[lp].Width) > 10 && rec[lp].Height / rec[lp].Width < 1.5)
                {
                    rec.Remove(rec[lp]);
                    
                }
            }
            
            return rec;
        }

        bool IsPlate(int i, List<System.Drawing.Rectangle> rect)
        {
            float sum = 0.0f;
            for (int ij = 0; ij < rect.Count; ij++)
            {
                sum += rect[ij].Y + rect[ij].Height;
            }
            float sredY = sum / rect.Count;

            int index = 0;
            for (int kl = 0; kl < rect.Count; kl++)
            {
                if (System.Math.Abs(sredY - (rect[kl].Y + rect[kl].Height)) < 8)
                    index++;
            }

            float sredX = 0.0f;
            for (int kl = 0; kl < rect.Count; kl++)
            {
                sredX += rect[kl].Width;
            }
            sredX = sredX / rect.Count;

            int index2 = 0;
            for (int lp = 0; lp < rect.Count; lp++)
            {
                if (System.Math.Abs(sredX - rect[lp].Width) < 10)
                    index2++;
            }

            if ((index == 8 && index2 == 8) || (index == 9 && index2 == 9))
                return true;
            return false;
        }

        double Value(Image<Gray, byte> im, Image<Gray, byte> sample)
        {
            int [] sum = new int[im.Width];
            for(int i=0; i<im.Width; i++)
                for (int j = 0; j < im.Height; j++)
                {
                    if(im[j,i].Intensity > 250)
                    sum[i] ++;
                }

            int[] sum1 = new int[sample.Width];
            for (int i = 0; i < sample.Width; i++)
                for (int j = 0; j < sample.Height; j++)
                {
                    if (sample[j, i].Intensity > 250)
                        sum1[i]++;
                }
            double value = 0.0;
            for (int i = 0; i < sum.Length; i++)
            {
                value += System.Math.Abs(sum[i] - sum1[i]);
            }



                return value;
        }

        double Value1(Image<Gray, byte> im, Image<Gray, byte> im2)
        {
            double[] d1 = new double[im.Width];
            for (int i = 0; i < im.Width; i++)
            {
                double sum = 0;
                for (int j = 0; j < im.Height; j++)
                {
                    sum += im[j, i].Intensity;
                }
                d1[i] = sum;
            }

            double[] d2 = new double[im.Height];
            for (int i = 0; i < im.Height; i++)
            {
                double sum = 0;
                for (int j = 0; j < im.Width; j++)
                {
                    sum += im[i, j].Intensity;
                }
                d2[i] = sum;
            }

            double[] d3 = new double[im.Width];
            for (int i = 0; i < im2.Width; i++)
            {
                double sum = 0;
                for (int j = 0; j < im2.Height; j++)
                {
                    sum += im2[j, i].Intensity;
                }
                d3[i] = sum;
            }

            double[] d4 = new double[im2.Height];
            for (int i = 0; i < im2.Height; i++)
            {
                double sum = 0;
                for (int j = 0; j < im2.Width; j++)
                {
                    sum += im2[i, j].Intensity;
                }
                d4[i] = sum;
            }

            double kl = 0;
            for (int i = 0; i < d1.Length; i++)
            {
                kl+=System.Math.Abs(d1[i]-d1[3]);
            }
            
            

            double kl1 = 0;
            for (int i = 0; i < d2.Length; i++)
            {
                kl1 += System.Math.Abs(d2[i] - d4[3]);
            }

            return 1-(kl + kl1) / 512000;
        }

        public double Value2(Image<Gray, byte> im, Image<Gray, byte> sample)
        {
            MCvScalar m1, m2;
            Gray gray;
            im.AvgSdv(out gray, out m1);
            Image<Gray, byte> f = im;
            sample.AvgSdv(out gray, out m2);
            return Math.Abs(m1.v0 - m2.v0);
        }

        /*public double VVV(int digit, Config.Config.Data data)
        {
            double add = 0.0;
            
          //  add += System.Math.Abs(conf.data[digit].M1 - data.M1);
            add += System.Math.Abs(conf.data[digit].M2 - data.M2);
          //  add += System.Math.Abs(conf.data[digit].M3 - data.M3);
           // add += System.Math.Abs(conf.data[digit].M4 - data.M4);
            //add += System.Math.Abs(conf.data[digit].M5 - data.M5);
            //add += System.Math.Abs(conf.data[digit].M6 - data.M6);
           // add += System.Math.Abs(conf.data[digit].M7 - data.M7);
            return add;
        }*/

        public string TextLetter(int i, int dig)
        {
            try
            {
                double min = Plate[i].digit[dig]._list[10];
                int op = 10;
                for (int lp = 11; lp < Plate[i].digit[dig]._list.Count; lp++)
                {

                    double v = Plate[i].digit[dig]._list[lp];
                    if (min < v)
                    {
                        min = v;
                        op = lp;
                    }
                }

                switch (op)
                {
                    case 10:  return "A";
                    case 11: return "B";
                    case 12:  return "E";
                    case 13:  return "К";
                    case 14:  return "М";
                    case 15:  return "Н";
                    case 16:  return "O";
                    case 17:  return "P";
                    case 18:  return "С";
                    case 19: return "Т";
                    case 20:  return "X";
                    case 21:  return "Y";
                    default: return "";
                }
            }
            catch { return ""; }
        }

        public string TextDigit(int i, int dig)
        {
            try
            {
                double min = Plate[i].digit[dig]._list[0];
                int op = 0;
                for (int lp = 1; lp < 10; lp++)
                {
                    double v = Plate[i].digit[dig]._list[lp];
                    if (min < v)
                    {
                        min = v;
                        op = lp;
                    }
                }

                switch (op)
                {
                    case 0: return "0";
                    case 1: return "1";
                    case 2: return "2";
                    case 3: return "3";
                    case 4: return "4";
                    case 5: return "5";
                    case 6: return "6";
                    case 7: return "7";
                    case 8: return "8";
                    case 9: return "9";
                    default: return "";
                }
            }
            catch { return ""; }
        }
    }
}
