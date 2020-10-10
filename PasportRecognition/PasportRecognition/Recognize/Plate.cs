using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PasportRecognition.Recognize
{
    public class Plate
    {
        public int x1, y1, x2, y2;
        public float[] px;
        public double Alpha;
        public Image<Bgr, byte> original;
        public Image<Gray, byte> sobel;
        public Image<Bgr, byte> rotate;
        public Image<Gray, byte> rotate1;
        public Image<Bgr, byte> rotate2;
        public int[] massiv = new int[256];
        public Image<Bgr, byte> gmassive = null;
        public List<LetterDigit> digit = new List<LetterDigit>();
        public string text = "";
    }
}
