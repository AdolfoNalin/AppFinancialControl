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
            TransactionEdit edit = this.Handler.MauiContext.Services.GetService<TransactionEdit>()
                ?? throw new ArgumentNullException("Maiu is null");
            Navigation.PushModalAsync(edit);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion
}