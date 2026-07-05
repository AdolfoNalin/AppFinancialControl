using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Threading.Tasks;

namespace AppFinancialControl.View;

public partial class TransactionAdd : ContentPage
{
    private ITransactionService _service;

	public TransactionAdd(ITransactionService service)
	{
        InitializeComponent();
        _service = service;
    }

    #region TapGestureRecognizer_Tapped
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #endregion

    #region ButtonClicked
    private void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            Transaction transaction = new Transaction()
            {
                UserId = UserSession.Id,
                Name = EntryName.Text ?? throw new ArgumentNullException("Nome é um campo necessário"),
                Date = DateOnly.Parse(DPDate.Date.Value.Date.ToString("D") ?? throw new ArgumentNullException("Data é um campo necessário")),
                Value = Math.Abs(float.Parse(EntryValue.Text ?? throw new ArgumentNullException("Valor é um campo necessário")
                ?? throw new FormatException("O campo 'Valor' só pode ser digitado somente números"))),
                Type = rbEntry.IsChecked == true ? TransactionType.Income : TransactionType.Expenses,
            };

            _service.Add(transaction);

            DisplayAlert("Aceito", "Transição salva", "ok");

            Navigation.PopModalAsync();
            WeakReferenceMessenger.Default.Send<String>("");
        }
        catch(FormatException fe)
        {
            DisplayAlert("Error", $"{fe.Message}", "OK");
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Error", ane.ParamName, "Ok");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");;
        }
    }
    #endregion
}