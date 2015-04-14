using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace TextDeal
{
    class DeleteSpace:ITextDeal
    {
        public  override void TextDeal()
        {
// 	         base.TextDeal();
             Console.Write("hello world\n");
             String FileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.txt";
             StreamReader sReader = new StreamReader(FileName, Encoding.UTF8);
             string allData = sReader.ReadToEnd();
             string[] arrLine = allData.Split("\n".ToCharArray());
             //System.Text.RegularExpressions.Regex.Split(str1,@"[*]+"); 
             //            string[] arrLine = System.Text.RegularExpressions.Regex.Split(allData, @"\r\n</review>\r\n");

             string resLine = "";
             List<string> newLineList = new List<string>();
             StringBuilder modelBuilder = new StringBuilder();
             StringBuilder textBuilder = new StringBuilder();
             int k = 1;
             for (int i = 0; i < arrLine.Length; i++)
             {
                 if (arrLine[i] != "\r")
                 {
                     newLineList.Add(arrLine[i]);
                     arrLine[i] += "\n";
                     modelBuilder.Append(arrLine[i]);
                     if (!arrLine[i].StartsWith("<review") && !arrLine[i].StartsWith("</review"))
                     //                     if(arrLine[i] == "")
                     {
                         textBuilder.AppendFormat("> {0}", arrLine[i]);
                         k++;
                     }
                 }
             }
             string TempFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.temp.txt";
             Class1.WriteToFile(TempFileName, modelBuilder.ToString().Trim());
             Console.Read();       
        }
    }
}
