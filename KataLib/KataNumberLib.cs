using System;
using System.Collections.Generic;

namespace CODEWARS
{
    public static class KataNumberLib
    {
        public static long GCD(long a, long b) => b == 0 ? a : GCD(b, a % b);
        public static int CountDigits(long n) => n.ToString().Length;

        public static ulong Factorial(int value)
        {
            ulong result = 1;

            while (value > 1)
            {
                result *= (uint)value;
                value--;
            }

            return result;
        }

        public static bool IsPrimeNumber(long num)
        {
            if (num % 2 == 0)
            {
                return false;
            }

            long max = (long)Math.Sqrt(num) + 1;

            for (long i = 3; i <= max; i += 2)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static List<long> GetPrimeNumbers(long start, long end)
        {
            List<long> primeNumbers = new List<long>();

            for (long num = start; num <= end; num++)
            {
                if (IsPrimeNumber(num))
                {
                    primeNumbers.Add(num);
                }
            }

            return primeNumbers;
        }
        
        public static List<long> GetPrimeNumbers(long max)
        {
            List<long> primeNumbers = new List<long>();

            if (max >= 2)
            {
                primeNumbers.Add(2);
            }

            for (long i = 3; i <= max; i += 2)
            {
                bool isPrimeNumber = true;
                long check = (long)Math.Sqrt(i) + 1;

                foreach (int num in primeNumbers)
                {
                    if (num > check)
                    {
                        break;
                    }

                    if (i % num == 0)
                    {
                        isPrimeNumber = false;
                        break;
                    }
                }

                if (isPrimeNumber)
                {
                    primeNumbers.Add(i);
                }
            }

            return primeNumbers;
        }

        public static bool IsPalindrome(string text)
        {
            for (int i = 0, j = text.Length - 1; i <= j; i++, j--)
            {
                if (text[i] != text[j])
                {
                    return false;
                }
            }

            return true;
        }
    }
}