using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jImaging
{
    public class ExifFraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public ExifFraction(int numerator, int denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        public ExifFraction(uint numerator, uint denominator)
        {
            Numerator = Convert.ToInt32(numerator);
            Denominator = Convert.ToInt32(denominator);
        }

        public ExifFraction(int numerator) : this(numerator, 1){}

        public override string ToString()
        {
            return Denominator == 1 ? 
                Numerator.ToString() : 
                string.Format($"{Numerator}/{Denominator}");
        }

        public double Value()
        {
            return (double) Numerator/Denominator;
        }
    }
}
