﻿using Flugplatzfest_Terminal.Model.Interfaces;
using Flugplatzfest_Terminal.Model.Messages;
using Flugplatzfest_Terminal.Stores;
using Flugplatzfest_Terminal.ViewModels;

namespace Flugplatzfest_Terminal.Commands
{
    public class NavigateSettingsCommand : CommandBase
    {
        private NavigationStore navigationStore;
        private Interface inter;
        private ChatList chatList;
        private App app;

        public NavigateSettingsCommand(NavigationStore navigationStore, Interface inter, ChatList chatList, App app)
        {
            this.navigationStore = navigationStore;
            this.inter = inter;
            this.chatList = chatList;
            this.app = app;
        }

        public override void Execute(object parameter)
        {
            navigationStore.CurrentViewModel = new SettingsViewModel(app, navigationStore, inter, chatList);
        }
    }
}