using Caliburn.Micro;

namespace eWads.ViewModels
{
    public class AppShellViewModel : Conductor<object>
    {
        public AppShellViewModel()
        {
            ActivateItemAsync(new LoginPageViewModel());
        }
    }
}
