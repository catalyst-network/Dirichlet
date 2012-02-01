﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;

namespace Decompose.Numerics
{
    public static class IntegerMath
    {
        public static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        public static BigInteger Min(BigInteger a, BigInteger b)
        {
            return a < b ? a : b;
        }

        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public static BigInteger Max(BigInteger a, BigInteger b)
        {
            return a > b ? a : b;
        }

        public static int QuotientFloor(int a, int b)
        {
            return a / b;
        }

        public static BigInteger QuotientFloor(BigInteger a, BigInteger b)
        {
            return a / b;
        }

        public static int QuotientCeiling(int a, int b)
        {
            return (a + b - 1) / b;
        }

        public static BigInteger QuotientCeiling(BigInteger a, BigInteger b)
        {
            return (a + b - 1) / b;
        }

        public static int MultipleOfFloor(int a, int b)
        {
            return a / b * b;
        }

        public static BigInteger MultipleOfFloor(BigInteger a, BigInteger b)
        {
            return a / b * b;
        }

        public static int MultipleOfCeiling(int a, int b)
        {
            return (a + b - 1) / b * b;
        }

        public static BigInteger MultipleOfCeiling(BigInteger a, BigInteger b)
        {
            return (a + b - 1) / b * b;
        }

        public static int Modulus(int n, int p)
        {
            return (n % p + p) % p;
        }

        public static int Modulus(BigInteger n, int p)
        {
            return ((int)(n % p) + p) % p;
        }

        public static BigInteger Modulus(BigInteger n, BigInteger p)
        {
            return (n % p + p) % p;
        }

        public static int Power(int n, int e)
        {
            return (int)Math.Round(Math.Pow(n, e));
        }

        public static BigInteger Power(BigInteger n, int e)
        {
            return BigInteger.Pow(n, e);
        }

        public static BigInteger Power(BigInteger n, BigInteger e)
        {
            return BigInteger.Pow(n, (int)e);
        }

        public static int ModularInverse(int n, int p)
        {
            int x;
            int y;
            ExtendedGreatestCommonDivisor(n, p, out x, out y);
            if (x < 0)
                x += p;
            return x;
        }

        public static uint ModularInverse(uint n, uint p)
        {
            return (uint)ModularInverse((int)n, (int)p);
        }

        public static long ModularInverse(long n, long p)
        {
            long x;
            long y;
            ExtendedGreatestCommonDivisor(n, p, out x, out y);
            if (x < 0)
                x += p;
            return x;
        }

        public static ulong ModularInverse(ulong n, ulong p)
        {
            return (ulong)ModularInverse((long)n, (long)p);
        }

        public static int ModularInverse(BigInteger n, int p)
        {
            if (p == 0)
                return 1;
            int r = (int)(n % p);
            int x;
            int y;
            ExtendedGreatestCommonDivisor(r, p, out x, out y);
            if (x < 0)
                x += p;
            return x;
        }

        public static BigInteger ModularInverse(BigInteger n, BigInteger p)
        {
            BigInteger x;
            BigInteger y;
            ExtendedGreatestCommonDivisor(n, p, out x, out y);
            if (x < 0)
                x += p;
            return x;
        }

        public static bool IsSquareFree(IEnumerable<int> factors)
        {
            return factors
                .OrderBy(factor => factor)
                .GroupBy(factor => factor)
                .All(grouping => grouping.Count() < 2);
        }

        public static bool IsSquareFree(IEnumerable<BigInteger> factors)
        {
            return factors
                .OrderBy(factor => factor)
                .GroupBy(factor => factor)
                .All(grouping => grouping.Count() < 2);
        }

        public static int GreatestCommonDivisor(int a, int b)
        {
             while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return Math.Abs(a);
        }

        public static uint GreatestCommonDivisor(uint a, uint b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return Math.Abs(a);
        }

        public static ulong GreatestCommonDivisor(ulong a, ulong b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        public static BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            return BigInteger.GreatestCommonDivisor(a, b);
        }

