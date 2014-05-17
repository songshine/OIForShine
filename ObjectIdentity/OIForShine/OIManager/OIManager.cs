using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Classify;
using FeatureExtract;
using KMeanCluster;

namespace OIManager
{
    public class OIManager
    {
        private int clusterNumber = 100;

        private List<Tuple<string, float[]>> trainDataHistList =
              new List<Tuple<string, float[]>>();

        KMeansResult kmeansResult = null;

        public OIManager()
        {
        }

        public OIManager(int clusterNum)
        {
            this.clusterNumber = clusterNum;
        }
        public static void OptimalTest(List<Tuple<string, string>> trianImagePathList,
            List<Tuple<string, string>> predictImagePathList,
            int minK,
            int maxK,
            IOutputLog outputLog)
        {
            List<Tuple<string, float[][]>> trainFeaturesList =
              new List<Tuple<string, float[][]>>();

            List<Tuple<string, float[][]>> predictFeatureList = 
                new List<Tuple<string,float[][]>>();

            #region Extract features
            outputLog.OutputLogInfo("Extracting features starts...");
            SIFTExtract siftExtract = new SIFTExtract();
            foreach (var item in trianImagePathList)
            {
                Bitmap bitmap = new Bitmap(item.Item2);
                trainFeaturesList.Add(new Tuple<string, float[][]>(item.Item1, siftExtract.Extract(bitmap)));
            }
            
            foreach(var item in predictImagePathList)
            {
                Bitmap bitmap = new Bitmap(item.Item2);
                predictFeatureList.Add(new Tuple<string, float[][]>(item.Item1, siftExtract.Extract(bitmap)));
            }
            outputLog.OutputLogInfo("Extracting features finshed...");
            #endregion


            #region Finds optimal K value
            int optmialK = 0;
            int maxCorrect = int.MinValue;
            #endregion
            float[][] clusterData = MergeAllFeatures(trainFeaturesList);
            
            for (int k = minK; k <= maxK;k++ )
            {
                #region Cluster data

                outputLog.OutputLogInfo(string.Format("---Cluster data: K = {0}---------", k));
                KMeansResult kmeansResult = KMeans.Cluster(clusterData, k, true);
                if (kmeansResult.sucess == false)
                {
                    throw new ArgumentException("Cluster failer, please try again!");
                }
                #endregion

                #region Histogram

                List<Tuple<string, float[]>> trainFeatureHistList = new List<Tuple<string, float[]>>();
                foreach(var item in trainFeaturesList)
                {
                    trainFeatureHistList.Add(new Tuple<string, float[]>(item.Item1, Histogram.Histogram.Calculate(item.Item2, kmeansResult.means, true, true)));
                }
                #endregion

                #region Predict
                int sucess = 0;
                foreach (var item in predictFeatureList)
                {
                    float[] siftHist = Histogram.Histogram.Calculate(item.Item2, kmeansResult.means, true, true);
                    if (item.Item1 == Classifer.AngleOffset(siftHist, trainFeatureHistList))
                    {
                        sucess++;
                    }
                }
                if (maxCorrect < sucess)
                {
                    optmialK = k;
                    maxCorrect = sucess;
                }
                outputLog.OutputLogInfo(string.Format("Total correct prediction is {0}, accuracy {1}%", sucess,(float)sucess/predictFeatureList.Count*100));
                #endregion
            }
            outputLog.OutputLogInfo(string.Format("----The optmial K value is {0} , the accuracy is {1}%---\r\n",optmialK, (float)maxCorrect/predictFeatureList.Count*100));

        }
        public void Train(List<Tuple<string, Bitmap>> trainBitmapList)
        {
            List<Tuple<string, float[][]>> featuresDicList =
                new List<Tuple<string, float[][]>>();         

            #region Extract features

            SIFTExtract siftExtract = new SIFTExtract();
            foreach (var item in trainBitmapList)
            {
                featuresDicList.Add(new Tuple<string,float[][]>(item.Item1, siftExtract.Extract(item.Item2)));
            }

            #endregion

            #region Cluster data

            float[][] clusterData = MergeAllFeatures(featuresDicList);
            kmeansResult = KMeans.Cluster(clusterData, clusterNumber, true);
            if (kmeansResult.sucess == false)
            {
                throw new ArgumentException("Cluster failer, please try again!");
            }

            #endregion

            #region Calculate Histogram

            foreach (var item in featuresDicList)
            {
                trainDataHistList.Add(new Tuple<string, float[]>(item.Item1, Histogram.Histogram.Calculate(item.Item2, kmeansResult.means, true, true)));
            }

            #endregion


        }

        public void Train(List<Tuple<string, string>> trainImagePathList)
        {
            List<Tuple<string, float[][]>> featuresDicList =
                new List<Tuple<string, float[][]>>();


            #region Extract features

            SIFTExtract siftExtract = new SIFTExtract();
            foreach (var item in trainImagePathList)
            {
                Bitmap bitmap = new Bitmap(item.Item2);
                featuresDicList.Add(new Tuple<string,float[][]>(item.Item1,siftExtract.Extract(bitmap)));
            }

            #endregion

            #region Cluster data

            float[][] clusterData = MergeAllFeatures(featuresDicList);
            kmeansResult = KMeans.Cluster(clusterData, clusterNumber, true);
            if (kmeansResult.sucess == false)
            {
                throw new ArgumentException("Cluster failer, please try again!");
            }

            #endregion

            #region Calculate Histogram

            foreach (var item in featuresDicList)
            {
                trainDataHistList.Add(new Tuple<string,float[]>(item.Item1, Histogram.Histogram.Calculate(item.Item2, kmeansResult.means, true, true)));
            }

            #endregion
        }

        public string Predict(Bitmap bitmap)
        {
            // TO DO: Implements classifier.
            //string resule;
            float[][] siftDescriptor = SIFTExtract.Extract(bitmap, null);
            float[] siftHist = Histogram.Histogram.Calculate(siftDescriptor, kmeansResult.means, true, true);
            return Classifer.AngleOffset(siftHist, trainDataHistList);

        }

        public string Predict(byte[] bitmapBytes)
        {
            // TO DO: Implements classifier.
            //string resule;
            float[][] siftDescriptor = SIFTExtract.Extract(bitmapBytes, null);
            float[] siftHist = Histogram.Histogram.Calculate(siftDescriptor, kmeansResult.means, true, true);
            return Classifer.AngleOffset(siftHist, trainDataHistList);

        }


        #region Private Methods

        private static float[][] MergeAllFeatures(List<Tuple<string, float[][]>> featuresDicList)
        {

            int featuresAccount = featuresDicList
                        .Select(item => item.Item2)
                        .Sum(item => item.Length);

            float[][] result = new float[featuresAccount][];

            int cursorLength = 0;
            foreach (var item in featuresDicList)
            {
                for (int i = 0; i < item.Item2.Length; i++)
                {
                    result[cursorLength + i] = new float[item.Item2[i].Length];
                    Array.Copy(item.Item2[i], result[cursorLength + i], item.Item2[i].Length);
                    
                }
                cursorLength += item.Item2.Length;
            }

            return result;
        }

        #endregion

    }
}