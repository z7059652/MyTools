using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TextDeal
{
    class CharExtractor:ITextDeal
    {
        public override void TextDeal()
        {
//            base.TextDeal();
            Console.Write("hello world\n");
            String FileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.temp.txt";
            StreamReader sReader = new StreamReader(FileName, Encoding.Default);
            string allData = sReader.ReadToEnd();
            //           string[] arrLine = allData.Split("</review>".ToCharArray());    //System.Text.RegularExpressions.Regex.Split(str1,@"[*]+"); 
            string[] arrLine = System.Text.RegularExpressions.Regex.Split(allData, @"\r\n</review>\r\n");
            List<string> newLineList = new List<string>();
            StringBuilder modelBuilder = new StringBuilder();
            StringBuilder textBuilder = new StringBuilder();
            StringBuilder PositiveTextBuilder = new StringBuilder();
            int k = 1;
            for (int i = 0; i < arrLine.Length; i++)
            {
                //"<review id=\"5000\">\r\n看过此人在百家讲坛的演讲，简直就是垃圾。"
                string[] resLine = System.Text.RegularExpressions.Regex.Split(arrLine[i], @"label=""[0-9]"">\r\n");
                string reg = @"""[0-9]""";
                Match mat = Regex.Match(arrLine[i], reg);
                if (mat.Success)
                {
                    string les = mat.Value.ToString();
                    k = Convert.ToInt32(les.Substring(1, les.Length - 2));
                    Regex regex = new Regex("\r\n");
                    string temp = regex.Replace(resLine[1], " ");
                    if (k == 0)
                        textBuilder.AppendFormat("{0} > {1}\r\n", k, temp);
                    else
                    {
                        PositiveTextBuilder.AppendFormat("{0} > {1}\r\n", k, temp);
                    }
                }
            }
            string TempFileName = "D:/visual studio 2013/Projects/TextDeal/test.label.cn.negative.txt";
            Class1.WriteToFile(TempFileName, textBuilder.ToString().Trim());
            string TempFileName1 = "D:/visual studio 2013/Projects/TextDeal/test.label.cn.positive.txt";
            Class1.WriteToFile(TempFileName1, PositiveTextBuilder.ToString().Trim());
            Console.Read();  
        }
    }
}
