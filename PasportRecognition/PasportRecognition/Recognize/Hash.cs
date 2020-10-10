using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PasportRecognition.Recognize
{
    public class Hash
    {
        public static Int64 calcImageHash(Image<Gray, byte> image)
        {
            int x = 0;
            Int64 hash = 0;
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    if (image[j, i].Intensity > 250)
                        hash |= 1 << x; 
                    x++;
                }
            return hash;
        }

        public static Int64 CalcHammingDistance(Int64 x, Int64 y)
        {
            Int64 dist = 0, val = x ^ y;
            while (val!=0)
            {
                dist++;
                val &= val - 1;
            }
            return dist;
        }
    }
}
