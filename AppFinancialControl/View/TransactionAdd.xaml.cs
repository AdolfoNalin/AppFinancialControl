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
}