using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatureExtract
{
    public class SIFTParam
    {
        /// <summary>
        /// The number of octave layers. Use 3 for default
        /// </summary>
        public short octaveLayer = 3;

        /// <summary>
        /// Contrast threshold. Use 0.04 as default
        /// </summary>
        public double contrastThreshold = 0.04;

        /// <summary>
        /// Detector parameter. Use 10.0 as default
        /// </summary>
        public double edgeThreshold = 10.0;

        /// <summary>
        ///  Use 1.6 as default
        /// </summary>
        public double sigma = 1.6;

        public SIFTParam()
        {

        }

        public SIFTParam(short octaveLayer, double contrastThreshold, double edgeThreshold, double sigma)
        {
            this.octaveLayer = octaveLayer;
            this.contrastThreshold = contrastThreshold;
            this.edgeThreshold = edgeThreshold;
            this.sigma = sigma;
        }
    }
}
