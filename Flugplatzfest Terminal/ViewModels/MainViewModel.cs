using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel(ChatList chatList, Interface inter)
        {
            CurrentViewModel = new TerminalViewModel(chatList, inter);
        }
    }
}