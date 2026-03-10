using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;

namespace AppFinancialControl.View;

public partial class TransactionList : ContentPage
{
    private ITransactionService _service;

	public TransactionList(ITransactionService service)
	{
        _service = service;

		InitializeComponent();
       
        UpdateData();
        WeakReferenceMessenger.Default.Register<String>(this, (e, message) =>
        {
            UpdateData();
        });
    }

    #region UpdateData
    private void UpdateData()
    {
        try
        {
            List<Transaction> list = _service.GetAll() ?? 
                throw new ArgumentNullException("Nenhuma Despesa ou Saldo cadastrado");

            float income = list.Where(b => b.Type == TransactionType.Income)
                .Sum(a => a.Value);

            float expensse = list.Where(b => b.Type == TransactionType.Expenses)
               .Sum(a => a.Value);

            float balance = income - expensse;

            lblBalance.Text = balance.ToString("C");
            lblIncome.Text = income.ToString("C");
            lblExpense.Text = expensse.ToString("C");

            cvTransaction.ItemsSource = list;
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Error", $"{ane.Message}", "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Ok");
        }
    }
    #endregion

    #region OpenScreenAdd
    private async void OpenScreenAdd(object sender, EventArgs e)
    {
        try
        {
            
            TransactionAdd add =  this.Handler.MauiContext.Services.GetService<TransactionAdd>()
                ?? throw new ArgumentNullException("MauiContext is null");

            Navigation.PushModalAsync(add);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion

    #region OpenScreenEdit
    private async void OpenScreenEdit(object sender, EventArgs e)
    {
        try
        {
            var grid = (Grid)sender;
            var gestre = (TapGestureRecognizer) grid.GestureRecognizers[0];
            Transaction transaction = (Transaction) gestre.CommandParameter ?? throw new ArgumentNullException("Trasação na identificada!");

            TransactionEdit edit = this.Handler.MauiContext.Services.GetService<TransactionEdit>()
                ?? throw new ArgumentNullException("Maiu is null");

            edit.SetTransactionToEdit(transaction);

            Navigation.PushModalAsync(edit);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion
}