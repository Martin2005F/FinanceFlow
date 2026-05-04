using FinanceFlow.Domain.Interfaces;
using FinanceFlow.Domain.Models.Transactions;
using FinanceFlow.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace FinanceFlow.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly ITransactionRepository _transactionRepository;

        public MainWindow(ITransactionRepository transactionRepository)
        {
            InitializeComponent();
            _transactionRepository = transactionRepository;

            LoadDashboardData();
            RefreshTransactionsList();
        }

        private async void LoadDashboardData()
        {
            try
            {
                var totalBalance = await _transactionRepository.GetTotalBalanceAsync();
                txtTotalBalance.Text = $"{totalBalance:N2} €";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading balance: {ex.Message}");
            }
        }

        private async void RefreshTransactionsList()
        {
            try
            {
                var transactions = await _transactionRepository.GetAllAsync();
                dgTransactions.ItemsSource = transactions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading list: {ex.Message}");
            }
        }


        private void BtnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            var addWindow = app.ServiceProvider.GetRequiredService<AddTransactionWindow>();

            if (addWindow.ShowDialog() == true)
            {
                LoadDashboardData();
                RefreshTransactionsList();
            }
        }

        private void BtnDashboard_Click(object sender, RoutedEventArgs e)
        {
            LoadDashboardData();
            RefreshTransactionsList();
        }

        private void BtnTransactions_Click(object sender, RoutedEventArgs e)
        {
            RefreshTransactionsList();
            MessageBox.Show("Transaction list refreshed!");
        }

        private void BtnCategories_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Categories management is coming soon!");
        }
    }
}