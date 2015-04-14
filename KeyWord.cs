using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TextDeal
{
    public class KeyWord:ITextDeal
    {
        private string InputFilename;
        private string OutputFilename;
        public KeyWord(string input,string output)
        {
            InputFilename = input;
            OutputFilename = output;
        }
        public void WriteFile(string strdata)
        {
            StreamWriter writer = new StreamWriter(OutputFilename, true, Encoding.Default);
            writer.Write(strdata);
            writer.Close();
        }
        public override void TextDeal()
        {
            StreamReader sReader = new StreamReader(InputFilename, Encoding.Default);
            string allData = sReader.ReadToEnd();
            sReader.Close();
            string[] arrLine = allData.Split("\n".ToCharArray());
            StringBuilder textBuilder = new StringBuilder();
            for (int i = 0; i < arrLine.Length; i++)
            {
                textBuilder.Append(arrLine[i] + " key\r\n");
            }
            WriteFile(textBuilder.ToString().Trim());
        }
    }
}
