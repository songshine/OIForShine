using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Histogram
{
    public class Histogram
    {

        /// <summary>
        /// Compute Histogram.
        /// </summary>
        /// <param name="inputData">Raw data which needs to be compute histogram.</param>
        /// <param name="means">Kemans out put</param>
        /// <param name="isInputNormalized">Need input data to be normalized?</param>
        /// <param name="isOutPutNormalized">Need output data to be normalized?</param>
        /// <returns></returns>
        public static float[] Calculate(float[][] inputData, float[][] means, bool isInputNormalized = true, bool isOutPutNormalized = true)
        {
            #region Checks Paramters

            if (inputData == null || means == null)
            {
                throw new ArgumentNullException("Input data and menas all can not be null.");
            }
            if (inputData.Length == 0 || means.Length == 0)
            {
                throw new ArgumentException("Input data and menas all can not empty.");
            }
            if (inputData[0].Length != means[0].Length)
            {
                throw new ArgumentException("Input data dimension is different froom menas");
            }
            #endregion

            float[][] data = isInputNormalized ? Normalized(inputData) : inputData;
            float[] histogram = new float[means.Length];
            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0.0f;
            }
            for (int i = 0; i < data.Length; i++)
            {
                float minDistance = float.MaxValue;
                int indexCluster = 0;
                for (int j = 0; j < means.Length; j++)
                {
                    float distance = Distance(data[i], means[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        indexCluster = j;
                    }
                }

                histogram[indexCluster] += 1.0f; ;
            }
             
            if (isOutPutNormalized)
            {
                for (int i = 0; i < histogram.Length; i++)
                {
                    histogram[i] /= inputData.Length;
                }
            }
            return histogram;


        }

        private static float Distance(float[] tuple, float[] mean)
        {
            // Euclidean distance between two vectors for UpdateClustering()
            // consider alternatives such as Manhattan distance
            float sumSquaredDiffs = 0.0f;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += (float)Math.Pow((tuple[j] - mean[j]), 2);
            return (float)Math.Sqrt(sumSquaredDiffs);
        }

        private static float[][] Normalized(float[][] rawData)
        {
            // normalize raw data by computing (x - mean) / stddev
            // primary alternative is min-max:
            // v' = (v - min) / (max - min)

            // make a copy of input data
            float[][] result = new float[rawData.Length][];
            for (int i = 0; i < rawData.Length; ++i)
            {
                result[i] = new float[rawData[i].Length];
                Array.Copy(rawData[i], result[i], rawData[i].Length);
            }

            for (int j = 0; j < result[0].Length; ++j) // each col
            {
                float mean = 0.0f;
                for (int i = 0; i < result.Length; ++i)
                    mean += (result[i][j] / result.Length);
                //float mean = colSum / result.Length;
                float sd = 0.0f;
                for (int i = 0; i < result.Length; ++i)
                    sd += ((result[i][j] - mean) * (result[i][j] - mean) / result.Length);
                sd = (float)Math.Sqrt(sd + 1);
                //float sd = sum / result.Length;
                for (int i = 0; i < result.Length; ++i)
                    result[i][j] = (result[i][j] - mean) / sd;
            }
            return result;
        }
    }
}
