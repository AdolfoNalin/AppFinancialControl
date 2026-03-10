

using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppFinancialControl.View;

public partial class TransactionEdit : ContentPage
{
    private Transaction _transaction;
    private ITransactionService _service;
	public TransactionEdit(ITransactionService service)
	{
		InitializeComponent();
        _service = service;
	}

    public void SetTransactionToEdit(Transaction transaction)
    {
        try
        {
            _transaction = transaction;

            if(_transaction.Type == TransactionType.Income)
            {
                rbIncome.IsChecked = true;
            }
            else
            {
                rbExpense.IsChecked = true;
            }

            etName.Text = _transaction.Name;
            dpDate.Date = _transaction.Date.Date;
            etValue.Text = _transaction.Value.ToString("C");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Ok");
        }
    }

    #region TapGestureRecognizer_Tapped
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion

    #region SaveEdit
    private void SaveEdit(object sender, EventArgs e)
    {
        try
        {
            Transaction transaction = new Transaction()
            {
                Id = _transaction.Id,
                Name = etName.Text ?? throw new ArgumentNullException("Nome é um campo necessário"),
                Type = rbExpense.IsChecked == true ? TransactionType.Expenses : TransactionType.Income,
                Date = dpDate.Date ?? throw new ArgumentNullException("Data é um campo necessário"),
                Value = float.Parse(etValue.Text.Replace("R$", "") ?? throw new ArgumentNullException("Valor é um campo necessário")),
            };

            _service.Update(transaction);

            DisplayAlert("Aceito", "Trasação atualizada", "Ok");

            Navigation.PopModalAsync();

            WeakReferenceMessenger.Default.Send<string>("");
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Error", $"{ane.ParamName}", "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion
}