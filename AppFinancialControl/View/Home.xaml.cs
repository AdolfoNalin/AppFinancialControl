using AppFinancialControl.Models;
using AppFinancialControl.Service;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Collections.ObjectModel;

namespace AppFinancialControl.View;

public partial class Home : ContentPage
{
    public ISeries[] Series { get; set; } =
       {
            new PieSeries<double>
            {
                Values = new double[] { 50 }
            },
            new PieSeries<double>
            {
                Values = new double[] { 20 }
            }
        };

    public Home()
    {
		InitializeComponent();
        UpdateData();
        charts.Series = Summary.GetSeries();
	}

    #region UpdateData
    /// <summary>
    /// Method responsable for update data in frondend
    /// </summary>
    public async void UpdateData()
    {
        try
        {
            ObservableCollection<Transaction> transactions = await TransactionService.GetAllAPI(UserSession.Id)
                ?? throw new NullReferenceException("Nenhuma transação encontrada");
            Double.TryParse(transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Value).ToString(), out double income);
            Double.TryParse(transactions.Where(t => t.Type == TransactionType.Expenses).Sum(t => t.Value).ToString(), out double expenses);
            
            double balance = expenses - income;

            lblExpenses.Text += $"\n{expenses.ToString("C")}";
            lblInconse.Text += $"\n{income.ToString("C")}";
            lblBalance.Text += $"\n{balance.ToString("C")}";

            sylblExpenses.Text += $"\n{expenses.ToString("C")}";
            sylblInconse.Text += $"\n{income.ToString("C")}";
            sylblBalance.Text += $"\n{balance.ToString("C")}";
        }
        catch(NullReferenceException nre)
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
    private void ImagemClicked_NewTransaction(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}