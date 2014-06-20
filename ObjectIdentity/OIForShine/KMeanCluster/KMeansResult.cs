using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMeanCluster
{
    [Serializable]
    public class KMeansResult
    {
        public int[] clustering;
        public float[][] means;
        public bool sucess;
    }
}
