using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace TextDeal
{
    [StructLayout(LayoutKind.Explicit)]
    public struct result_t
    {
        [FieldOffset(0)]
        public int start;
        [FieldOffset(4)]
        public int length;
        [FieldOffset(8)]
        public int sPos1;
        [FieldOffset(12)]
        public int sPos2;
        [FieldOffset(16)]
        public int sPos3;
        [FieldOffset(20)]
        public int sPos4;
        [FieldOffset(24)]
        public int sPos5;
        [FieldOffset(28)]
        public int sPos6;
        [FieldOffset(32)]
        public int sPos7;
        [FieldOffset(36)]
        public int sPos8;
        [FieldOffset(40)]
        public int sPos9;
        [FieldOffset(44)]
        public int sPos10;
        //[FieldOffset(12)] public int sPosLow;
        [FieldOffset(48)]
        public int POS_id;
        [FieldOffset(52)]
        public int word_ID;
        [FieldOffset(56)]
        public int word_type;
        [FieldOffset(60)]
        public int weight;
    }
    class Program
    {
        const string path = @"NLPIR.dll";//设定dll的路径
        //对函数进行申明
        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Init")]
        public static extern bool NLPIR_Init(String sInitDirPath, int encoding, String sLicenseCode);

        //特别注意，C语言的函数NLPIR_API const char * NLPIR_ParagraphProcess(const char *sParagraph,int bPOStagged=1);必须对应下面的申明
        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcess")]
        public static extern IntPtr NLPIR_ParagraphProcess(String sParagraph, int bPOStagged = 1);

        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Exit")]
        public static extern bool NLPIR_Exit();

        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ImportUserDict")]
        public static extern int NLPIR_ImportUserDict(String sFilename);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_FileProcess")]
        public static extern bool NLPIR_FileProcess(String sSrcFilename, String sDestFilename, int bPOStagged = 1);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_FileProcessEx")]
        public static extern bool NLPIR_FileProcessEx(String sSrcFilename, String sDestFilename);

        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetParagraphProcessAWordCount")]
        static extern int NLPIR_GetParagraphProcessAWordCount(String sParagraph);
        //NLPIR_GetParagraphProcessAWordCount
        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessAW")]
        static extern void NLPIR_ParagraphProcessAW(int nCount, [Out, MarshalAs(UnmanagedType.LPArray)] result_t[] result);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_AddUserWord")]
        static extern int NLPIR_AddUserWord(String sWord);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_SaveTheUsrDic")]
        static extern int NLPIR_SaveTheUsrDic();

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_DelUsrWord")]
        static extern int NLPIR_DelUsrWord(String sWord);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_NWI_Start")]
        static extern bool NLPIR_NWI_Start();

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_NWI_Complete")]
        static extern bool NLPIR_NWI_Complete();

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_NWI_AddFile")]
        static extern bool NLPIR_NWI_AddFile(String sText);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_NWI_AddMem")]
        static extern bool NLPIR_NWI_AddMem(String sText);

        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi, EntryPoint = "NLPIR_NWI_GetResult")]
        public static extern IntPtr NLPIR_NWI_GetResult(bool bWeightOut = false);

        [DllImport(path, CharSet = CharSet.Ansi, EntryPoint = "NLPIR_NWI_Result2UserDict")]
        static extern uint NLPIR_NWI_Result2UserDict();

        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi, EntryPoint = "NLPIR_GetKeyWords")]
        public static extern IntPtr NLPIR_GetKeyWords(String sText, int nMaxKeyLimit = 50, bool bWeightOut = false);

        [DllImport(path, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetFileKeyWords")]
        public static extern IntPtr NLPIR_GetFileKeyWords(String sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false);

        /// <summary>
        /// 主程序入口点
        /// </summary>
        /// <param name="args"></param>
        /// 
        static String InputNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.negative.temp.text.nonum.txt";
        static String InputPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.positive.temp.text.nonum.txt";
        static String InputTestNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.negative1.txt";
        static String InputTestPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.positive.txt";

        static String OutputNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.negative.temp.text.nonum.key.txt";
        static String OutputPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.positive.temp.text.nonum.key.txt";
        static String OutputTestNegFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.negative.key.txt";
        static String OutputTestPosFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.positive.key.txt";

        static string[] ReadFile(string path)
        {
            StreamReader sReader = new StreamReader(path, Encoding.Default);
            string allData = sReader.ReadToEnd();
            sReader.Close();
            string[] arrLine = allData.Split("\n".ToCharArray());
            return arrLine;
        }
        static void SplitWord(string[] arrLine,string outputfile)
        {
            StringBuilder textBuilder = new StringBuilder();
            for(int i = 0;i < arrLine.Length;i++)
            {
                int count = NLPIR_GetParagraphProcessAWordCount(arrLine[i]);    //先得到结果的词数
//                result_t[] result = new result_t[count];//在客户端申请资源
//                NLPIR_ParagraphProcessAW(count, result);//获取结果存到客户的内存中
                IntPtr intPtr = NLPIR_ParagraphProcess(arrLine[i],0);//切分结果保存为IntPtr类型
                String str = Marshal.PtrToStringAnsi(intPtr);//将切分结果转换为string
                textBuilder.Append(str.Trim()+"\n");
            }
//            string TempFileName = "D:/visual studio 2013/Projects/TextDeal/Sentiment/test.label.cn.positive.split.txt";
            Class1.WriteToFile(outputfile, textBuilder.ToString().Trim());
        }
        static void GetKeyWords(string inputfile = "",string outputfile = "")
        {
//            string[] arrLine = ReadFile(path);
            IntPtr intPtr = NLPIR_GetFileKeyWords("D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.positive.temp.text.nonum.txt", 6000);
            String str = Marshal.PtrToStringAnsi(intPtr);//将切分结果转换为string
            string[] strLine = str.Split("#".ToCharArray());
            StringBuilder textBuilder = new StringBuilder();
            for (int i = 0; i < strLine.Length; i++)
            {
                textBuilder.Append(strLine[i] + "\r\n");
            }
            Class1.WriteToFile("D:/visual studio 2013/Projects/TextDeal/Sentiment/cn_sample_data/sample.positive.keywords.txt", textBuilder.ToString().Trim());
        }
        static void Main1(string[] args)
        {
            if (!NLPIR_Init("D:/visual studio 2013/Projects/TextDeal/", 0, ""))//给出Data文件所在的路径，注意根据实际情况修改。
            {
                System.Console.WriteLine("Init ICTCLAS failed!");
                return;
            }
            System.Console.WriteLine("Init ICTCLAS success!");
            int ncount = NLPIR_ImportUserDict("E:/Actual/ntusdkey.txt");
            Console.WriteLine("已导入"+ncount+"条用户词典");
            GetKeyWords();
//             string[] arrLine = ReadFile(InputTestPosFileName);
//             SplitWord(arrLine, OutputTestPosFileName);
            NLPIR_Exit();
        }

    }
}
//String s = "　　【环球网综合报道】俄罗斯世界武器贸易分析中心12月26日根据已经签订的合同和采购意向，对2012年除了俄罗斯之外的其他国家巨额武器交易情况进行总结，推出了年度世界20大武器交易排行榜，其中印度不仅高居榜首，而且还6次上榜。具体情况如下：";

