using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;

namespace AppFinancialControl.View;

public partial class GoalsEdit : ContentPage
{
    private readonly IGoalsService _service;
    private Goals _goals;
    public GoalsEdit(IGoalsService service)
    {
        _service = service;
        InitializeComponent();
    }

    #region SetGoalsScreenEdit
    /// <summary>
    /// 
    /// </summary>
    /// <param name="goals"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void SetGoalsScreenEdit(Goals goals)
    {
        try
        {
            _goals = goals;
            if (goals is null)
            {
                throw new ArgumentNullException("Meta está vazia");
            }
            else
            {
                txtTitle.Text = goals.Title;
                dpDate.Date = goals.Date;
                txtValue.Text = goals.Value.ToString("C");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

    #region Button_Clicked_SaveGoals
    private void Button_Clicked_SaveGoals(object sender, EventArgs e)
    {
        try
        {
            Goals goals = new Goals()
            {
                Id = _goals.Id,
                UserId = UserSession.Id,
                Title = txtTitle.Text ?? throw new ArgumentNullException("O Titulo é obrigatório"),
                Date = DateTime.Parse(dpDate.Date.Value.Date.ToString("D") ?? throw new ArgumentNullException("É necessário a data")),
                Value = Math.Abs(float.Parse(txtValue.Text.Replace("R$", "") ?? throw new ArgumentNullException("É necessário o campo Valor")))
            };

            DisplayAlert("Meta cadastrada", _service.Update(goals), "Fechar");

            WeakReferenceMessenger.Default.Send("");

            Navigation.PopAsync();
        }
        catch (ArgumentNullException ane)
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