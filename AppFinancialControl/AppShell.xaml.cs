using AppFinancialControl.View;

namespace AppFinancialControl;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();

        //Dispatcher.Dispatch(async () =>
        //{
        //    await Shell.Current.GoToAsync("//home");x
        //});
    }
}