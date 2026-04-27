namespace AppFinancialControl.View;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    #region Button_Clicked_ReadThePassword
    private void Button_Clicked_ReadThePassword(object sender, EventArgs e)
    {
        try
        {
            if(txtPassoword.IsPassword)
            {
                txtPassoword.IsPassword = false;
            }
            else
            {
                txtPassoword.IsPassword = true;
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion
}