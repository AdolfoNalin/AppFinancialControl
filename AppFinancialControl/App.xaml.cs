using Microsoft.Extensions.DependencyInjection;
using AppFinancialControl.View;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new TransactionAdd());
        }
    }
}