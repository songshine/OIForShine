using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Features2D;

namespace FeatureExtract.SURF
{
    public class SURFExtract
    {
        #region Private Fields

        private SURFDetector surfDetector = null;

        #endregion

        #region Constructor

        public SURFExtract()
        {
            surfDetector = new SURFDetector(300, true);
        }

        #endregion


    }
}
