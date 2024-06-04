using EPIHelper.Commands;
using EPIHelper.Services;
using System.Windows.Input;

namespace EPIHelper.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewService _viewService;
        public MainViewModel(IViewService viewService)
        {
            _viewService = viewService;
        }
        public ICommand ShowSettingViewCommand => new RelayCommand<object>(ShowSettingView);

        private void ShowSettingView(object obj)
        {
            _viewService.ShowSettingView();
        }
    }
}
