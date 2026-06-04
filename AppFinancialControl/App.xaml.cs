using AppFinancialControl.Service;
using AppFinancialControl.View;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        IUserService _service;
        public App(GoalsList goals)
        {
            InitializeComponent();

            MainPage = new AppShell();
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