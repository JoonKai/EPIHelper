using Caliburn.Micro;
using EPIHelper.Commands;
using EPIHelper.ViewModels;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EPIHelper.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive
    {
        
        //public ICommand ShowSettingViewCommand => new RelayCommand<object>(OpenSettingView);

        //private void OpenSettingView(object obj)
        //{
        //    OnViewLoaded(SettingView);
        //}


        private IEventAggregator _eventAggregator;
        public MainViewModel()
        {
            _eventAggregator = new EventAggregator();
            _eventAggregator.SubscribeOnUIThread(this);
            Task.Run(async () =>
            {
                await ActivateItemAsync(new StartViewModel(_eventAggregator));
            });
        }
        public void LoadPagePLTrend()
        {
            Task.Run(async () =>
            {
                await ActivateItemAsync(new PLTrendViewModel());
            });
        }
        public void LoadPageTest()
        {
            Task.Run(async () =>
            {
                await ActivateItemAsync(new PLTrendViewModel());
            });
        }
        //public Task HandleAsync(string message, CancellationToken cancellationToken)
        //{
        //    this.Message = message.ToString();
        //    return Task.CompletedTask;
        //}
    }
}
