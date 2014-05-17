using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KMeanCluster
{
    class ThreadContext
    {
        public float[][] data;
        public float[][] means;
        public int[] clustering;
        public int threadIndex;

        public ThreadContext(float[][] data, float[][] means, int[] clustering, int threadIndex)
        {
            this.data = data;
            this.means = means;
            this.clustering = clustering;
            this.threadIndex = threadIndex;
        }
    }
    class MutiThreadCalcDistance
    {
        private const int MAX_THREAD_NUM = 4;
        Thread[] doWorkThreads = null;
        ManualResetEvent[] manualResetEvents = null;
        public MutiThreadCalcDistance()
        {
            doWorkThreads = new Thread[MAX_THREAD_NUM];
            manualResetEvents = new ManualResetEvent[MAX_THREAD_NUM];
            for (int i = 0; i < MAX_THREAD_NUM; i++)
            {
                doWorkThreads[i] = new Thread(new ParameterizedThreadStart(CalcDistanceCallback));
                manualResetEvents[i] = new ManualResetEvent(false);
            }
        }

        public bool Calculate(float[][] data, float[][] means, int[] clustering)
        {
            int[] oldClustering = new int[clustering.Length];
            Array.Copy(clustering, oldClustering, clustering.Length);

            for (int i = 0; i < MAX_THREAD_NUM; i++)
            {
                ThreadContext context = new ThreadContext(data, means, clustering, i);
                doWorkThreads[i].IsBackground = true;
                doWorkThreads[i].Start(context);
            }

            WaitHandle.WaitAll(manualResetEvents);

            bool changed = false;
            for (int i = 0; i < clustering.Length; i++ )
            {
                if (clustering[i] != oldClustering[i])
                {
                    changed = true;
                    break;
                }
            }

            // check proposed clustering[] cluster counts
            int numClusters = means.Length;
            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < clustering.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // bad clustering. no change to clustering[][]

            return changed;
        }

        public void CalcDistanceCallback(object obj)
        {
            ThreadContext threadContext = (ThreadContext)obj;
            float[][] data = threadContext.data;
            float[][] means = threadContext.means;
            int[] clustering = threadContext.clustering;

            
            for (int i = threadContext.threadIndex; i < data.Length; i += MAX_THREAD_NUM)
            {
                float minDistance = float.MaxValue;
                int indexOfMin = 0;
                for(int j=0;j<means.Length;j++)
                {
                    float distance = Distance(data[i], means[j]);
                    if(distance < minDistance)
                    {
                        minDistance = distance;
                        indexOfMin = j;
                    }
                }
                clustering[i] = indexOfMin;
            }

            manualResetEvents[threadContext.threadIndex].Set();

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
    }
}
