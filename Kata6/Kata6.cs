using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CODEWARS
{
    public class Kata6
    {
        //https://www.codewars.com/kata/roman-numerals-encoder
        public static string Solution(int n)
        {
            StringBuilder result = new StringBuilder();

            List<KeyValuePair<int, string>> list = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1000, "M"),
                new KeyValuePair<int, string>(900, "CM"),
                new KeyValuePair<int, string>(500, "D"),
                new KeyValuePair<int, string>(400, "CD"),
                new KeyValuePair<int, string>(100, "C"),
                new KeyValuePair<int, string>(90, "XC"),
                new KeyValuePair<int, string>(50, "L"),
                new KeyValuePair<int, string>(40, "XL"),
                new KeyValuePair<int, string>(10, "X"),
                new KeyValuePair<int, string>(9, "IX"),
                new KeyValuePair<int, string>(5, "V"),
                new KeyValuePair<int, string>(4, "IV"),
                new KeyValuePair<int, string>(1, "I"),
            };

            while (n > 0)
            {
                KeyValuePair<int, string> item = list.First(x => x.Key <= n);
                result.Append(item.Value);
                n -= item.Key;
            }

            return result.ToString();
        }

        //https://www.codewars.com/kata/mexican-wave
        public List<string> wave(string str)
        {
            List<string> result = new List<string> { };

            int index = 0;

            foreach (char c in str)
            {
                if (c != ' ')
                {
                    result.Add(str.Substring(0, index) + char.ToUpper(c) + str.Substring(index + 1));
                }

                index++;
            }

            return result;
        }

        /*
        //https://www.codewars.com/kata/decode-the-morse-code
        public static string Decode(string morseCode)
        {
            StringBuilder result = new StringBuilder();
        
            morseCode = morseCode?.Trim() ?? string.Empty;
        
            string[] words = morseCode.Split(new string[] { "   " }, StringSplitOptions.RemoveEmptyEntries);
        
            foreach (string word in words)
            {
                string[] codes = word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        
                foreach (string code in codes)
                {
                    result.Append(MorseCode.Get(code));
                }
        
                result.Append(" ");
            }
        
            return result.ToString().Trim();
        }
        */

        //https://www.codewars.com/kata/simple-fun-number-23-square-digits-sequence
        public int SquareDigitsSequence(int a0)
        {
            HashSet<int> set = new HashSet<int>();
            int count = 1;
            int next = a0;

            while (!set.Contains(next))
            {
                count++;

                set.Add(next);

                int tmp = next;
                next = 0;

                while (tmp != 0)
                {
                    int digit = tmp % 10;
                    next = next + digit * digit;
                    tmp /= 10;
                }
            }

            return count;
        }

        //https://www.codewars.com/kata/help-the-bookseller
        public static string stockSummary(string[] lstOfArt, string[] lstOf1stLetter)
        {
            if (lstOfArt.Length == 0 || lstOf1stLetter.Length == 0)
            {
                return string.Empty;
            }

            List<string> list = new List<string>();

            foreach (string letter in lstOf1stLetter)
            {
                int count = 0;

                foreach (string item in lstOfArt)
                {
                    if (item[0] == letter[0])
                    {
                        count += int.Parse(item.Substring(item.LastIndexOf(' ')));
                    }
                }

                list.Add($"({letter} : {count})");
            }

            return string.Join(" - ", list.ToArray());
        }

        //https://www.codewars.com/kata/moduli-number-system
        public static string fromNb2Str(int n, int[] sys)
        {
            List<long> primeNumbers = KataNumberLib.GetPrimeNumbers(sys.Max());

            foreach (int num in sys)
            {
                bool check = false;

                foreach (long prime in primeNumbers)
                {
                    if (num > prime && num % prime == 0)
                    {
                        if (check)
                        {
                            return "Not applicable";
                        }

                        check = true;
                    }
                }
            }

            StringBuilder builder = new StringBuilder();
            int mul = 1;

            foreach (int num in sys)
            {
                foreach (int num2 in sys)
                {
                    if (num > num2 && num % num2 == 0)
                    {
                        return "Not applicable";
                    }
                }

                mul *= num;
                builder.Append($"-{n % num}-");
            }

            if (mul <= n)
            {
                return "Not applicable";
            }

            return builder.ToString();
        }

        //https://www.codewars.com/kata/ease-the-stockbroker
        public static string balanceStatements(string lst)
        {
            List<string> badOrders = new List<string>();
            double buy = 0;
            double sell = 0;

            string[] orders = lst.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string order in orders)
            {
                string trimmedOrder = order.Trim();

                string[] args = trimmedOrder.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (args.Length != 4)
                {
                    badOrders.Add(trimmedOrder);
                    continue;
                }

                if (args[1].Contains('.') || !int.TryParse(args[1], out int quantity))
                {
                    badOrders.Add(trimmedOrder);
                    continue;
                }

                if (!args[2].Contains('.') || !double.TryParse(args[2], out double price))
                {
                    badOrders.Add(trimmedOrder);
                    continue;
                }

                string priceType = args[3];

                if (priceType == "S")
                {
                    sell += price * quantity;
                }
                else if (priceType == "B")
                {
                    buy += price * quantity;
                }
                else
                {
                    badOrders.Add(trimmedOrder);
                }
            }

            return $"Buy: {buy} Sell: {sell}" + (badOrders.Count > 0 ? $"; Badly formed {badOrders.Count}: {string.Join(" ;", badOrders)} ;" : string.Empty);
        }

        //https://www.codewars.com/kata/duplicate-encoder
        public static string DuplicateEncode(string word)
        {
            StringBuilder result = new StringBuilder(word.ToLower());

            int lcount = word.Count(x => x == '(');
            int dcount = word.Count(x => x == ')');
            int dindex = -1;

            if (dcount == 1)
            {
                dindex = word.IndexOf(')');
            }

            if (lcount > 1)
            {
                result = result.Replace('(', ')');
            }

            if (dindex >= 0)
            {
                result[dindex] = '(';
            }

            while (true)
            {
                string current = result.ToString();
                char change = current.FirstOrDefault(x => x != '(' && x != ')');

                if (change != '\0')
                {
                    int count = current.Count(x => x == change);

                    if (count == 1)
                    {
                        result = result.Replace(change, '(');
                    }
                    else
                    {
                        result = result.Replace(change, ')');
                    }
                }
                else
                {
                    return result.ToString();
                }
            }
        }

        //https://www.codewars.com/kata/make-the-deadfish-swim
        public static int[] Parse(string data)
        {
            List<int> result = new List<int>();
            int currentValue = 0;

            foreach (char c in data)
            {
                switch (c)
                {
                    case 'i':
                        currentValue++;
                        break;
                    case 'd':
                        currentValue--;
                        break;
                    case 's':
                        currentValue *= currentValue;
                        break;
                    case 'o':
                        result.Add(currentValue);
                        break;
                }
            }

            return result.ToArray();
        }

        //https://www.codewars.com/kata/are-they-the-same
        public static bool comp(int[] a, int[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            int[] oa = a.OrderBy(x => x).ToArray();
            int[] ob = b.OrderBy(x => x).ToArray();

            for (int i = 0; i < oa.Length; i++)
            {
                if (oa[i] * oa[i] != ob[i])
                {
                    return false;
                }
            }

            return true;
        }

        //https://www.codewars.com/kata/which-are-in
        public static string[] inArray(string[] array1, string[] array2)
        {
            List<string> result = new List<string>();

            foreach (string item in array1)
            {
                if (array2.Any(x => x.Contains(item)))
                {
                    result.Add(item);
                }
            }

            result.Sort();
            return result.ToArray();
        }

        //https://www.codewars.com/kata/persistent-bugger
        public static int Persistence(long n)
        {
            int count = 0;

            while (n > 9)
            {
                long tmp = 1;

                while (n > 0)
                {
                    tmp *= n % 10;
                    n = n / 10;
                }

                n = tmp;
                count++;
            }

            return count;
        }

        //https://www.codewars.com/kata/positions-average
        public static double PosAverage(string s)
        {
            int count = 0;
            string[] arrays = s.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (arrays.Length == 0)
            {
                return -1;
            }

            for (int i = 0; i < arrays.Length; i++)
            {
                for (int j = i + 1; j < arrays.Length; j++)
                {
                    for (int c = 0; c < arrays[i].Length; c++)
                    {
                        if (arrays[i][c] == arrays[j][c])
                        {
                            count++;
                        }
                    }
                }
            }

            int max = arrays[0].Length * (arrays.Length * (arrays.Length - 1)) / 2;

            return ((double)(count * 100) / max);
        }

        //https://www.codewars.com/kata/backwards-read-primes
        public static string backwardsPrime(long start, long end)
        {
            List<string> result = new List<string>();
            List<long> primes = KataNumberLib.GetPrimeNumbers(start, end).Where(x => !KataNumberLib.IsPalindrome(x.ToString())).ToList();

            foreach (long prime in primes.Select(x => long.Parse(new string(x.ToString().Reverse().ToArray()))))
            {
                if (KataNumberLib.IsPrimeNumber(prime))
                {
                    result.Add(new string(prime.ToString().Reverse().ToArray()));
                }
            }

            return string.Join(" ", result);
        }

        //https://www.codewars.com/kata/rectangle-into-squares
        public static List<int> sqInRect(int lng, int wdth)
        {
            if (lng == wdth)
            {
                return null;
            }

            List<int> result = new List<int>();

            while (lng > 0 && wdth > 0)
            {
                int maxSquare = Math.Min(lng, wdth);
                result.Add(maxSquare);

                if (lng > maxSquare)
                {
                    lng -= maxSquare;
                }
                else
                {
                    wdth -= maxSquare;
                }
            }

            return result;
        }

        //https://www.codewars.com/kata/reverse-or-rotate
        public static string RevRot(string strng, int sz)
        {
            StringBuilder builder = new StringBuilder();

            if (sz > 0 && !string.IsNullOrWhiteSpace(strng))
            {
                while (strng.Length >= sz)
                {
                    int sum = 0;

                    string item = strng.Substring(0, sz);
                    strng = strng.Remove(0, sz);

                    foreach (char c in item)
                    {
                        sum += (c - '0') * (c - '0') * (c - '0');
                    }

                    if (sum % 2 == 0)
                    {
                        builder.Append(item.Reverse().ToArray());
                    }
                    else
                    {
                        builder.Append(item.Remove(0, 1));
                        builder.Append(item.First());
                    }
                }
            }

            return builder.ToString();
        }
    }
}