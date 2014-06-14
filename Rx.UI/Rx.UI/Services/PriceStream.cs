using Rx.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx.UI.Services
{
    public interface IPriceStream
    {
        IObservable<PriceData> GetPrices();

    }
    public class PriceStream : IPriceStream
    {
        Random r = new Random();

        string[] tickers = new[] { "MSFT", "GOOG", "FB", "AAPL" };

        public IObservable<PriceData> GetPrices()
        {
            return Observable.Interval(TimeSpan.FromSeconds(1))
                     .Select(_ =>
                         new PriceData()
                         {
                             Price = Convert.ToDecimal(r.NextDouble()),
                             Ticker = tickers[r.Next(0, 4)]
                         });
        }
    }
}
