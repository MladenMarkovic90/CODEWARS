using System.Collections.Generic;
using System.Linq;

namespace CODEWARS
{
    public static class KataStringLib
    {
        #region Public

        public static char[] GetLowerVowels() => new char[] { 'a', 'e', 'i', 'o', 'u' };
        public static char[] LowerASCIILetters() => ASCIIChars('a', 26);
        public static char[] UpperASCIILetters() => ASCIIChars('A', 26);
        public static int SumDigits(string number) => number.Select(x => x - '0').Sum();

        public static Dictionary<char, int> CountChars(string text, bool caseSensitive)
        {
            Dictionary<char, int> result = new Dictionary<char, int>();

            foreach (char item in text ?? string.Empty)
            {
                char c = caseSensitive ? item : char.ToLower(item);

                if (result.ContainsKey(c))
                {
                    result[c]++;
                }
                else
                {
                    result.Add(c, 1);
                }
            }

            return result;
        }

        #endregion Public

        #region Private

        private static char[] ASCIIChars(char first, int count)
        {
            char[] result = new char[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = first++;
            }

            return result;
        }

        #endregion Private
    }
}