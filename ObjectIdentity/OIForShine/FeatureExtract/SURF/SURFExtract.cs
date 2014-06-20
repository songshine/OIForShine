using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

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
            surfDetector = new SURFDetector(1000, false);
        }

        #endregion

        #region Public Methods

        public float[][] Extract(byte[] imageBytes)
        {
            //Converts bgr into gray.
            Image<Gray, byte> image = null;
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                Bitmap bmpImage = new Bitmap(ms);
                image = new Image<Gray, byte>(bmpImage);
            }
            //TODO: Smooth.
            /////////////////////////////////////
            image.SmoothMedian(5);


            //CvInvoke.cvEqualizeHist(image.Ptr, image.Ptr);
            //CvInvoke.cvShowImage("song", image.Ptr);
            //CvInvoke.cvWaitKey(0);
            //Image<Gray, byte>rezImage = image.Resize(SIFTConstant.NORMALIZE_WIDTH, SIFTConstant.NORMALIZE_HEIGHT, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);

            ImageFeature<float>[] features = surfDetector.DetectFeatures(image, null);

            return ImageFeatureTOFloatArray(features);
        }
        public float[][] Extract(Bitmap bitmap)
        {
            //Converts bgr into gray.
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);

            //TODO: Smooth.
            image.SmoothMedian(5);
            //CvInvoke.cvShowImage("song", image.Ptr);
            //CvInvoke.cvWaitKey(0);
            //Image<Gray, byte> rezImage = image.Resize(SIFTConstant.NORMALIZE_WIDTH, SIFTConstant.NORMALIZE_HEIGHT, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            ImageFeature<float>[] features = surfDetector.DetectFeatures(image, null);
            return ImageFeatureTOFloatArray(features);
        }

        public static float[][] Extract(byte[] imageBytes, SIFTParam param)
        {
            SIFTExtract detector = new SIFTExtract(param);
            return detector.Extract(imageBytes);
        }
        public static float[][] Extract(Bitmap bitmap, SIFTParam param)
        {
            SIFTExtract detector = new SIFTExtract(param);
            return detector.Extract(bitmap);
        }


        #endregion

        #region Private Methods
        private float[][] ImageFeatureTOFloatArray(ImageFeature<float>[] features)
        {
            float[][] featureArray = new float[features.Length][];
            for (int i = 0; i < features.Length; i++)
            {
                featureArray[i] = new float[features[i].Descriptor.Length];
                Array.Copy(features[i].Descriptor, featureArray[i], features[i].Descriptor.Length);
            }

            return featureArray;
        }
        #endregion

    }
}
