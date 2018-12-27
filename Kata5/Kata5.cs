using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODEWARS
{
    public static class Kata5
    {
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
    }
}