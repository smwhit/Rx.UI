using Microsoft.Reactive.Testing;
using Moq;
using Rx.UI.Contracts;
using Rx.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Rx.UI.Tests
{
    public class MainViewViewModelTests
    {
        [Fact]
        public void PriceIsStreamed_IsAddedToCollection()
        {
            var _schedulerProvider = new TestSchedulerProvider();
            var fakePriceStream = new Mock<IPriceStream>();

            var priceStream = new Subject<PriceData>();

            fakePriceStream.Setup(svc => svc.GetPrices()).Returns(priceStream);
            var vm = new MainViewViewModel(_schedulerProvider, () => fakePriceStream.Object);

            var price = new PriceData { Price = 1.23m, Ticker = "ABC" };

            Assert.Equal(0, vm.Prices.Count);
            vm.StartCommand.Execute(null);
            _schedulerProvider.TaskPool.Schedule(() => priceStream.OnNext(price));

            Assert.Equal(0, vm.Prices.Count);
            
            _schedulerProvider.TaskPool.AdvanceBy(1);
            Assert.Equal(0, vm.Prices.Count);
            
            _schedulerProvider.Dispatcher.AdvanceBy(1);

            Assert.Equal(1, vm.Prices.Count);
            Assert.Equal(1.23m, vm.Prices[0].Price);
            Assert.Equal("ABC", vm.Prices[0].Ticker);
        }
    }

   
}
