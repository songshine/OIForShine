using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.GPU;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace TestSURF
{
    public partial class TestSURF : Form
    {
        private SURFDetector surfDetector = null;
        public TestSURF()
        {
            InitializeComponent();
            surfDetector = new SURFDetector(1000, false);
        }

        private void pbImage_DoubleClick(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap img = new Bitmap(this.openFileDialog1.FileName);

                TestSomething(img);
                //this.pbImage.Image = img;

                //Rectangle[] regions;
                //using (HOGDescriptor des = new HOGDescriptor())
                //{
                //    des.SetSVMDetector(HOGDescriptor.GetDefaultPeopleDetector());
                //    regions = des.DetectMultiScale(new Image<Bgr, Byte>(img));
                //}

                //Graphics g = Graphics.FromImage(img);
                //Pen redPen = new Pen(Color.Red);


                //foreach (Rectangle pedestrain in regions)
                //{
                //    //img(pedestrain, new Bgr(Color.Red), 1);
                //    g.DrawRectangle(redPen, pedestrain);
                //}
                //this.pbImage.Image = img;
                //return image;

                //DrawSURF2(img, Extract2(img));
                //DrawSURF(img, Extract(img));

                this.pbImage.Image = img;
            }
        }
        public void TestWithoutParam()
        {

        }
        public void TestSomething(Bitmap bitmap)
        {
            //Stopwatch watch = Stopwatch.StartNew();
            //Image<Gray, Byte>[] channels = null;
            //using (Image<Hsv, Byte> hsv = new Image<Hsv, byte>(bitmap))
            //{
            //    channels = hsv.Split();

            //    try
            //    {
            //        //channels[0] is the mask for hue less than 20 or larger than 160
            //        CvInvoke.cvInRangeS(channels[0], new MCvScalar(20), new MCvScalar(160), channels[0]);
            //        channels[0]._Not();

            //        //channels[1] is the mask for satuation of at least 10, this is mainly used to filter out white pixels
            //        channels[1]._ThresholdBinary(new Gray(10), new Gray(255.0));

            //        CvInvoke.cvAnd(channels[0], channels[1], channels[0], IntPtr.Zero);
            //    }
            //    finally
            //    {
            //        //channels[1].Dispose();
            //        //channels[2].Dispose();
            //    }
            //}

            //watch.Stop();

            //long ss = watch.ElapsedMilliseconds;

            //MessageBox.Show(ss.ToString());

            //CvInvoke.cvShowImage("channels0", channels[0]);
            //CvInvoke.cvWaitKey(0);

            //CvInvoke.cvShowImage("channels1", channels[1]);
            //CvInvoke.cvWaitKey(0);
            
            //CvInvoke.cvShowImage("channels2", channels[2]);
            //CvInvoke.cvWaitKey(0);

            /***Capture
            ImageViewer view = new ImageViewer();
            Capture capture = new Capture();
            Application.Idle += new EventHandler(delegate(object sender, EventArgs e)
                {
                    view.Image = capture.QueryFrame();
                });
            view.ShowDialog();
            ****/

            HistogramViewer view = new HistogramViewer();
            Image<Bgr, byte> img = new Image<Bgr, byte>(bitmap);
            HistogramViewer.Show(img);
            
        }


        public ImageFeature<float>[] Extract(Bitmap bitmap)
        {
            //Converts bgr into gray.
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);

            //TODO: Smooth.
            image.SmoothMedian(5);
            //if (GpuInvoke.HasCuda)
            {
                CvInvoke.cvShowImage("song", image.Ptr);
                CvInvoke.cvWaitKey(0);
            }




            image = image.PyrDown().PyrUp();


            //image = image.ThresholdBinary(new Gray(100), new Gray(255));
            //image = image.ThresholdAdaptive(new Gray(255), Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_OTSU, 6, new Gray(10));
            CvInvoke.cvShowImage("song", image.Ptr);
            CvInvoke.cvWaitKey(0);

            //Image<Gray, byte> rezImage = image.Resize(SIFTConstant.NORMALIZE_WIDTH, SIFTConstant.NORMALIZE_HEIGHT, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            ImageFeature<float>[] features = surfDetector.DetectFeatures(image, null);
            return features;
        }

        public SURFFeature[] Extract2(Bitmap bitmap)
        {
            //Converts bgr into gray.
            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);

            //TODO: Smooth.
            image.SmoothMedian(5);
            MCvSURFParams surfParams = this.surfDetector.SURFParams;
            SURFFeature[] features = image.ExtractSURF(ref surfParams);
            //CvInvoke.cvShowImage("song", image.Ptr);
            //CvInvoke.cvWaitKey(0);
            //Image<Gray, byte> rezImage = image.Resize(SIFTConstant.NORMALIZE_WIDTH, SIFTConstant.NORMALIZE_HEIGHT, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
            return features;
        }

        public void DrawSURF2(Bitmap img, SURFFeature[] features)
        {
            Graphics g = Graphics.FromImage(img);
            Pen redPen = new Pen(Color.Red);
            foreach (var item in features)
            {
                g.DrawEllipse(redPen, item.Point.pt.X - 1, item.Point.pt.Y - 1, 4, 4);
            }
        }

        public void DrawSURF(Bitmap img, ImageFeature<float>[] features)
        {
            Graphics g = Graphics.FromImage(img);
            Pen redPen = new Pen(Color.Red);
            foreach (var item in features)
            {
                g.DrawEllipse(redPen, item.KeyPoint.Point.X - 1, item.KeyPoint.Point.Y - 1, 4, 4);
            }
        }
    }
}
