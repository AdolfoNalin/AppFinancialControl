using Microsoft.Extensions.DependencyInjection;
using AppFinancialControl.View;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        public App(TransactionList listPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(listPage);
        }
    }
}