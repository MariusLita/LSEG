using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSEG_API
{
    public class Stocks
    {
        public string stockID { get; set; }

        public string timeStamp { get; set; }

        public double stockPrice { get; set; }

    }
}
