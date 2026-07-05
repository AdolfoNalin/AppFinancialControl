

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

    #region SetTransactionToEdit
    /// <summary>
    /// method responsable for set the entry for screen
    /// </summary>
    /// <param name="transaction"></param>
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
            dpDate.Date = DateTime.Parse(_transaction.Date.ToString());
            etValue.Text = _transaction.Value.ToString("C");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Ok");
        }
    }
    #endregion

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
    /// <summary>
    /// Event responsable for edit the transaction
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <exception cref="NullReferenceException"></exception>
    private void SaveEdit(object sender, EventArgs e)
    {
        try
        {
            Transaction transaction = new Transaction()
            {
                Id = _transaction.Id,
                UserId = UserSession.Id,
                Name = etName.Text ?? throw new ArgumentNullException("Nome é um campo necessário"),
                Type = rbExpense.IsChecked == true ? TransactionType.Expenses : TransactionType.Income,
                Date = DateOnly.Parse(dpDate.Date.Value.Date.ToString("D")?? throw new NullReferenceException("O campo data é obrigatório")),
                Value = Math.Abs(float.Parse(etValue.Text.Replace("R$", "") ?? throw new ArgumentNullException("Valor é um campo necessário"))),
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