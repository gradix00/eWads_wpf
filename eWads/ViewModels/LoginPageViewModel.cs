using Caliburn.Micro;
using eWads.Views;
using eWadsLib;
using System.Threading.Tasks;
using System.Windows;

namespace eWads.ViewModels
{
    public class LoginPageViewModel : Conductor<object>
    {
        public string Email { get; set; }
        public string Pwd { get; set; }

        public LoginPageViewModel() 
        {
            AppShellViewModel.Title = "eWads - login";
        }

        public async Task LoginToServicesAsync()
        {
            Authentication auth = new Authentication();
            bool res = await Task.Run(() => auth.InitLogin(Email, Pwd));
            if (res)
                MessageBox.Show("logged In!");
            else
                MessageBox.Show("Incorrect data!");
        }
    }
}
