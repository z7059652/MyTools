using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextDeal
{
    class VectorCosine:ITextDeal
    {
        private string InputFilename = "E:/BigData/feature.txt";
        public override void TextDeal()
        {
            StreamReader sReader = new StreamReader(InputFilename, Encoding.Default);
            string allData = sReader.ReadToEnd();
            base.TextDeal();
        }
    }
}
