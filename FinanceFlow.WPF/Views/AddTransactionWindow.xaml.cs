using System;
using System.Windows;
using FinanceFlow.Domain.Interfaces;
using FinanceFlow.Domain.Models.Transactions;

namespace FinanceFlow.Wpf.Views
{
    public partial class AddTransactionWindow : Window
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddTransactionWindow(ITransactionRepository transactionRepository)
        {
            InitializeComponent();
            _transactionRepository = transactionRepository;
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                try
                {
                    var newTransaction = new Transaction(
                        0,
                        amount,
                        txtDescription.Text,
                        1,
                        DateTime.Now
                    );

                    await _transactionRepository.AddAsync(newTransaction);

                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving transaction: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric amount.");
            }
        }
    }
}