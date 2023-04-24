using Caliburn.Micro;
using eWads.Views;
using eWadsLib;
using System.Threading.Tasks;
using System.Windows;

namespace eWads.ViewModels
{
    public class LoginPageViewModel : Conductor<object>
    {
        //Data user - inputs
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string Info { get; set; }

        //LoginText - show text "logging in" when in process login to services
        private Visibility _loginText = Visibility.Hidden;
        public Visibility LoginText
        {
            get { return _loginText; }
            set
            {
                _loginText = value;
                NotifyOfPropertyChange(() => LoginText);
            }
        }

        //Login panel - forms
        private Visibility _loginPanel = Visibility.Visible;
        public Visibility LoginPanel
        {
            get { return _loginPanel; }
            set
            {
                _loginPanel = value;
                NotifyOfPropertyChange(() => LoginPanel);
            }
        }

        public LoginPageViewModel() => AppShellViewModel.SetTitle("eWads - login");

        public async Task LoginToServicesAsync()
        {
            LoginPanel = Visibility.Hidden;
            LoginText = Visibility.Visible;

            Authentication auth = new Authentication();
            bool res = await Task.Run(() => auth.InitLogin(Email, Pwd));
            if (res)
            {
                var getUser = await UserService.GetInfoUser(Email);
                AppShellViewModel.SetPage(new UserPanelViewModel(getUser), "eWads - News");
            }
            else
                Info = "Incorrect email or password!";

            LoginPanel = Visibility.Visible;
            LoginText = Visibility.Hidden;
        }
    }
}
