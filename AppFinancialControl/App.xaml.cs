using Microsoft.Extensions.DependencyInjection;
using AppFinancialControl.View;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TransactionList());
        }
    }
}