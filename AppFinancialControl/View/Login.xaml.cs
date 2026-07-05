using AppFinancialControl.Models;
using AppFinancialControl.Service;
using Plugin.FirebaseAuth;

namespace AppFinancialControl.View;

public partial class Login : ContentPage
{
    IUserService _service;

	public Login(IUserService userService)
	{
		InitializeComponent();
        _service = userService;
	}

    #region Button_clicked_Login
    /// <summary>
    /// Button event Login
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void Button_Clicked_Login(object sender, EventArgs e)
    {
        try
        {
            UserLogin login = new UserLogin()
            {
                Login = txtEmail.Text ?? 
                throw new ArgumentNullException("Email ou nome do usuário é obrigatorio"),
                Password = txtPassoword.Text ?? 
                throw new ArgumentNullException("Senha é obrigatorio"),
            };

            User user = await _service.Login(login);

            if (user != null)
            {
                UserSession.Id = user.Id;
                UserSession.Login = user.Login;
                UserSession.Password = user.Password;
                UserSession.Token = user.Token;

                await SecureStorage.SetAsync("userId", UserSession.Id.ToString());
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                throw new ArgumentNullException("Usuário não encontrado");
            }
        }
        catch(NullReferenceException nre)
        {
            DisplayAlert("Erro", nre.Message, "Fechar");
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Erro",ane.ParamName, "Fechar");
        }
        catch(ArgumentException ae)
        {
            DisplayAlert("Erro", ae.ParamName, "Fechar");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region ImageButton_Clicked_IsSeePassword
    /// <summary>
    /// Método responsável mudar a imagem e o tipo de caracteri do txtPassword
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ImageButton_Clicked_IsSeePassword(object sender, EventArgs e)
    {
        if(txtPassoword.IsPassword)
        {
            txtPassoword.IsPassword = false;
            btnImageSee.Source = "olho.png";
        }
        else
        {
            txtPassoword.IsPassword = true;
            btnImageSee.Source = "olho_fechado.png";
        }
    }
    #endregion

    #region ImageButton_Clicked_EntryGoogle
    private async void Button_Clicked_EntryGoogle(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region ImageButton_Clicked_EntryGithub
    private void Button_Clicked_EntryGithub(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region ImageButton_Clicked_EntryFacebook
    private void Button_Clicked_EntryFacebook(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region Button_Clicked_EntryForApp
    private void Button_Clicked_EntryForApp(object sender, EventArgs e)
    {
        try
        {
            InsertClient client = this.Handler.MauiContext.Services.GetService<InsertClient>();
            Navigation.PushModalAsync(client);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region Clicked_Enter_Button
    private void Clicked_Enter_Button(object sender, EventArgs e)
    {
        Button_Clicked_Login(sender, e);
    }
    #endregion
}