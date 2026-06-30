using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppFinancialControl.View;

public partial class GoalsAdd : ContentPage
{
    private IGoalsService _service;
    private string _name;
	public GoalsAdd(IGoalsService service)
	{
        _service = service;
		InitializeComponent();
	}

    #region Button_Clicked_SaveGoals
    /// <summary>
    /// Event responsable for Save the Goals in database Localhost
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Button_Clicked_SaveGoals(object sender, EventArgs e)
    {
        try
        {
            Goals goals = new Goals()
            {
                UserId = UserSession.Id,
                Title = txtTitle.Text ?? throw new ArgumentNullException("O Titulo é obrigatório"),
                Description = txtDescription.Text ?? throw new ArgumentNullException("A descrição é obrigatório"),
                Date = DateTime.Parse(dpDate.Date.Value.Date.ToString("D") ?? throw new ArgumentNullException("É necessário a data")),
                Value = float.Parse(txtValue.Text)
            };

            DisplayAlert("Meta cadastrada", _service.Insert(goals), "Fechar");

            WeakReferenceMessenger.Default.Send("");

            Navigation.PopAsync();
        }
        catch(ArgumentNullException ane)
        {
            DisplayAlert("Verifique os campos", ane.ParamName, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.StackTrace}, {ex.Message}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region TapGestureRecognizer_Tapped
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion
}