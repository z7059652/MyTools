using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace TextDeal
{
    class Class1
    {
        static void Main(string[] args)
        {            
            int i = 0;
            Console.WriteLine("process start");
//             ITextDeal KW = new KeyWord("E:/Actual/ntusd.txt","E:/Actual/ntusdkey.txt");
//             KW.TextDeal();
            GenVector.INSTANCE.GenNegVector();
        }
        public static bool WriteToFile(string path, string strdata)
        {
            StreamWriter writer = new StreamWriter(path, true, Encoding.Default);
            writer.Write(strdata);
            writer.Close();
            return true;
        }
        /// <summary>
        /// 第二步处理
        /// 功能：主要是把每一个ID 对应的字符串提取出来
        /// </summary>
        /// 
        static void deal2()
        {

        }
        /// <summary>
        /// 第一步处理：把空格行删掉
        /// </summary>
        static void deal1()
        {

        }
    }
}
