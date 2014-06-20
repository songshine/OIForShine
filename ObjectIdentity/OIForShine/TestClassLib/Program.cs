using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KMeanCluster;

namespace TestClassLib
{
    class Program
    {
        static void Main(string[] args)
        {
            float[][] data = new float[10][];
            data[0] = new float[2]{2.0f, 2.0f};
            data[1] = new float[2] { 3.0f, 3.0f };
            data[2] = new float[2] { 1.0f, 1.0f };
            data[3] = new float[2] { 1.0f, 2.0f };

            data[4] = new float[2] { 12.0f, 12.0f };
            data[5] = new float[2] { 13.0f, 13.0f };
            data[6] = new float[2] { 11.0f, 11.0f };
            data[7] = new float[2] { 11.0f, 12.0f };

            data[8] = new float[2] { 20.0f, 20.0f };
            data[9] = new float[2] { 111.0f, 112.0f };

            KMeansResult result = KMeans.Cluster2(data, 3, true);
            for (int i = 0; i < result.clustering.Length; i++)
            {
                Console.Write(result.clustering[i].ToString() + " ");
            }
            Console.WriteLine();
            float[] hist = Histogram.Histogram.Calculate(data, result.means, true, true);
            for (int i = 0; i < hist.Length; i++)
            {
                Console.Write(hist[i] + " ");
            }
            Console.ReadKey();
        }
    }
}
