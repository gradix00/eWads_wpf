using Caliburn.Micro;
using eWads.ViewModels;
using System.Windows;

namespace eWads
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<AppShellViewModel>();
        }
    }
}
