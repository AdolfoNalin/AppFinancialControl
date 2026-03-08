using AppFinancialControl.Models;
using AppFinancialControl.Service;
using System.Threading.Tasks;

namespace AppFinancialControl.View;

public partial class TransactionList : ContentPage
{
    private TransactionAdd _add;
    private TransactionEdit _edit;
    private ITransactionService _service;
	public TransactionList(TransactionAdd add, TransactionEdit edit, ITransactionService service)
	{
        _add = add;
        _edit = edit;
        _service = service;

		InitializeComponent();
       
        cvTransaction.ItemsSource = UpdateData();
    }

    #region UpdateData
    private List<Transaction> UpdateData()
    {
        try
        {
            List<Transaction> list = _service.GetAll() ?? 
                throw new ArgumentNullException("Nenhuma Despesa ou Saldo cadastrado");

            return list;
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Error", $"{ane.Message}", "Ok");
            return null;
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Ok");
            return null;
        }
    }
    #endregion

    #region OpenScreenAdd
    private async void OpenScreenAdd(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushModalAsync(_add);
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
            Navigation.PushModalAsync(_edit);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion
}