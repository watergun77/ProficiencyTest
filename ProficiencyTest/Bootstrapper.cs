using Caliburn.Micro;
using ProficiencyTest.ViewModels;
using System.Windows;

namespace ProficiencyTest
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}
