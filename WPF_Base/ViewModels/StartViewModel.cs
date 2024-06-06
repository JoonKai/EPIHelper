using Caliburn.Micro;
using System.Threading.Tasks;
using System.Threading;

namespace EPIHelper.ViewModels
{
    public class StartViewModel : Screen
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyOfPropertyChange("Title");
            }
        }


        private IEventAggregator _eventAggregator;

        public StartViewModel(IEventAggregator eventAgg)
        {
            Title = "Welcome to Caliburn Micro in WPF";
            _eventAggregator = eventAgg;
        }


        protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            await _eventAggregator.PublishOnUIThreadAsync("Closing");
        }

        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await _eventAggregator.PublishOnUIThreadAsync("Loading");
            await Task.Delay(2000);
            await _eventAggregator.PublishOnUIThreadAsync(string.Empty);

        }
    }
}
