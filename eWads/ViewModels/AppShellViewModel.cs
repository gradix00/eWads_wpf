using Caliburn.Micro;

namespace eWads.ViewModels
{
    public class AppShellViewModel : Conductor<object>
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    NotifyOfPropertyChange(() => Title);
                }
            }
        }
        public static AppShellViewModel AppShell { get; set; }

        public AppShellViewModel()
        {
            AppShell = this;
            ActivateItemAsync(new LoginPageViewModel());
        }

        public static void SetTitle(string title) => AppShell.Title = title;

        public static void SetPage(object page, string pageTitle)
        {
            AppShell.ActivateItemAsync(page);
            SetTitle(pageTitle);
        }
    }
}
