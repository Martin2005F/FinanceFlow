using FinanceFlow.Data.DbContext;
using FinanceFlow.Data.Repositories;
using FinanceFlow.Domain.Interfaces;
using FinanceFlow.Domain.Models.Transactions;
using FinanceFlow.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;

namespace FinanceFlow.Wpf
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; } = null!;

        public App()
        {
            var services = new ServiceCollection();

            services.AddSingleton<SqliteDbContext>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<AddTransactionWindow>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}