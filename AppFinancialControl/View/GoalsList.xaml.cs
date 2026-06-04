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
}