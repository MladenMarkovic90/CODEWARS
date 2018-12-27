namespace CODEWARS
{
    public static class KataStringLib
    {
        public static char[] LowerASCIILetters() => ASCIIChars('a', 26);
        public static char[] UpperASCIILetters() => ASCIIChars('A', 26);

        private static char[] ASCIIChars(char first, int count)
        {
            char[] result = new char[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = first++;
            }

            return result;
        }
    }
}