namespace AppFinancialControl.View;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
		InitializeComponent();
	}

    private void OpenScreen(object sender, EventArgs e)
    {
        try
        {
            App.Current.MainPage = new TransactionAdd();
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }
}