        public static void ExtendedGreatestCommonDivisor(int a, int b, out int c, out int d)
        {
            var x = 0;
            var lastx = 1;
            var y = 1;
            var lasty = 0;

            while (b != 0)
            {
                var quotient = a / b;
                var tmpa = a;
                a = b;
                b = tmpa - quotient * b;
                var tmpx = x;
                x = lastx - quotient * x;
                lastx = tmpx;
                var tmpy = y;
                y = lasty - quotient * y;
                lasty = tmpy;
            }
            c = lastx;
            d = lasty;
        }

        public static void ExtendedGreatestCommonDivisor(long a, long b, out long c, out long d)
        {
            var x = (long)0;
            var lastx = (long)1;
            var y = (long)1;
            var lasty = (long)0;

            if (a < b)
            {
                var tmpa = a;
                a = b;
                b = tmpa;
                x = 1;
                lastx = 0;
                y = 0;
                lasty = 1;
            }
            while (b != 0)
            {
                Debug.Assert(a >= b);
                var b2 = b << 1;
                if (a < b2)
                {
                    Debug.Assert(a / b == 1);
                    var tmpa = a;
                    a = b;
                    b = tmpa - b;
                    var tmpx = x;
                    x = lastx - x;
                    lastx = tmpx;
                    var tmpy = y;
                    y = lasty - y;
                    lasty = tmpy;
                }
                else if (a < b2 + b)
                {
                    Debug.Assert(a / b == 2);
                    var tmpa = a;
                    a = b;
                    b = tmpa - (b << 1);
                    var tmpx = x;
                    x = lastx - (x << 1);
                    lastx = tmpx;
                    var tmpy = y;
                    y = lasty - (y << 1);
                    lasty = tmpy;
                }
                else
                {
                    if (a < int.MaxValue)
                        break;
                    var quotient = a / b;
                    var tmpa = a;
                    a = b;
                    b = tmpa - quotient * b;
                    var tmpx = x;
                    x = lastx - quotient * x;
                    lastx = tmpx;
                    var tmpy = y;
                    y = lasty - quotient * y;
                    lasty = tmpy;
                }
            }
            if (b != 0)
            {
                var aa = (uint)a;
                var bb = (uint)b;
                while (bb != 0)
                {
                    Debug.Assert(aa >= bb);
                    var bb2 = bb << 1;
                    if (aa < bb2)
                    {
                        Debug.Assert(aa / bb == 1);
                        var tmpa = aa;
                        aa = bb;
                        bb = tmpa - bb;
                        var tmpx = x;
                        x = lastx - x;
                        lastx = tmpx;
                        var tmpy = y;
                        y = lasty - y;
                        lasty = tmpy;
                    }
                    else if (aa < bb2 + bb)
                    {
                        Debug.Assert(aa / bb == 2);
                        var tmpa = aa;
                        aa = bb;
                        bb = tmpa - (bb << 1);
                        var tmpx = x;
                        x = lastx - (x << 1);
                        lastx = tmpx;
                        var tmpy = y;
                        y = lasty - (y << 1);
                        lasty = tmpy;
                    }
                    else
                    {
                        var quotient = aa / bb;
                        var tmpa = aa;
                        aa = bb;
                        bb = tmpa - quotient * bb;
                        var tmpx = x;
                        x = lastx - quotient * x;
                        lastx = tmpx;
                        var tmpy = y;
                        y = lasty - quotient * y;
                        lasty = tmpy;
                    }
                }
            }
            c = lastx;
            d = lasty;
        }

        public static void ExtendedGreatestCommonDivisor(BigInteger a, BigInteger b, out BigInteger c, out BigInteger d)
        {
            var x = BigInteger.Zero;
            var lastx = BigInteger.One;
            var y = BigInteger.One;
            var lasty = BigInteger.Zero;

            while (!b.IsZero)
            {
                var quotient = a / b;
                var tmpa = a;
                a = b;
                b = tmpa - quotient * b;
                var tmpx = x;
                x = lastx - quotient * x;
                lastx = tmpx;
                var tmpy = y;
                y = lasty - quotient * y;
                lasty = tmpy;
            }
            c = lastx;
            d = lasty;
        }

