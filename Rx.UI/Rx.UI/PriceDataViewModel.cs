using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx.UI
{
    public class PriceDataViewModel
    {
        public decimal Price { get; set; }

        public string Ticker { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}
