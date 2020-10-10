using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PasportRecognition.Recognize
{
    public class Horizontal
    {
        public int i;
        public int k1;
        public int k2;
        public Image<Bgr, byte> original;
        public Image<Gray, byte> sobel;
        public Image<Bgr, byte> sobel1;
        public float[] p;
        public float[] pShrih;
        public float Alpha;
    }
}
