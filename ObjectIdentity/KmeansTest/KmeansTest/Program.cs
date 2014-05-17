using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KmeansTest
{
    class Program
    {
        static void Main(string[] args)
        {
            float[][] rawData = new float[10][];
            rawData[0] = new float[] { 5.0f, 5.0f };
            rawData[1] = new float[] { 5.5f, 5.0f };
            rawData[2] = new float[] { 4.5f, 5.5f };
            rawData[3] = new float[] { 4.5f, 5.0f };
            rawData[4] = new float[] { 20.0f, 20.0f };
            rawData[5] = new float[] { 19.5f, 20.0f };
            rawData[6] = new float[] { 19.5f, 19.5f };
            rawData[7] = new float[] { 20.5f, 20.5f };

            rawData[8] = new float[] { 50.0f, 50.0f };
            rawData[9] = new float[] { 60.0f, 60.0f };

            Console.WriteLine("Raw unclustered data:\n");
            Console.WriteLine("    Height Weight");
            Console.WriteLine("-------------------");
            ShowData(rawData, 1, true, true);

            int numClusters = 3;
            Console.WriteLine("\nSetting numClusters to " + numClusters);

            int[] clustering = null;
            float[][] means = KMeans.Cluster(rawData, numClusters,out clustering , true); // this is it

            Console.WriteLine("\nK-means clustering complete\n");

            Console.WriteLine("Final clustering in internal form:\n");
            ShowVector(clustering,true);

            //Console.WriteLine("Final means \n");
            //ShowData(means, 1, true, true);

            //Console.WriteLine("Raw data by cluster:\n");
            //ShowClustered(rawData, clustering, numClusters, 1);

            Console.WriteLine("\nEnd k-means clustering demo\n");
            Console.ReadLine();
        }

        // ============================================================================

        // misc display helpers for demo

        static void ShowData(float[][] data, int decimals, bool indices, bool newLine)
        {
            for (int i = 0; i < data.Length; ++i)
            {
                if (indices) Console.Write(i.ToString().PadLeft(3) + " ");
                for (int j = 0; j < data[i].Length; ++j)
                {
                    if (data[i][j] >= 0.0) Console.Write(" ");
                    Console.Write(data[i][j].ToString("F" + decimals) + " ");
                }
                Console.WriteLine("");
            }
            if (newLine) Console.WriteLine("");
        } // ShowData

        static void ShowVector(int[] vector, bool newLine)
        {
            for (int i = 0; i < vector.Length; ++i)
                Console.Write(vector[i] + " ");
            if (newLine) Console.WriteLine("\n");
        }

        public static void ShowClustered(float[][] data, int[] clustering, int numClusters, int decimals)
        {
            for (int k = 0; k < numClusters; ++k)
            {
                Console.WriteLine("===================");
                for (int i = 0; i < data.Length; ++i)
                {
                    int clusterID = clustering[i];
                    if (clusterID != k) continue;
                    Console.Write(i.ToString().PadLeft(3) + " ");
                    for (int j = 0; j < data[i].Length; ++j)
                    {
                        if (data[i][j] >= 0.0) Console.Write(" ");
                        Console.Write(data[i][j].ToString("F" + decimals) + " ");
                    }
                    Console.WriteLine("");
                }
                Console.WriteLine("===================");
            } // k
        }
    }
}
