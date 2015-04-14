using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TextDeal
{
    class GenVector
    {
        String InputNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.negative.temp.text.nonum.key.txt";
        String InputPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.positive.temp.text.nonum.key.txt";
        String InputTestNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.negative.key.txt";
        String InputTestPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.positive.key.txt";

        static String InPutDicFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/keywords.svm.txt";

        String OutPutVecPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.vector.pos.txt";
        String OutPutVecNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.vector.neg.txt";
        //String InputTestNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.negative.key.txt";
        //String InputTestPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.positive.key.txt";

        String trainFilename = "D:/visual studio 2013/Projects/TextDeal/Sentiment/train.vector.txt";
        String testFilename = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.vector.txt";

        static Dictionary<string, int> dic = new Dictionary<string, int>(); 
        Dictionary<string, int> DicNeg = new Dictionary<string, int>();
        Dictionary<string, int> DicPos = new Dictionary<string, int>();
        List<string> listdic = new List<string>();
        private static GenVector instance = null;
        private GenVector() 
        { }
        public static GenVector INSTANCE
        {
            get
            {
                if (instance == null)
                {
                    instance = new GenVector();
                    Inital();
                }
                Console.WriteLine("get instance");
                return instance;
            }
        }
        static void Inital()
        {
            Console.WriteLine("Inital");
            string allData = LoadDicFile(InPutDicFileName);
            string[] arrLine = allData.Split("\n".ToCharArray());
            int ncount = 1;
            for (int i = 0; i < arrLine.Length; i++)
            {
                if (!dic.ContainsKey(arrLine[i].Trim()))
                {
                    dic.Add(arrLine[i].Trim(), ncount++);
                }
            }
//            CustomMethod.Sort(testDictioary); 
        }
        static string LoadDicFile(string path)
        {
            StreamReader sReader = new StreamReader(path, Encoding.Default);
            string allData = sReader.ReadToEnd();
            sReader.Close();
            return allData;
        }

        string LoadFile(string path)
        {
            StreamReader sReader = new StreamReader(path, Encoding.Default);
            string allData = sReader.ReadToEnd();
            sReader.Close();
            return allData;
        }
        void LoadPosDic(string filename,ref Dictionary<string,int> tempdic)
        {
            string allData = LoadFile(filename);
            string[] arrLine = allData.Split("\n".ToCharArray());
            StringBuilder textBuilder = new StringBuilder();
            for (int i = 0; i < arrLine.Length; i++)
            {
                string[] temp = arrLine[i].Split(" ".ToCharArray());
                for (int j = 0; j < temp.Length; j++)
                {
                    if (!tempdic.ContainsKey(temp[j].Trim()))
                    {
                        tempdic.Add(temp[j].Trim(), 1);
                    }
                }
            }
        }
        void reducedic(Dictionary<string, int> indic)
        {
            foreach(string str in dic.Keys)
            {
                if(indic.ContainsKey(str) && !listdic.Contains(str))
                {
                    listdic.Add(str); 
                }
            }
        }
        public void GenUserDic()
        {
            StringBuilder textBuilder = new StringBuilder();
            LoadPosDic(InputPosFileName,ref DicPos);
            LoadPosDic(InputNegFileName, ref DicNeg);
            reducedic(DicPos);
            reducedic(DicNeg);
            foreach (string str in listdic)
            {
                textBuilder.Append(str + "\r\n");
            }
            Class1.WriteToFile(InPutDicFileName, textBuilder.ToString() + "\r\n");
        }
        public void GenNegVector()
        {
            //1. Load neg file
            string allData = LoadFile(InputNegFileName);
            string[] arrLine = allData.Split("\n".ToCharArray());
            StringBuilder textBuilder = new StringBuilder();
            Dictionary<string, int> tempdic = new Dictionary<string, int>();
            for (int i = 0; i < arrLine.Length; i++)
            {
                int tempcount = 0;
                StringBuilder data = new StringBuilder();
//                data.Append("0 ");
                string[] temp = arrLine[i].Split(" ".ToCharArray());
                for(int j = 0;j < temp.Length; j++)
                {
                    if (!tempdic.ContainsKey(temp[j].Trim()))
                    {
                        tempdic.Add(temp[j].Trim(), 1);
                    }
                }
                foreach(string str in dic.Keys)
                {
                    if (tempdic.ContainsKey(str))
                    {
                        tempcount = 1;
                        data.AppendFormat("{0}:{1} ", dic[str], tempcount);  
                    }
                }
//                data.Append("\r\n");
                if (data.ToString().Trim().Length != 0)
                    textBuilder.AppendFormat("{0} {1}",0,data.ToString().Trim()+"\r\n");
                Class1.WriteToFile(trainFilename+"svm.txt", textBuilder.ToString());
                textBuilder.Clear();
                tempdic.Clear();
            }
        }
        public void GenPosVector()
        {

        }
    }
}
