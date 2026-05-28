using AppFinancialControl.Service;
using AppFinancialControl.View;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        IUserService _service;
        public App(Login login)
        {
            InitializeComponent();

            MainPage = new NavigationPage(login);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Height = 700;
            window.Width = 300;
            return window;
        }
    }
}