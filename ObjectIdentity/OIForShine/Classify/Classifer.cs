using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classify
{
    public class Classifer
    {
        public static string AngleOffset(float[] inputData, List<Tuple<string, float[]>> trainDataHistList)
        {
            double minAngleOffsert = double.MaxValue;
            string result = string.Empty;

            foreach (var item in trainDataHistList)
            {
                double angelOffset = VectorAngle(inputData, item.Item2);
                if (angelOffset < minAngleOffsert)
                {
                    minAngleOffsert = angelOffset;
                    result = item.Item1;
                }
            }

            return result;
        }

        private static double VectorAngle(float[] p1, float[] p2)
        {
            // To calculate angle between p1 and p2.
            double p12 = 0.0f, p22 = 0.0f, dotM = 0.0f;
            for (int i = 0; i < p1.Length; i++)
            {
                p12 += p1[i] * p1[i];
                p22 += p2[i] * p2[i];
                dotM += p1[i] * p2[i];
            }

            return Math.Acos(dotM/(Math.Sqrt(p12)*Math.Sqrt(p22)));
        }
    }
}