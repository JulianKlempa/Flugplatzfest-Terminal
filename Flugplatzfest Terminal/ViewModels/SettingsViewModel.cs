using Flugplatzfest_Terminal.Commands;
using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Stores;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string menuText;

        public ICommand NavigateBackCommand { get; }
        public ICommand UseMenuCommand { get; }

        public SettingsViewModel(App app, NavigationStore navigationStore, Interface inter, ChatList chatList)
        {
            NavigateBackCommand = new NavigateBackCommand(navigationStore, inter, chatList, app);
            UseMenuCommand = new UseMenuCommand(app, this);
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
    }
}