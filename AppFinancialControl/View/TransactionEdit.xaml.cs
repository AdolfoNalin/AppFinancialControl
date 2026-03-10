

using AppFinancialControl.Models;

namespace AppFinancialControl.View;

public partial class TransactionEdit : ContentPage
{
    private Transaction _transaction;
	public TransactionEdit()
	{
		InitializeComponent();

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
}