using AppFinancialControl.Models;
using AppFinancialControl.Service;

namespace AppFinancialControl.View;

public partial class InsertClient : ContentPage
{
    private readonly IClientService _service;
	public InsertClient(IClientService service)
	{
        _service = service;
		InitializeComponent();
	}

    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    #region Button_Clicked_Next
    private async void Button_Clicked_Next(object sender, EventArgs e)
    {
        try
        {
            Client client = new Client()
            {
                Name = txtNameClient.Text ?? throw new ArgumentNullException("Nome é obrigatório"),
                Email = txtEmailClient.Text ?? throw new ArgumentNullException("Email é obrigatório"),
                Salary = Decimal.Parse(txtSalaryClient.Text)
            };

            string message = _service.Add(client);

            DisplayAlert("Deu certo", message, "Fechar");

            InsertUser insertUser = this.Handler.MauiContext.Services.GetService<InsertUser>();
            await Navigation.PushModalAsync(insertUser);
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}, {ex.InnerException.Message}", "Fechar");
        }
    }
    #endregion
}