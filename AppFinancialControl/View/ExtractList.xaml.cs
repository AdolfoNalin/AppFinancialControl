using AppFinancialControl.Models;
using AppFinancialControl.Service;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;

namespace AppFinancialControl.View;

public partial class ExtractList : ContentPage
{
    private readonly ITransactionService _service;
    public ObservableCollection<MonthItem> Months { get; } = new();
	public ExtractList(ITransactionService servuce)
	{
        _service = servuce;
		InitializeComponent();

        AddingDateInCarouseView();

        UpdateData();
        WeakReferenceMessenger.Default.Register<string>(this, (e, message) =>
        {
            UpdateData();
        });
    }

    private MonthItem _selectedMonth;

    public MonthItem SelectedMonth
    {
        get => _selectedMonth;
        set
        {
            if (_selectedMonth != value)
            {
                _selectedMonth = value;

                _service.GetDate(UserSession.Id, value);

                OnPropertyChanged(nameof(SelectedMonth));
            }
        }
    }

    #region AddingDateInCarouseView
    public void AddingDateInCarouseView()
    {
        try
        {
            var current = DateTime.Today;

            for (int i = -12; i <= 12; i++)
            {
                var date = current.AddMonths(i);

                Months.Add(new MonthItem
                {
                    Month = date.Month,
                    Year = date.Year
                });
            }

            cvMonths.ItemsSource = Months;
            cvMonths.Position = 12;
            cvMonths.CurrentItem = SelectedMonth;
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", $"{ex.Message}, {ex.StackTrace}, {ex.HelpLink}", "Fechar");
        }
    }
    #endregion

    #region UpateData
    private void UpdateData()
    {
        try
        {
            MonthItem value = Months[cvMonths.Position];

            ObservableCollection<Transaction> transactions = _service.GetDate(UserSession.Id, value)
                ?? throw new NullReferenceException("Nenhum item encontrado");

            float income = transactions.Where(b => b.Type == TransactionType.Income)
                .Sum(a => a.Value);

            float expensse = transactions.Where(b => b.Type == TransactionType.Expenses)
               .Sum(a => a.Value);

            float balance = income - expensse;

            lblBalance.Text = balance.ToString("C");
            lblIncome.Text = income.ToString("C");
            lblExpense.Text = expensse.ToString("C");

            cvExtract.ItemsSource = transactions;
        }
        catch(NullReferenceException nre)
        {
            DisplayAlert("Erro", nre.Message, "Fechar");
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.InnerException.Message, "Fechar");
        }
    }
    #endregion

    private void UpdateDataDate(object sender, PositionChangedEventArgs e)
    {
        UpdateData();
    }
}