//int count = NLPIR_GetParagraphProcessAWordCount(s);//先得到结果的词数
//System.Console.WriteLine("NLPIR_GetParagraphProcessAWordCount success!");

//result_t[] result = new result_t[count];//在客户端申请资源
//NLPIR_ParagraphProcessAW(count, result);//获取结果存到客户的内存中
//int i = 1;
//foreach (result_t r in result)
//{
//    String sWhichDic = "";
//    switch (r.word_type)
//    {
//        case 0:
//            sWhichDic = "核心词典";
//            break;
//        case 1:
//            sWhichDic = "用户词典";
//            break;
//        case 2:
//            sWhichDic = "专业词典";
//            break;
//        default:
//            break;
//    }
//    Console.WriteLine("No.{0}:start:{1}, length:{2},POS_ID:{3},Word_ID:{4}, UserDefine:{5}\n", i++, r.start, r.length, r.POS_id, r.word_ID, sWhichDic);//, s.Substring(r.start, r.length)
//}

//StringBuilder sResult = new StringBuilder(600);
////准备存储空间         

//IntPtr intPtr = NLPIR_ParagraphProcess(s);//切分结果保存为IntPtr类型
//String str = Marshal.PtrToStringAnsi(intPtr);//将切分结果转换为string
//Console.WriteLine(str);