        public static int Sqrt(int n)
        {
            return (int)Math.Floor(Math.Sqrt(n));
        }

        private static ISqrtAlgorithm<BigInteger> sqrt = new SqrtNewtonsMethod();

        public static BigInteger Sqrt(BigInteger n)
        {
            return sqrt.Sqrt(n);
        }

        public static int ModularProduct(int a, int b, int modulus)
        {
            return (int)((long)a * b % modulus);
        }

        public static uint ModularProduct(uint a, uint b, uint modulus)
        {
            return (uint)((ulong)a * b % modulus);
        }

        public static long ModularProduct(long a, long b, long modulus)
        {
            var result = (long)UInt128.ModularProduct((ulong)Math.Abs(a), (ulong)Math.Abs(b), (ulong)modulus);
            return (a < 0) != (b < 0) ? -result : result;
        }

        public static ulong ModularProduct(ulong a, ulong b, ulong modulus)
        {
            return UInt128.ModularProduct(a, b, modulus);
        }

        public static int ModularPower(int value, int exponent, int modulus)
        {
            return (int)ModularPower((uint)value, (uint)exponent, (uint)modulus);
        }

        public static uint ModularPower(uint value, uint exponent, uint modulus)
        {
            var result = (uint)1;
            while (exponent != 0)
            {
                if ((exponent & 1) != 0)
                    result = (uint)((ulong)result * value % modulus);
                value = (uint)((ulong)value * value % modulus);
                exponent >>= 1;
            }
            return result;
        }

        public static uint ModularPower(uint value, ulong exponent, uint modulus)
        {
            var result = (uint)1;
            while (exponent != 0)
            {
                if ((exponent & 1) != 0)
                    result = (uint)((ulong)result * value % modulus);
                value = (uint)((ulong)value * value % modulus);
                exponent >>= 1;
            }
            return result;
        }

        public static long ModularPower(long value, long exponent, long modulus)
        {
            return (long)ModularPower((ulong)value, (ulong)exponent, (ulong)modulus);
        }

        private static IReductionAlgorithm<ulong> reduction = new UInt64MontgomeryReduction();

        public static ulong ModularPower(ulong value, ulong exponent, ulong modulus)
        {
            if (modulus <= uint.MaxValue)
                return ModularPower((uint)(value % modulus), exponent, (uint)modulus);
            if ((modulus & 1) == 0)
                return UInt128.ModularPower(value, exponent, modulus);
            var reducer = reduction.GetReducer(modulus);
            var b = reducer.ToResidue(value);
            var result = reducer.ToResidue(1);
            while (exponent != 0)
            {
                if ((exponent & 1) != 0)
                    result.Multiply(b);
                b.Multiply(b);
                exponent >>= 1;
            }
            return result.Value();
        }

        public static BigInteger ModularPower(BigInteger value, BigInteger exponent, BigInteger modulus)
        {
            return BigInteger.ModPow(value, exponent, modulus);
        }

        private static IPrimalityAlgorithm<int> primalityInt = new TrialDivisionPrimality();

        public static bool IsPrime(int n)
        {
            return primalityInt.IsPrime(n);
        }

        private static IPrimalityAlgorithm<BigInteger> primalityBigInteger = MillerRabin.Create(16, new BigIntegerReduction());

        public static bool IsProbablePrime(int n)
        {
            return IsProbablePrime((uint)n);
        }

        public static bool IsProbablePrime(uint n)
        {
            return IntegerMath.ModularPower(2, n - 1, n) == 1;
        }

        public static bool IsProbablePrime(long n)
        {
            return IsProbablePrime((ulong)n);
        }

        public static bool IsProbablePrime(ulong n)
        {
            if (n <= uint.MaxValue)
                return IsProbablePrime((uint)n);
            return ModularPower(2, n - 1, n) == 1;
        }

