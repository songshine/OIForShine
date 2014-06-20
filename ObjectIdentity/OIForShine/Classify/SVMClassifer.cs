using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using SVM;

namespace Classify
{
    public class SVMClassifer
    {
        Dictionary<double, string> type2IndexDic = null;
        Model trainModel = null;
        Parameter parameters = null;

        public SVMClassifer()
        {
            type2IndexDic = new Dictionary<double, string>();
            parameters = new Parameter();
        }

        public void Train(List<Tuple<string, float[]>> trainDataHistList)
        {
            double currLabel = 0.0;
            int numOfVector = trainDataHistList.Count;
            double[] y = new double[numOfVector];
            Node[][] x = new Node[numOfVector][];
            int maxIndex = 0;
            for (int i=0;i<numOfVector;i++)
            {
                Tuple<string, float[]> item = trainDataHistList[i];
                if (!type2IndexDic.ContainsValue(item.Item1))
                {
                    type2IndexDic[currLabel] = item.Item1;
                    currLabel += 1.0;
                }
                y[i] = type2IndexDic.FirstOrDefault(it=>it.Value == item.Item1).Key;
                x[i] = FloatToNode(item.Item2);
                maxIndex = Math.Max(maxIndex, item.Item2.Length-1);
            }

            Problem train = new Problem(numOfVector, y,x, maxIndex);
            double C, Gamma;
            ParameterSelection.Grid(train, parameters, null, out C, out Gamma);
            parameters.C = C;
            parameters.Gamma = Gamma;
            parameters.KernelType = KernelType.RBF;

            //BinaryFormatter formatter = new BinaryFormatter();
            //Stream stream = File.Open("Train.model", FileMode.Open);
            //trainModel = (Model)formatter.Deserialize(stream);
            //stream.Close();

            trainModel = Training.Train(train, parameters);

            OutputModel("Train.model");
        }

        public string Predict(float[] inputData)
        {
            string result = string.Empty;
            if (trainModel != null)
            {
                Node[] x = FloatToNode(inputData);
                double label = Prediction.Predict(trainModel, x);
                result = type2IndexDic[label];

            }
            return result;
        }

        public void OutputModel(string fileName)
        {
            Stream stream = File.Open(fileName, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, trainModel);
            stream.Close();

        }

        private Node[] FloatToNode(float[] data)
        {
            Node[] nodes = new Node[data.Length];
            for (int i = 0; i < data.Length;i++ )
            {
                nodes[i] = new Node(i, data[i]);
            }
            return nodes;
        }

    }
}
