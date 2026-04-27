using AppFinancialControl.View;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        public App(TransactionList listPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Login());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Height = 600;
            window.Width = 300;
            return window;
        }
    }
}