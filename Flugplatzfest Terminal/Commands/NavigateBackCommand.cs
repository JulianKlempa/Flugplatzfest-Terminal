using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Stores;
using Flugplatzfest_Terminal.ViewModels;

namespace Flugplatzfest_Terminal.Commands
{
    public class NavigateBackCommand : CommandBase
    {
        private NavigationStore navigationStore;
        private Interface inter;
        private ChatList chatList;
        private App app;

        public NavigateBackCommand(NavigationStore navigationStore, Interface inter, ChatList chatList, App app)
        {
            this.navigationStore = navigationStore;
            this.inter = inter;
            this.chatList = chatList;
            this.app = app;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new TerminalViewModel(chatList, inter, navigationStore, app);
        }
    }
}