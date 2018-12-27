using System.Collections.Generic;
using System.Text;

//https://www.codewars.com/kata/decimal-to-factorial-and-back
namespace CODEWARS.Kata5_Dec2Fact
{
    public class Dec2Fact
    {
        public static string dec2FactString(long nb)
        {
            StringBuilder builder = new StringBuilder();
            List<long> factList = new List<long>();

            long fact = 1;
            long count = 0;

            while (fact < nb)
            {
                factList.Add(fact);
                count++;
                fact *= count;
            }

            factList.Reverse();

            for (int i = 0; i < factList.Count; i++)
            {
                long num = nb / factList[i];

                if (num < 10)
                {
                    builder.Append(num);
                }
                else
                {
                    builder.Append((char)('A' + (num - 10)));
                }

                nb = nb % factList[i];
            }

            return builder.ToString();
        }

        public static long factString2Dec(string str)
        {
            long result = 0;
            List<long> factList = new List<long>();
            long fact = 1;
            long count = 0;

            while (count < str.Length)
            {
                factList.Add(fact);
                count++;
                fact *= count;
            }

            factList.Reverse();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < 'A')
                {
                    result += factList[i] * (str[i] - '0');
                }
                else
                {
                    result += factList[i] * (10 + (str[i] - 'A'));
                }
            }

            return result;
        }
    }
}