using System.Threading.Tasks;

namespace AppFinancialControl.View;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
		InitializeComponent();
	}

    #region OpenScreenAdd
    private async void OpenScreenAdd(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushModalAsync(new TransactionAdd());
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
            Navigation.PushModalAsync(new TransactionEdit());
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "OK");
        }
    }
    #endregion
}