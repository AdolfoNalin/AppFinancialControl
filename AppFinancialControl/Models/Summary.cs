using AppFinancialControl.Models;
using AppFinancialControl.Service;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

public class Summary
{
    private ITransactionService _service;
    private double _expenses = 0;
    private double _income = 0;
    private double _balance = 0;
    public ISeries[] Series { get; private set; }
    public Summary(ITransactionService service)
    {
        _service = service;
        GetTransaction();
    }

    private void GetTransaction()
    {
        try
        {
            _expenses = _service.GetAll(UserSession.Id).Where(t => t.Type == TransactionType.Expenses).Sum(t => t.Value);
            _income = _service.GetAll(UserSession.Id).Where(t => t.Type == TransactionType.Income).Sum(t => t.Value);

            _balance = _income - _expenses;

            Series =
            [
                new PieSeries<double>
                {
                    Values = [_expenses],
                    Name = "Gastos"
                },

                new PieSeries<double>
                {
                    Values = [_income],
                    Name = "Receita"
                },

                new PieSeries<double>
                {
                    Values = [_balance],
                    Name = "Saldo"
                }
            ];
        }
        catch(NullReferenceException nre)
        {
            throw nre;
        }
        catch(ArgumentException ae)
        {
            throw ae;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}