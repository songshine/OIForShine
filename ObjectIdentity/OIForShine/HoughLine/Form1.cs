using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace HoughLine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Bitmap bitmap = new Bitmap(this.openFileDialog1.FileName);
                //this.pictureBox1.Image = bitmap;


                this.pictureBox1.Image = FindHoughLine(bitmap);
            }
        }


        private Bitmap FindHoughLine(Bitmap bitmap)
        {
            Image<Gray, byte> gray = new Image<Gray, byte>(bitmap);
            Image<Gray, Byte> cannyEdges = gray.Canny(180, 120);        

            LineSegment2D[] lines = cannyEdges.HoughLinesBinary(
                            1, //Distance resolution in pixel-related units
                            Math.PI / 45.0, //Angle resolution measured in radians.
                            30, //threshod
                            80, //min Line width
                            10 //gap between lines
                            )[0]; //Get the lines from the first channel

            Image<Bgr, byte> lineImage = new Image<Bgr, byte>(bitmap);
            foreach (var line in lines)
            {
                lineImage.Draw(line, new Bgr(Color.Red), 2);
            }

            return lineImage.Bitmap;
        }

    }
}
