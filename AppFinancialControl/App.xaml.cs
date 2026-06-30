using AppFinancialControl.Models;
using AppFinancialControl.Service;
using AppFinancialControl.View;
using System.Runtime.CompilerServices;

namespace AppFinancialControl
{
    public partial class App : Application
    {
        private User _user = null;
        private IUserService _service;
        public App(IUserService service)
        {
            _service = service;
            try
            {
                InitializeComponent();
                GetUser();

                if (_user == null)
                {
                    Login login = new Login(_service);
                    MainPage = login;
                }
                else
                {
                    UserSession.Id = _user.Id;
                    UserSession.Login = _user.Login;
                    UserSession.Password = _user.Password;
                    UserSession.Token = _user.Token;

                    MainPage = new AppShell();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void GetUser()
        {
            try
            {
                Guid id = Guid.Parse(await SecureStorage.Default.GetAsync("userId") ?? 
                    throw new NullReferenceException());
                if(id != Guid.Empty)
                {
                    _user = _service.GetUser(id);
                }
            }
            catch(NullReferenceException nre)
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Height = 700;
            window.Width = 600;
            return window;

        }
    }
}