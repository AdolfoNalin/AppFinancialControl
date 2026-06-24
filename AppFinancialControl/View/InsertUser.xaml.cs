using AppFinancialControl.Models;
using AppFinancialControl.Service;

namespace AppFinancialControl.View;

public partial class InsertUser : ContentPage
{
    private readonly IUserService _service;
    public InsertUser(IUserService service)
    {
        _service = service;
        InitializeComponent();
    }

    #region Button_Clicked_Cancel
    private void Button_Clicked_Cancel(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    #endregion

    #region Button_Clicked_Finish
    private async void Button_Clicked_Finish(object sender, EventArgs e)
    {
        try
        {
            if (txtPassword.Text.Contains(txtRepeatPassword.Text))
            {
                User user = new User()
                {
                    Login = txtLogin.Text ?? throw new ArgumentNullException("Digite um login"),
                    Password = txtPassword.Text ?? throw new ArgumentNullException("Digite a senha"),
                };

                string message = await _service.Insert(user);

                DisplayAlert("Cadastrado com Sucesso", message, "Fechar");

                UserLogin userLogin = new UserLogin()
                {
                    Login = user.Login,
                    Password = user.Password,
                };

                Login login = this.Handler.MauiContext.Services.GetService<Login>();
                Navigation.PushModalAsync(login);
            }
            else
            {
                DisplayAlert("Erro", "A senhas não estão exatamente iquais", "Fechar");
            }
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Erro", ane.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.InnerException}", "Fechar");
        }
    }
    #endregion

    private void Entry_Completed_Enter(object sender, EventArgs e)
    {
        Button_Clicked_Finish(sender, e);
    }

    private void Entry_TextChanged_VerificationPassword(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (!txtPassword.Text.Contains(txtRepeatPassword.Text))
            {
                throw new ArgumentException("Senha não está correta");
            }
        }
        catch (ArgumentException ae)
        {
            DisplayAlert("Error", ae.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }

    }
}