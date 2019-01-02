using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODEWARS
{
    public static class Kata5
    {
        #region Finished

        //https://www.codewars.com/kata/rot13-1
        public static string Rot13(string message)
        {
            char[] lowerChars = KataStringLib.LowerASCIILetters();
            char[] upperChars = KataStringLib.UpperASCIILetters();

            StringBuilder result = new StringBuilder();
            int moveChar = 13;

            foreach (char c in message)
            {
                if (char.IsLetter(c))
                {
                    if (char.IsLower(c))
                    {
                        result.Append(lowerChars[(c - 'a' + moveChar) % 26]);
                    }
                    else
                    {
                        result.Append(upperChars[(c - 'A' + moveChar) % 26]);
                    }
                }
                else
                {
                    result.Append(c);
                }
            }

            return result.ToString();
        }

        //https://www.codewars.com/kata/directions-reduction
        public static string[] dirReduc(string[] arr)
        {
            Stack<string> stack = new Stack<string>();

            foreach (string item in arr)
            {
                if (stack.Count == 0)
                {
                    stack.Push(item);
                }
                else
                {
                    string last = stack.Peek();

                    if (last == "NORTH" && item == "SOUTH" ||
                        last == "SOUTH" && item == "NORTH" ||
                        last == "EAST" && item == "WEST" ||
                        last == "WEST" && item == "EAST")
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(item);
                    }
                }
            }

            return stack.Reverse().ToArray();
        }

        //https://www.codewars.com/kata/help-your-granny
        public static int tour(string[] arrFriends, string[][] ftwns, Hashtable h)
        {
            double result = 0;
            double prevDistance = 0;

            foreach (string friend in arrFriends)
            {
                string[] friendAndTown = ftwns.FirstOrDefault(x => x[0] == friend);
                double townDistance = 0;

                if (friendAndTown != null && friendAndTown.Length == 2)
                {
                    string town = friendAndTown[1];

                    if (!string.IsNullOrWhiteSpace(town))
                    {
                        townDistance = (double)h[town];
                    }
                }

                if (townDistance > 0)
                {
                    if (prevDistance == 0)
                    {
                        result += townDistance;
                    }
                    else
                    {
                        result += Math.Sqrt(townDistance * townDistance - prevDistance * prevDistance);
                    }

                    prevDistance = townDistance;
                }
            }

            if (prevDistance > 0)
            {
                result += prevDistance;
            }

            return (int)result;
        }

        //https://www.codewars.com/kata/primes-in-numbers
        public static string factors(int lst)
        {
            StringBuilder builder = new StringBuilder();
            List<long> list = KataNumberLib.GetPrimeNumbers((long)Math.Sqrt(lst) + 1).ToList();

            foreach (int prime in list)
            {
                if (lst == 1)
                {
                    break;
                }

                int count = 0;

                while (lst % prime == 0)
                {
                    count++;
                    lst = lst / prime;
                }

                if (count == 1)
                {
                    builder.Append($"({prime})");
                }
                else if (count > 1)
                {
                    builder.Append($"({prime}**{count})");
                }
            }

            if (lst > 1)
            {
                builder.Append($"({lst})");
            }

            return builder.ToString();
        }

        //https://www.codewars.com/kata/weight-for-weight
        public static string orderWeight(string strng)
        {
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();

            foreach (string item in strng.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result.Add(new Tuple<string, int>(item, KataStringLib.SumDigits(item)));
            }

            return string.Join(" ", result.OrderBy(x => x.Item2).ThenBy(x => x.Item1).Select(x => x.Item1));
        }

        //https://www.codewars.com/kata/first-non-repeating-character
        public static string FirstNonRepeatingLetter(string s)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();

            foreach (char c in s)
            {
                if (result.Keys.Contains(char.ToUpper(c)))
                {
                    result[char.ToUpper(c)]++;
                }
                else if (result.Keys.Contains(char.ToLower(c)))
                {
                    result[char.ToLower(c)]++;
                }
                else
                {
                    result.Add(c, 0);
                }
            }

            if (result.Any(x => x.Value == 0))
            {
                return result.First(x => x.Value == 0).Key.ToString();
            }

            return string.Empty;
        }

        //https://www.codewars.com/kata/closest-and-smallest
        public static int[][] Closest(string strng)
        {
            if (string.IsNullOrEmpty(strng))
            {
                return new int[0][];
            }

            List<Tuple<string, int, int>> list = new List<Tuple<string, int, int>>();

            int index = 0;
            foreach (string item in strng.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                list.Add(new Tuple<string, int, int>(item, KataStringLib.SumDigits(item), index++));
            }

            List<Tuple<string, int, int>> oList = list.OrderBy(x => x.Item2).ThenBy(x => x.Item3).ToList();

            int index1 = 0;
            int index2 = 1;
            int minDiff = int.MaxValue;

            for (int i = 1; i < oList.Count; i++)
            {
                int diff = oList[i].Item2 - oList[i - 1].Item2;
                
                if (diff < minDiff)
                {
                    index1 = i - 1;
                    index2 = i;
                    minDiff = diff;
                }
            }

            int[][] result = new int[2][];

            result[0] = new int[3]
            {
                oList[index1].Item2,
                oList[index1].Item3,
                int.Parse(oList[index1].Item1)
            };

            result[1] = new int[3]
            {
                oList[index2].Item2,
                oList[index2].Item3,
                int.Parse(oList[index2].Item1)
            };

            return result;
        }

        //https://www.codewars.com/kata/eulers-method-for-a-first-order-ode
        public static double ExEuler(int nb)
        {
            double sumA = 0;
            double xk = 0;
            double yk = 1;
            double zk = 1;
            double h = 1.0 / nb;

            for (int n = 1; n <= nb; n++)
            {
                double dydx = 2 - Math.Exp(-4 * xk) - 2 * yk;
                xk = (double)n / nb;
                yk = yk + dydx * h;
                zk = 1 + (0.5 * Math.Exp(-4 * xk)) - (0.5 * Math.Exp(-2 * xk));
                sumA += Math.Abs(yk - zk) / zk;
            }

            sumA = sumA / (nb + 1);
            double part1 = Math.Truncate(sumA);

            return part1 + Math.Truncate((sumA - part1) * 1_000_000) / 1_000_000;
        }

        #endregion Finished
    }
}