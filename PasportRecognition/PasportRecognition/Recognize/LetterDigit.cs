using System;
using System.Collections.Generic;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace PasportRecognition.Recognize
{
    public class LetterDigit
    {
        public Image<Gray, byte> LD;
        public List<double> _list = new List<double>();
    }
}
