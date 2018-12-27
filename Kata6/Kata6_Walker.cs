using System;
using System.Collections.Generic;

//https://www.codewars.com/kata/the-walker
namespace CODEWARS.Kata6_Walker
{
    public class Walker
    {
        public static int[] Solve(int a, int b, int c, int alpha, int beta, int gamma)
        {
            List<int> result = new List<int>();

            KeyValuePair<double, double> A = _A(0, a, alpha, 0, 0);
            KeyValuePair<double, double> B = _B(0, b, beta, A.Key, A.Value);
            KeyValuePair<double, double> C = _C(0, c, gamma, B.Key, B.Value);

            double length = Math.Sqrt(C.Value * C.Value + C.Key * C.Key);

            result.Add((int)Math.Round(length, 0));

            double angle = 90 + 90 * (Math.Asin(Math.Abs(C.Key) / length) / (Math.PI / 2));

            int degrees = (int)angle;
            int minutes = (int)((angle - degrees) * 60);
            int seconds = (int)((((angle - degrees) * 60) - minutes) * 60);

            result.Add(degrees);
            result.Add(minutes);
            result.Add(seconds);

            return result.ToArray();
        }

        public static KeyValuePair<double, double> _A(double piFactor, int length, int angle, double x, double y)
        {
            double resultX = 0;
            double resultY = 0;
            double xAngle = 90 - angle;
            double yAngle = angle;

            resultX = length * Math.Sin(Math.PI * (piFactor + (xAngle / 180)));
            resultY = length * Math.Sin(Math.PI * (piFactor + (yAngle / 180)));

            return new KeyValuePair<double, double>(resultX, resultY);
        }

        public static KeyValuePair<double, double> _B(double piFactor, int length, int angle, double x, double y)
        {
            double resultX = 0;
            double resultY = 0;
            double xAngle = angle;
            double yAngle = 90 - angle;

            resultX = length * Math.Sin(Math.PI * (piFactor + (xAngle / 180)));
            resultY = length * Math.Sin(Math.PI * (piFactor + (yAngle / 180)));

            return new KeyValuePair<double, double>(x - resultX, y + resultY);
        }

        public static KeyValuePair<double, double> _C(double piFactor, int length, int angle, double x, double y)
        {
            double resultX = 0;
            double resultY = 0;
            double xAngle = 90 - angle;
            double yAngle = angle;

            resultX = length * Math.Sin(Math.PI * (piFactor + (xAngle / 180)));
            resultY = length * Math.Sin(Math.PI * (piFactor + (yAngle / 180)));

            return new KeyValuePair<double, double>(x - resultX, y - resultY);
        }
    }
}