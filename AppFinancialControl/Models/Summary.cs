using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

public class Summary
{
    public static ISeries[] Series { get; set; } =
    [
        new PieSeries<double>
        {
            Values = [10],
            Name = "Alimentação"
        },

        new PieSeries<double>
        {
            Values = [25],
            Name = "Transporte"
        },

        new PieSeries<double>
        {
            Values = [40],
            Name = "Lazer"
        }
    ];

    public static ISeries[] GetSeries()
    {
        try
        {
            return Series;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}