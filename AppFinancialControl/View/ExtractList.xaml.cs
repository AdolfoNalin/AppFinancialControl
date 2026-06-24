using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppFinancialControl.View;

public partial class ExtractList : ContentPage
{
    private readonly ITransactionService _service;
	public ExtractList(ITransactionService servuce)
	{
        _service = servuce;
		InitializeComponent();

        UpdateData();
        WeakReferenceMessenger.Default.Register<string>(this, (e, message) =>
        {
            UpdateData();
        });
	}

    #region UpateData
    private void UpdateData()
    {
        try
        {
            DateOnly dateNow = DateOnly.Parse(DateTime.Now.Date.ToString("D"));

            ObservableCollection<Transaction> transactions = _service.GetAll(UserSession.Id)
                ?? throw new NullReferenceException("Nenhuma transação realizada neste perildo de tempo");


            float income = transactions.Where(b => b.Type == TransactionType.Income)
                .Sum(a => a.Value);

            float expensse = transactions.Where(b => b.Type == TransactionType.Expenses)
               .Sum(a => a.Value);

            float balance = income - expensse;

            lblBalance.Text = balance.ToString("C");
            lblIncome.Text = income.ToString("C");
            lblExpense.Text = expensse.ToString("C");

            cvExtract.ItemsSource = transactions;
        }
        catch(NullReferenceException nre)
        {
            DisplayAlert("Erro", nre.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.InnerException.Message, "Fechar");
        }
    }
    #endregion

}