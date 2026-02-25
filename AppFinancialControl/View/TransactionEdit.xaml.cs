using System.Threading.Tasks;

namespace AppFinancialControl.View;

public partial class TransactionEdit : ContentPage
{
	public TransactionEdit()
	{
		InitializeComponent();
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