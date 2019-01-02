using System;
using System.Collections.Generic;
using System.Linq;

namespace CODEWARS
{
    public static class Kata4
    {
        #region Finished

        //https://www.codewars.com/kata/sum-by-factors
        public static string sumOfDivided(int[] lst)
        {
            Dictionary<long, long> result = new Dictionary<long, long>();

            Func<int, int, int, int> func = new Func<int, int, int, int>((number, tmp, prime) =>
            {
                if (result.Keys.Contains(prime))
                {
                    result[prime] += number;
                }
                else
                {
                    result.Add(prime, number);
                }

                while (tmp % prime == 0)
                {
                    tmp /= prime;
                }

                return tmp;
            });

            foreach (int number in lst)
            {
                int prime = 2;
                int tmp = Math.Abs(number);
                if (tmp % prime == 0) tmp = func(number, tmp, prime);

                for (prime = 3; tmp >= prime; prime += 2)
                {
                    if (tmp % prime == 0) tmp = func(number, tmp, prime);
                }
            }

            return string.Join(string.Empty, result.OrderBy(x => x.Key).Select(x => $"({x.Key} {x.Value})"));
        }

        #endregion Finished
    }
}