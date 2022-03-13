using Flugplatzfest_Terminal.Stores;
using Flugplatzfest_Terminal.ViewModels;

namespace Flugplatzfest_Terminal.Commands
{
    public class NavigateSettingsCommand : CommandBase
    {
        private NavigationStore navigationStore;

        public NavigateSettingsCommand(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new SettingsViewModel();
        }
    }
}