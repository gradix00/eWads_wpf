using Caliburn.Micro;

namespace eWads.ViewModels
{
    public class AppShellViewModel : Conductor<object>
    {
        public static string Title { get; set; }

        public AppShellViewModel()
        {
            ActivateItemAsync(new LoginPageViewModel());
        }
    }
}
