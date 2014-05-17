using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OIForShine
{
    public partial class OIUI : Form, OIManager.IOutputLog
    {
        int minK = 0;
        int maxK = 100;
        string trainFolerPath = string.Empty;
        string testFolerPath = string.Empty;
        StringBuilder outputLogBuilder = new StringBuilder();
        
        public OIUI()
        {
            InitializeComponent();
        }


        List<Tuple<string, string>> PrepareParam(string folderPath)
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();

            Dictionary<string, string> folerDic = GetAllFolderPath(folderPath);
            foreach (var item in folerDic)
            {
                List<string> allJpgPath = GetAllJPGPath(item.Value);
                foreach (string jpgPath in allJpgPath)
                {
                    result.Add(new Tuple<string, string>(item.Key, jpgPath));
                }
            }

            return result;
        }

        Dictionary<string,string> GetAllFolderPath(string parementFolderPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(parementFolderPath);
            DirectoryInfo[] childDirectorInfo = directoryInfo.GetDirectories();
            Dictionary<string, string> resultDic = new Dictionary<string, string>();

            foreach (var item in childDirectorInfo)
            {
                resultDic[item.Name] = item.FullName;
            }
            return resultDic;
        }

        List<string> GetAllJPGPath(string folerPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folerPath);
            FileInfo[] allJpgs = directoryInfo.GetFiles();

            List<string> result = new List<string>();
            foreach (var item in allJpgs)
            {
                result.Add(item.FullName);
            }

            return result;
        }

        List<Bitmap> GetAllJPG(string folerPath)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(folerPath);
            FileInfo[] allJpgs = directoryInfo.GetFiles();

            List<Bitmap> result = new List<Bitmap>();
            foreach (var item in allJpgs)
            {
                Bitmap bitmap = new Bitmap(item.FullName);
                result.Add(bitmap);
            }

            return result;
        }

        private void btnTrainPath_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tbTrainPath.Text = this.folderBrowserDialog1.SelectedPath;
                
            }
        }

        private void btnTestPath_Click(object sender, EventArgs e)
        {

            if (this.folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tbTestPath.Text = this.folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnGO_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.tbTrainPath.Text)
                && !string.IsNullOrEmpty(this.tbTestPath.Text)
                && !string.IsNullOrEmpty(this.tbKMin.Text)
                && !string.IsNullOrEmpty(this.tbKMax.Text))
            {
                if (int.TryParse(this.tbKMin.Text, out minK) == false
                    || int.TryParse(this.tbKMax.Text, out maxK) == false)
                {
                    MessageBox.Show("K must be a number.");
                }
                else
                {
                    int.TryParse(this.tbKMin.Text, out minK);
                    int.TryParse(this.tbKMax.Text, out maxK);
                    if (minK > maxK)
                    {
                        MessageBox.Show("Min K must be smaller than max K.");
                        return;
                    }
                    this.trainFolerPath = this.tbTrainPath.Text;
                    this.testFolerPath = this.tbTestPath.Text;
                   

                    //for (int k = minK; k <= maxK; k++)
                    //{
                    //    OIManager.OIManager oiManager = new OIManager.OIManager(k);
                    //    Process(oiManager, trainFolerPath, testFolerPath, k);
                    //}
                    DisableButton();
                    Thread thread = new Thread(new ThreadStart(FindOptimalTestK));

                    thread.IsBackground = true;

                    thread.Start();
                    

                }
            }
            else
            {
                MessageBox.Show("Null denied...");
            }
        }
        delegate void EnableButtonDelegate();

        void DisableButton()
        {
            this.btnGO.Enabled = false;
            this.btnClearLog.Enabled = false;
        }
        void EnabelButton()
        {
            this.btnGO.Enabled = true;
            this.btnClearLog.Enabled = true;            
        }
        void FindOptimalTestK()
        {
            List<Tuple<string, string>> allTrainJpgPath = PrepareParam(trainFolerPath);
            List<Tuple<string, string>> allPredictJpgPath = PrepareParam(testFolerPath);
            OIManager.OIManager.OptimalTest(allTrainJpgPath, allPredictJpgPath, minK, maxK, this);
            this.Invoke(new EnableButtonDelegate(EnabelButton));
        }
        void Process(OIManager.OIManager oiManager, string trainFolerPath, string predictFolerPath, int k)
        {
            
            //OutputLogInfo(string.Format("Start------K={0}-----------------------------\r\n", k));

            //OutputLogInfo("Retriving all train jpgs...");
            //List<Tuple<string, string>> allTrainJpgPath = PrepareParam(trainFolerPath);
            //OutputLogInfo(string.Format("Total train jpgs is {0}...", allTrainJpgPath.Count));

            //OutputLogInfo("Start training...");
            //oiManager.Train(allTrainJpgPath);
            //OutputLogInfo("Training finished...");

            //OutputLogInfo("Retriving all predict jpgs...");
            //List<Tuple<string, string>> allPredictJpgPath = PrepareParam(predictFolerPath);
            //OutputLogInfo(string.Format("Total predict jpgs is {0}...", allPredictJpgPath.Count));

            //OutputLogInfo("Start predicting...");
            //int success = 0;
            //foreach (var item in allPredictJpgPath)
            //{
            //    Bitmap bitmap = new Bitmap(item.Item2);
            //    if (item.Item1 == oiManager.Predict(bitmap))
            //    {
            //        success++;
            //    }
            //}
            //OutputLogInfo(string.Format("Total correct is {0}, accuracy is {1}", success, (float)success/allPredictJpgPath.Count));

            //OutputLogInfo(string.Format("End------K={0}-----------------------------\r\n", k));

        }
        private delegate void SetTextCallback(string text);
        public void OutputLogInfo(string msg)
        {
            if (this.tbOutputLog.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(OutputLogInfo);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                outputLogBuilder.Append(msg + "\r\n");
                this.tbOutputLog.Text = outputLogBuilder.ToString();
                this.Refresh();
            }
          
            
        }
        void ClearLog()
        {
            outputLogBuilder.Clear();
            this.tbOutputLog.Text = string.Empty;
            this.Refresh();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            this.ClearLog();
        }
    }
}
