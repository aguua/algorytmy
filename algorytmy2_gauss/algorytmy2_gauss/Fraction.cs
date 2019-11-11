using System;
using System.Numerics;

namespace algorytmy2_gauss
{
    public class Fraction
    {
        private BigInteger n; //numerator
        private BigInteger d; //demnominator
        
        public Fraction(BigInteger n, BigInteger d)
        {
            this.n = n;
            this.d = d;
        }
        public Fraction() { this.n = 0; this.d = 0; }
        public override string ToString()
        {
            return n.ToString() + "/" + d.ToString();
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            Fraction f = (f1.d == f2.d)
                ? new Fraction((f1.n + f2.n), f1.d)
                : new Fraction((f1.n * f2.d) + (f2.n * f1.d), (f1.d * f2.d));
            return f.Reduce();
        }
        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            Fraction f = (f1.d == f2.d)
                ? new Fraction((f1.n - f2.n), f1.d)
                : new Fraction((f1.n * f2.d) - (f2.n * f1.d), (f1.d * f2.d));
            return f.Reduce();
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            Fraction f = new Fraction((f1.n * f2.n), (f1.d * f2.d));
            return f.Reduce();
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            Fraction f = new Fraction((f1.n * f2.d), (f1.d * f2.n));
            return f.Reduce();
        }

        public static bool operator <(Fraction f1, Fraction f2)
        {
            return f1.d == f2.d
                ? f1.n < f2.n ? true : false
                : (f1.n * f2.d) < (f1.d * f2.n) ? true : false;
        }

        public static bool operator >(Fraction f1, Fraction f2)
        {
            return f1.d == f2.d
                ? f1.n > f2.n ? true : false
                : (f1.n * f2.d) > (f1.d * f2.n) ? true : false;
        }

        public static bool operator ==(Fraction f1, Fraction f2)
        {
            f1.Reduce(); f2.Reduce();
            return f1.n == f2.n ? true : false;
        }

        public static bool operator !=(Fraction f1, Fraction f2)
        {
            return f1.d == f2.d
                ? f1.n != f2.n ? true : false
                : (f1.n * f2.d) != (f1.d * f2.n) ? true : false;
        }

        public static implicit operator Fraction(int v)
        {
            return new Fraction(v, 1);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var other = obj as Fraction;
            return other != null && this.n == other.n;
        }

        public override int GetHashCode()
        {
            return 0;
        }


        public Fraction Reduce()
        {
            BigInteger a = this.n < 0 ? -this.n : this.n;
            BigInteger b = this.d < 0 ? -this.d : this.d;
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            BigInteger gcd = (a == 0 ? b : a);  //gcd (NWD)

            if (gcd > 1)
            {
                this.n /= gcd;
                this.d /= gcd;
            }
            if (this.d < 0)
            {
                this.n *= -1;
                this.d *= -1;
            }
            return this;
        }

        public double ToDouble()
        {
            double change = (double)this.n / (double)this.d;
            return change;
        }
    }
}
