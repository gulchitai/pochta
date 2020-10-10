using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PasportRecognition.Recognize
{
    public static class Threshold
    {
        #region Метод ОЦУ
        public static Image<Gray, byte> Ocu(Image<Gray, byte> image)
        {
            int min = Convert.ToInt32(image[0, 0].Intensity);
            int max = Convert.ToInt32(image[0, 0].Intensity);

            int i, temp, temp1;
            int[] hist;
            int histSize;

            int alpha, beta, threshold = 0;
            double sigma, maxSigma = -1;
            double w1, a;

            for (i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    temp = Convert.ToInt32(image[j, i].Intensity);
                    if (temp < min) min = temp;
                    if (temp > max) max = temp;
                }
            }
            histSize = max - min + 1;

            hist = new int[histSize];
            for (i = 0; i < histSize; i++)
            {
                hist[i] = 0;
            }

            for (i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    hist[Convert.ToInt32(image[j, i].Intensity) - min]++;
                }
            }

            temp = temp1 = 0;
            alpha = beta = 0;
            for (i = 0; i <= (max - min); i++)
            {
                temp += i * hist[i];
                temp1 += hist[i];
            }

            for (i = 0; i < (max - min); i++)
            {
                alpha += i * hist[i];
                beta += hist[i];
                w1 = Convert.ToDouble(beta) / temp1;
                a = alpha / beta - (temp - alpha) / (temp1 - beta);
                sigma = w1 * (1 - w1) * a * a;
                if (sigma > maxSigma)
                {
                    maxSigma = sigma;
                    threshold = i;
                }
            }
            return image.ThresholdBinary(new Gray(threshold), new Gray(255));
        }
        #endregion

        #region Janovica
        public static Image<Gray, byte> Janovica(Image<Gray, byte> image)
        {
            Image<Gray, byte> im = new Image<Gray, byte>(image.Width, image.Height, new Gray(255));
            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    double Rn = image[j, i - 1].Intensity + image[j, i + 1].Intensity +
                        image[j - 1, i].Intensity + image[j + 1, i].Intensity + 4 * image[j, i].Intensity;

                    im[j, i] = new Gray(Convert.ToInt32(image[j, i].Intensity + 0.5 * Rn / 4));
                }
            }
            return im;
        }

        #endregion

        #region Niblek

        public static Image<Gray, byte> Niblek(Image<Gray, byte> image)
        {
            Image<Gray, byte> im = new Image<Gray, byte>(image.Width, image.Height, new Gray(255));
            for (int i = 1; i < image.Width - 1; i++)
            {
                for (int j = 1; j < image.Height - 1; j++)
                {
                    double d = Math.Pow((Math.Pow(image[j, i].Intensity - image[j, i - 1].Intensity, 2) +
                        Math.Pow(image[j, i].Intensity - image[j, i + 1].Intensity, 2) +
                        Math.Pow(image[j, i].Intensity - image[j - 1, i].Intensity, 2) +
                        Math.Pow(image[j, i].Intensity - image[j + 1, i].Intensity, 2)) / 4, 0.5);
                    im[j, i] = new Gray(image[j, i].Intensity + 1 * d);
                }
            }
            return im;
        }

        #endregion

        #region SingleScaleRetinex

        public static Image<Bgr, byte> SingleScaleRetinex(this Image<Bgr, byte> img, int gaussianKernelSize, double sigma)
        {
            var radius = gaussianKernelSize / 2;
            var kernelSize = 2 * radius + 1;

            var ycc = img.Convert<Ycc, byte>();

            // Формируем ядро Гауссиана
            var sum = 0f;
            var gaussKernel = new float[kernelSize * kernelSize];
            for (int i = -radius, k = 0; i <= radius; i++, k++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    var val = (float)Math.Exp(-(i * i + j * j) / (sigma * sigma));
                    gaussKernel[k] = val;
                    sum += val;
                }
            }
            for (int i = 0; i < gaussKernel.Length; i++)
                gaussKernel[i] /= sum;

            // Работаем только с яркостным каналом
            var gray = new Image<Gray, byte>(ycc.Size);
            CvInvoke.cvSetImageCOI(ycc, 1);
            CvInvoke.cvCopy(ycc, gray, IntPtr.Zero);

            // Размеры изображения
            var width = img.Width;
            var height = img.Height;

            var bmp = gray.Bitmap;
            var bitmapData = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, gray.Size), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            unsafe
            {
                for (var y = 0; y < height; y++)
                {
                    var row = (byte*)bitmapData.Scan0 + y * bitmapData.Stride;
                    for (var x = 0; x < width; x++)
                    {
                        var color = row + x;

                        float val = 0;

                        for (int i = -radius, k = 0; i <= radius; i++, k++)
                        {
                            var ii = y + i;
                            if (ii < 0) ii = 0; if (ii >= height) ii = height - 1;

                            var row2 = (byte*)bitmapData.Scan0 + ii * bitmapData.Stride;
                            for (int j = -radius; j <= radius; j++)
                            {
                                var jj = x + j;
                                if (jj < 0) jj = 0; if (jj >= width) jj = width - 1;

                                val += *(row2 + jj) * gaussKernel[k];

                            }
                        }

                        var newColor = 127.5 + 255 * Math.Log(*color / val);
                        if (newColor > 255)
                            newColor = 255;
                        else if (newColor < 0)
                            newColor = 0;
                        *color = (byte)newColor;
                    }
                }
            }
            bmp.UnlockBits(bitmapData);

            CvInvoke.cvCopy(gray, ycc, IntPtr.Zero);
            CvInvoke.cvSetImageCOI(ycc, 0);

            return ycc.Convert<Bgr, byte>();

        }

        #endregion

        #region AnisotropicSmoothing

        public static Image<Gray, byte> AnisotropicSmoothing(Image<Gray, byte> im, int smooth)
        {
            double eps = .1;
            Image<Gray, byte> f = new Image<Gray, byte>(im.Size.Width, im.Size.Height);
            bool first = true;
            double tmp, min=0, max=0;
            for (int y = 0; y < f.Size.Height; y++)
            {
                for(int x=0; x<f.Size.Width; x++ ){
                    tmp = im[y, x].Intensity;
                    if (x > 0 && x < im.Size.Width - 1 && y > 0 && y < im.Size.Height - 1)
                    {
                        double A = tmp;
                        double E = im[y + 1, x].Intensity;
                        double S = im[y, x + 1].Intensity;
                        double N = im[y, x - 1].Intensity;
                        double W = im[y - 1, x].Intensity;

                        double Lw = A - W;
                        double Le = A - E;
                        double Ln = A - N;
                        double Ls = A - S;

                        double pw = Math.Min(A, W) / (Math.Abs(A - W) + eps);
                        double pe = Math.Min(A, E) / (Math.Abs(A - E) + eps);
                        double pn = Math.Min(A, N) / (Math.Abs(A - N) + eps);
                        double ps = Math.Min(A, S) / (Math.Abs(A - S) + eps);

                        tmp = A + smooth*(Ln * pn + Ls * ps + Le * pe + Lw * pw);

                        f[y, x] = new Gray( tmp);

                        if (first) { min = max = tmp; first = false; }
                        else
                        {
                            if (tmp < min) min = tmp;
                            else
                                max = tmp;
                        }
                    }
                }
            }
            return f;
        }

        #endregion

        #region IsotropicSmoothing

        public static Image<Gray, byte> IsotropicSmoothing(Image<Gray, byte> im, int smooth)
        {
            double eps = .1;
            Image<Gray, byte> f = new Image<Gray, byte>(im.Size.Width, im.Size.Height);
            bool first = true;
            double tmp, min = 0, max = 0;
            for (int y = 0; y < f.Size.Height; y++)
            {
                for (int x = 0; x < f.Size.Width; x++)
                {
                    tmp = im[y, x].Intensity;
                    if (x > 0 && x < im.Size.Width - 1 && y > 0 && y < im.Size.Height - 1)
                    {
                        double A = tmp;
                        double E = im[y + 1, x].Intensity;
                        double S = im[y, x + 1].Intensity;
                        double N = im[y, x - 1].Intensity;
                        double W = im[y - 1, x].Intensity;

                        double Lw = A - W;
                        double Le = A - E;
                        double Ln = A - N;
                        double Ls = A - S;

                        double pw = Math.Min(A, W) / (Math.Abs(A - W) + eps);
                        double pe = Math.Min(A, E) / (Math.Abs(A - E) + eps);
                        double pn = Math.Min(A, N) / (Math.Abs(A - N) + eps);
                        double ps = Math.Min(A, S) / (Math.Abs(A - S) + eps);

                        tmp = A + smooth * (Ln + Ls + Le + Lw);

                        f[y, x] = new Gray(tmp);

                        if (first) { min = max = tmp; first = false; }
                        else
                        {
                            if (tmp < min) min = tmp;
                            else
                                max = tmp;
                        }
                    }
                }
            }
            return f;
        }

        #endregion

        public static Image<Gray, byte> GrayHCH(Image<Gray, byte> im)
        {
            int x = Convert.ToInt32(im.Width * 0.1);
            int y = Convert.ToInt32(im.Height * 0.1);

            for(int j=0; j<im.Height; j++)
                for (int i = 0; i < im.Width; i++)
                {
                    if (j > im.Height / 2 - y / 2 && j < im.Height / 2 + y / 2 && i > im.Width / 2 - x / 2 && i < im.Height / 2 + y / 2) 
                        continue;
                    im[j, i] = new Gray(0);
                }
            return im;
        }

    }
}