using LabCMS.TestAbilityDomain.ImportHelper.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Raccoon.DevKits.Wpf.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LabCMS.TestAbilityDomain.ImportHelper.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IDIApplication
    {
        public IServiceProvider ServiceProvider { get; set; } = null!;
        public IConfiguration Configuration { get; set; } = null!;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowVM>();
            services.AddTransient<XlsxParseService>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.OnStartupProxy<MainWindow>();
        }
    }
}
