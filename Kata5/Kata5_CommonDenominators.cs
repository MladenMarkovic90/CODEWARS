using System;
using System.Collections.Generic;
using System.Linq;

//https://www.codewars.com/kata/common-denominators
namespace CODEWARS.Kata5_CommonDenominators
{
    public class CommonDenominators
    {
        private static long LCM(long a, long b) => a * b / KataNumberLib.GCD(a, b);

        public static string convertFrac(long[,] lst)
        {
            long factor = 1;
            List<Tuple<long, long>> list = new List<Tuple<long, long>>();

            for (int i = 0; i < lst.GetLength(0); i++)
            {
                list.Add(new Tuple<long, long>(lst[i, 0], lst[i, 1]));
            }

            foreach (Tuple<long, long> item in list)
            {
                factor = LCM(item.Item2, factor);
            }

            return string.Concat(list.Select(x => $"({x.Item1 * factor / x.Item2},{factor})"));
        }
    }
}