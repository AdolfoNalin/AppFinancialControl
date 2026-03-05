using AppFinancialControl.Models;
using AppFinancialControl.Service;
using System.Threading.Tasks;

namespace AppFinancialControl.View;

public partial class TransactionAdd : ContentPage
{
	public TransactionAdd()
	{
        InitializeComponent();
    }

    #region TapGestureRecognizer_Tapped
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            Navigation.PopModalAsync();
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
                Name = EntryName.Text ?? throw new ArgumentNullException("Nome é um campo necessário"),
                Date = DPDate.Date ?? throw new ArgumentNullException("Data é um campo necessário"),
                Value = float.Parse(EntryValue.Text ?? throw new ArgumentNullException("Valor é um campo necessário")
                ?? throw new FormatException("O campo 'Valor' só pode ser digitado somente números")),
                Type = rbEntry.IsChecked == true ? TransactionType.Income : TransactionType.Expenses,
            };

            var repository = this.Handler.MauiContext.Services.GetService<ITransactionService>();
            repository.Add(transaction);

            DisplayAlert("Aceito", "Transição salva", "ok");

            Navigation.PopModalAsync();
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