﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace Decompose.Numerics
{
    public struct Rational : IComparable<Rational>, IEquatable<Rational>
    {
        private BigInteger n;
        private BigInteger d;
        public bool IsInteger { get { return d.IsOne; } }
        public BigInteger Numerator { get { return n; } }
        public BigInteger Denominator { get { return d; } }
        public Rational(BigInteger n, BigInteger d)
        {
            if (d == 0)
                throw new DivideByZeroException();
            if (d < 0)
            {
                n = -n;
                d = -d;
            }
            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            if (!n.IsOne)
            {
                n /= gcd;
                d /= gcd;
            }
            this.n = n;
            this.d = d;
        }
        public static Rational operator +(Rational a, Rational b) { return new Rational(a.n * b.d + b.n * a.d, a.d * b.d); }
        public static Rational operator -(Rational a, Rational b) { return new Rational(a.n * b.d - b.n * a.d, a.d * b.d); }
        public static Rational operator *(Rational a, Rational b) { return new Rational(a.n * b.n, a.d * b.d); }
        public static Rational operator /(Rational a, Rational b) { return new Rational(a.n * b.d, a.d * b.n); }
        public static Rational operator -(Rational a) { return new Rational(-a.n, a.d); }
        public static bool operator ==(Rational a, Rational b) { return a.Equals(b); }
        public static bool operator !=(Rational a, Rational b) { return !a.Equals(b); }
        public static bool operator <(Rational a, Rational b) { return a.CompareTo(b) < 0; }
        public static bool operator <=(Rational a, Rational b) { return a.CompareTo(b) <= 0; }
        public static bool operator >(Rational a, Rational b) { return a.CompareTo(b) > 0; }
        public static bool operator >=(Rational a, Rational b) { return a.CompareTo(b) >= 0; }
        public static implicit operator Rational(int a) { return new Rational(a, 1); }
        public static implicit operator Rational(uint a) { return new Rational(a, 1); }
        public static implicit operator Rational(long a) { return new Rational(a, 1); }
        public static implicit operator Rational(ulong a) { return new Rational(a, 1); }
        public static explicit operator Rational(double a) { return new Rational((BigInteger)a, 1); }
        public static implicit operator Rational(BigInteger a) { return new Rational(a, 1); }
        public static explicit operator BigInteger(Rational a) { if (a.d != 1) throw new InvalidCastException(); return a.n; }
        public static explicit operator int(Rational a) { if (a.d != 1) throw new InvalidCastException(); return (int)a.n; }
        public static explicit operator double(Rational a) { return (double)a.n / (double)a.d; }
        public bool Equals(Rational a) { return n == a.n && d == a.d; }
        public int CompareTo(Rational a) { return (n * a.d).CompareTo(a.n * d); }
        public override bool Equals(object obj) { return obj is Rational && Equals((Rational)obj); }
        public override int GetHashCode() { return n.GetHashCode() ^ d.GetHashCode(); }
        public override string ToString() { return d.IsOne ? n.ToString() : string.Format("{0}/{1}", n, d); }
    }
}
