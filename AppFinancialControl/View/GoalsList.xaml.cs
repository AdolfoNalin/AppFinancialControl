using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppFinancialControl.View;

public partial class GoalsList : ContentPage
{
    private readonly IGoalsService _service;
    private ObservableCollection<Goals> _data;
	public GoalsList(IGoalsService service)
	{
        _service = service;
		InitializeComponent();
        UpdateData();
        WeakReferenceMessenger.Default.Register<String>(this, (e, message) =>
        {
            UpdateData();
        });

	}

    #region ImagemClicked_NewGoals
    private void ImagemClicked_NewGoals(object sender, EventArgs e)
    {
        try
        {
            GoalsAdd add = this.Handler.MauiContext.Services.GetService<GoalsAdd>();
            Navigation.PushAsync(add);
        }
        catch (Exception ex)
        {

            throw;
        }
    }
    #endregion

    #region UpdateData
    private void UpdateData()
    {
        try
        {
            _data = _service.GetUserId(UserSession.Id);
            cvGoalsList.ItemsSource = _data;

        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.InnerException.Message, "Fechar");
        }
    }
    #endregion

    #region Tapped_Delete_Goals
    private async void Tapped_Delete_Goals(object sender, TappedEventArgs e)
    {
        try
        {
            bool result = await DisplayAlert("Deletar?", "Deseja excluir essa meta?", "Sim", "Não");

            if (result)
            {
                Goals goalsConvert = e.Parameter as Goals;
                await DisplayAlert("Sucesso", _service.Delete(goalsConvert.Id), "Fechar");
                WeakReferenceMessenger.Default.Send("");
            }

        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.StackTrace}, {ex.Message}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region Tapped_Update_Goals
    private void Tapped_Update_Goals(object sender, TappedEventArgs e)
    {
        try
        {
            Goals goals = e.Parameter as Goals;
            GoalsEdit screen = this.Handler.MauiContext.Services.GetService<GoalsEdit>();
            screen.SetGoalsScreenEdit(goals);

            Navigation.PushAsync(screen);
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion
}