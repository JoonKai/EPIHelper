using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EPIHelper.Services;
using EPIHelper.ViewModels;
using EPIHelper.Views;

namespace EPIHelper
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _services = null;
        public App()
        {
            _services = ConfigureService();
        }

        private IServiceProvider ConfigureService()
        {
            IServiceCollection services = new ServiceCollection();
            //View
            services.AddSingleton<MainView>();
            services.AddTransient<SettingView>();
            //ViewModel
            services.AddSingleton<MainViewModel>();
            services.AddTransient<SettingViewModel>();
            //Service
            services.AddSingleton<IViewService, ViewService>();


            return services.BuildServiceProvider();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var viewService = (IViewService)_services.GetService(typeof(IViewService));
            viewService.ShowMainView();
            
        }
    }
}
