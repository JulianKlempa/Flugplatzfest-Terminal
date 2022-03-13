using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Flugplatzfest_Terminal.ViewModels
{
    public class TerminalViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ChatViewModel> chats;
        public IEnumerable<ChatViewModel> Chats => chats;

        public TerminalViewModel()
        {
            chats = new ObservableCollection<ChatViewModel>();
        }
    }
}