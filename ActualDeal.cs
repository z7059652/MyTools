using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextDeal
{
    class ActualDeal : ITextDeal
    {
        private string TestLableName = "E:/Actual/train/label.txt";
        private string TestfeaName = "E:/Actual/train/feature.txt";
        private string readFile(string FileName)
        {
            try
            {
                StreamReader sReader = new StreamReader(FileName, Encoding.UTF8);
                string allData = sReader.ReadToEnd();
                sReader.Close();
                return allData;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }
        private void WriteFile(string FileName,string strdata)
        {
            StreamWriter writer = new StreamWriter(FileName, true, Encoding.Default);
            writer.Write(strdata);
            writer.Close();
        }
        public override void TextDeal()
        {
//            string allData = readFile("E:/Actual/train12.txt");
            StreamReader sReader = new StreamReader("E:/Actual/train12.txt", Encoding.Default);
            while(sReader.Peek() > -1)
            {            
                string allData = sReader.ReadLine();
                string[] arrLine = allData.Split("\n".ToCharArray());
                StringBuilder LabelBuilder = new StringBuilder();
                StringBuilder FeatureBuilder = new StringBuilder();
                int temp = 0;
                for(int i = 0;i < arrLine.Length;i++)
                {
                    string[] text = arrLine[i].Trim().Split(" ".ToCharArray());
                    if (text.Length <= 1152)
                        continue;
                    temp = Convert.ToInt32(text[0]);
                    LabelBuilder.Append(temp.ToString()+" ");
                    for(int j = 1;j < text.Length;j++)
                    {
                        string[] fea = text[j].Trim().Split(":".ToCharArray());
                        FeatureBuilder.Append(fea[1] + " ");
                    }
                    FeatureBuilder.Append("\r\n");
                }
                WriteFile(TestLableName,LabelBuilder.ToString());
                WriteFile(TestfeaName, FeatureBuilder.ToString());
            }
            sReader.Close();
        }
    }
}