        public static bool IsProbablePrime(BigInteger n)
        {
            return ModularPower(BigIntegers.Two, n - BigInteger.One, n).IsOne;
        }

        public static bool IsPrime(BigInteger n)
        {
            if (n < int.MaxValue)
                return primalityInt.IsPrime((int)n);
            return primalityBigInteger.IsPrime(n);
        }

        public static BigInteger NextPrime(BigInteger n)
        {
            while (!IsPrime(n))
                ++n;
            return n;
        }

        public static int GetDigitLength(BigInteger n, int b)
        {
            return (int)Math.Ceiling(BigInteger.Log(n, b));
        }

        private static BigInteger limit = (BigInteger)int.MaxValue;
        private static BigInteger four = (BigInteger)4;
        private static BigInteger eight = (BigInteger)8;

        public static int JacobiSymbol(BigInteger m, BigInteger n)
        {
            if (n == 2)
                throw new InvalidOperationException("not an odd prime");
            int result = 1;
            while (true)
            {
                m = m % n;
                if (n <= limit)
                    return result * JacobiSymbol((int)m, (int)n);
                if (m.IsZero)
                    return 0;
                if (m.IsEven)
                {
                    int k = (int)(n % eight);
                    var toggle = k == 1 || k == 7 ? 1 : -1;
                    do
                    {
                        m >>= 1;
                        result *= toggle;
                    }
                    while (m.IsEven);
                }
                if (m.IsOne)
                    return result;
                if (!n.IsEven)
                {
                    if ((int)(m % four) == 3 && (int)(n % four) == 3)
                        result *= -1;
                    var tmp = m;
                    m = n;
                    n = tmp;
                }
            }
        }

        public static int JacobiSymbol(int m, int n)
        {
            int result = 1;
            while (true)
            {
                m = m % n;
                if (m == 0)
                    return 0;
                if ((m & 1) == 0)
                {
                    int k = n & 7;
                    int toggle = k == 1 || k == 7 ? 1 : -1;
                    do
                    {
                        m >>= 1;
                        result *= toggle;
                    }
                    while ((m & 1) == 0);
                }
                if (m == 1)
                    return result;
                if ((n & 1) != 0)
                {
                    if ((m & 3) == 3 && (n & 3) == 3)
                        result *= -1;
                    var tmp = m;
                    m = n;
                    n = tmp;
                }
            }
        }

        public static bool IsQuadraticResidue(BigInteger n, BigInteger p)
        {
            return BigInteger.ModPow(n, (p - 1) / 2, p).IsOne;
        }

        public static int ModularSquareRoot(BigInteger n, int p)
        {
            return (int)ModularSquareRoot(n, (BigInteger)p);
        }

        public static BigInteger ModularSquareRoot(BigInteger n, BigInteger p)
        {
            var r = ModularSquareRootCore(n, p);
            if (r > p / BigIntegers.Two)
                return p - r;
            return r;
        }

        private static BigInteger ModularSquareRootCore(BigInteger n, BigInteger p)
        {
            if (p == 2)
                return BigInteger.One;
            var q = p - 1;
            var s = 0;
            while (q.IsEven)
            {
                q >>= 1;
                ++s;
            }
            if (s == 1)
                return BigInteger.ModPow(n, (p + 1) / 4, p);
            var z = BigIntegers.Two;
            while (JacobiSymbol(z, p) != -1)
                ++z;
            var c = BigInteger.ModPow(z, q, p);
            var r = BigInteger.ModPow(n, (q + 1) / 2, p);
            var t = BigInteger.ModPow(n, q, p);
            var m = s;
            while (!t.IsOne)
            {
                int i = 0;
                var k = t;
                while (!k.IsOne)
                {
                    k = k * k % p;
                    ++i;
                }
                var b = BigInteger.ModPow(c, BigInteger.Pow(BigIntegers.Two, m - i - 1), p);
                r = r * b % p;
                var b2 = b * b % p;
                t = t * b2 % p;
                c = b2;
                m = i;
            }
            return r;
        }
    }
}
