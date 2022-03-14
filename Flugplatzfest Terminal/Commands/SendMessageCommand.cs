using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.ViewModels;

namespace Flugplatzfest_Terminal.Commands
{
    public class SendMessageCommand : CommandBase
    {
        private readonly Interface inter;
        private readonly TerminalViewModel terminalViewModel;

        public SendMessageCommand(TerminalViewModel terminalViewModel, Interface inter)
        {
            this.inter = inter;
            this.terminalViewModel = terminalViewModel;
            terminalViewModel.PropertyChanged += TerminalViewModel_PropertyChanged;
        }

        private void TerminalViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(terminalViewModel.SendMessageText):
                case nameof(terminalViewModel.SelectedChatViewModel):
                    OnCanExecuteChanged();
                    break;
            }
        }

        public override void Execute(object parameter)
        {
            if (terminalViewModel.GetSendMessage() != null && terminalViewModel.GetSendMessage() != "" && terminalViewModel.GetCurrentChat() != null)
            {
                inter.SendMessage(new TextMessage(terminalViewModel.GetSendMessage(), terminalViewModel.GetCurrentChat().GetChatId(), MessageDirection.outgoing));
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(terminalViewModel.GetSendMessage()) && terminalViewModel.GetCurrentChat() != null && base.CanExecute(parameter);
        }
    }
}