using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Rx.UI.Contracts;
using Rx.UI.Services;

namespace Rx.UI
{
    public interface ISchedulerProvider
    {
        IScheduler Dispatcher { get; }
        IScheduler TaskPool { get; }
    }

    public class SchedulerProvider : ISchedulerProvider
    {

        public IScheduler Dispatcher
        {
            get { return DispatcherScheduler.Current; }
        }

        public IScheduler TaskPool
        {
            get { return TaskPoolScheduler.Default; }
        }
    }

    public class MainViewViewModel
    {

        public ObservableCollection<PriceDataViewModel> Prices { get; set; }

        public DelegateCommand StartCommand { get; private set; }

        public DelegateCommand StopCommand { get; private set; }

        private IDisposable priceStream;

        ISchedulerProvider schedulerProvider;

        public MainViewViewModel(ISchedulerProvider schedulerProvider, Func<IPriceStream> priceStreamFactory)
        {
            this.schedulerProvider = schedulerProvider;
            Prices = new ObservableCollection<PriceDataViewModel>();

            StartCommand = new DelegateCommand(() =>
            {
                if (priceStream != null) return;
                Prices.Clear();

                priceStream = priceStreamFactory().GetPrices()
                    .Timestamp()
                    .ObserveOn(schedulerProvider.Dispatcher)
                    .Subscribe(p =>
                    {
                        var price = new PriceDataViewModel 
                        {
                            Price = p.Value.Price, 
                            Ticker = p.Value.Ticker, 
                            TimeStamp = p.Timestamp.ToLocalTime() 
                        };
                        Prices.Insert(0, price);
                    });
                StopCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();

            }, () => priceStream == null);

            StopCommand = new DelegateCommand(() =>
            {
                priceStream.Dispose();
                priceStream = null;
                StopCommand.RaiseCanExecuteChanged();
                StartCommand.RaiseCanExecuteChanged();
            }, () => priceStream != null);
        }
    }
}
