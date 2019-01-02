using System.Collections.Generic;

//https://www.codewars.com/kata/the-observed-pin
namespace CODEWARS.Kata4_Pins
{
    public static class Kata
    {
        private static Dictionary<char, char[]> dict = new Dictionary<char, char[]>();

        static Kata()
        {
            dict.Add('0', new char[] { '0', '8' });
            dict.Add('1', new char[] { '1', '2', '4' });
            dict.Add('2', new char[] { '2', '1', '5', '3' });
            dict.Add('3', new char[] { '3', '2', '6' });
            dict.Add('4', new char[] { '4', '1', '5', '7' });
            dict.Add('5', new char[] { '5', '2', '4', '6', '8' });
            dict.Add('6', new char[] { '6', '3', '5', '9' });
            dict.Add('7', new char[] { '7', '4', '8' });
            dict.Add('8', new char[] { '8', '5', '7', '9', '0' });
            dict.Add('9', new char[] { '9', '6', '8' });
        }

        private static void AddPin(List<string> result, string pin, string org, int index)
        {
            if (index < org.Length)
            {
                foreach (char c in dict[org[index]])
                {
                    AddPin(result, pin + c, org, index + 1);
                }
            }
            else
            {
                result.Add(pin);
            }
        }

        public static List<string> GetPINs(string observed)
        {
            List<string> result = new List<string>();
            AddPin(result, string.Empty, observed, 0);
            return result;
        }
    }
}