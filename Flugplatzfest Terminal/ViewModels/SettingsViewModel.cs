using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Services;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string menuText;
        private App app;

        public ICommand NavigateBackCommand { get; }
        public ICommand UseMenuCommand { get; }

        public SettingsViewModel(App app, NavigationService terminalViewNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(terminalViewNavigationService);
            UseMenuCommand = new UseMenuCommand(app, this);
            this.app = app;
        }

        public string MenuString
        {
            get => menuText;
            set
            {
                menuText = value;
                OnPropertyChanged(nameof(MenuString));
            }
        }

        public App App { get; }
    }
}