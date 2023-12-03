using CoreClassesLib.Contexts;
using MapControlTryOuts.Stores;
using MapControlTryOuts.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShowMeTheXAML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MapControlTryOuts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;


        public App()
        {
            _navigationStore = new NavigationStore();
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MainWindow>();
                    services.AddTransient<IPointsContext, PointsContext>();
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<MainWindowViewModel>();
                })
                .Build();
        }

        public IHost? AppHost { get; private set; }


        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            var wnd = AppHost.Services.GetRequiredService<MainWindow>();

            wnd.Title = "Border Control";
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.WindowStyle = WindowStyle.None;
            wnd.WindowState = WindowState.Maximized;
            wnd.Show();

            base.OnStartup(e);
        }
    }
}
