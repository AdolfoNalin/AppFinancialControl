using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using LiveChartsCore;
using System.Collections.ObjectModel;

namespace AppFinancialControl.View;

public partial class Home : ContentPage
{
    private readonly ITransactionService _servie;
    public Home(ITransactionService service)
    {
        _servie = service;
        InitializeComponent();
        UpdateData();
        WeakReferenceMessenger.Default.Register<String>(this, (e, message) =>
        {
            UpdateData();
        });
    }

    #region UpdateData
    /// <summary>
    /// Method responsable for update data in frondend
    /// </summary>
    public async void UpdateData()
    {
        try
        {
            ObservableCollection<Transaction> transactions = _servie.GetAll(UserSession.Id)
                ?? throw new NullReferenceException("Nenhuma transação encontrada");

            Double.TryParse(transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value).ToString(), out double income);
            Double.TryParse(transactions.Where(t => t.Type == TransactionType.Expenses).Sum(t => t.Value).ToString(), out double expenses);

            double balance = income - expenses;

            lblExpenses.Text = $"\n{expenses.ToString("C")}";
            lblInconse.Text = $"\n{income.ToString("C")}";
            lblBalance.Text = $"\n{balance.ToString("C")}";

            sylblExpenses.Text = $"{expenses.ToString("C")}";
            sylblInconse.Text = $"{income.ToString("C")}";
            sylblBalance.Text = $"{balance.ToString("C")}";

            Summary summary = new Summary(_servie);
            charts.Series = Array.Empty<ISeries>();
            charts.Series = summary.Series;
        }
        catch (NullReferenceException nre)
        {
            DisplayAlert("Erro", nre.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region ImagemClicked_NewTransaction
    private async void ImagemClicked_NewTransaction(object sender, EventArgs e)
    {
        try
        {
            TransactionAdd screen = this.Handler.MauiContext.Services.GetService<TransactionAdd>()
                ?? throw new ArgumentNullException("Contexto MAUI está nulo");

            Navigation.PushAsync(screen);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ImagemClicked_NewGoals
    private void ImagemClicked_NewGoals(object sender, EventArgs e)
    {
        try
        {
            GoalsAdd screen = this.Handler.MauiContext.Services.GetService<GoalsAdd>()
                ?? throw new ArgumentNullException("Context Maui Is null");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion
}