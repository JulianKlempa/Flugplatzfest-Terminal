using Flugplatzfest_Terminal.Services;

namespace Flugplatzfest_Terminal.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            navigationService.Navigate();
        }
    }
}