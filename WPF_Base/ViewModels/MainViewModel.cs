using Caliburn.Micro;
using EPIHelper.Commands;
using EPIHelper.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EPIHelper.ViewModels
{
    public class MainViewModel : Conductor<IScreen>.Collection.OneActive, IHandle<string>
    {
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange("Message");
            }
        }


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


        public Task HandleAsync(string message, CancellationToken cancellationToken)
        {
            this.Message = message.ToString();
            return Task.CompletedTask;
        }
    }
}
