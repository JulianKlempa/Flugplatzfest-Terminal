﻿using Flugplatzfest_Terminal.MVVM.Commands;
using Flugplatzfest_Terminal.MVVM.Services;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private string menuText;

        public ICommand NavigateBackCommand { get; }
        public ICommand SaveMenuCommand { get; }

        public SettingsViewModel(App app, NavigationService terminalViewNavigationService)
        {
            NavigateBackCommand = new NavigateCommand(terminalViewNavigationService);
            SaveMenuCommand = new SaveMenuCommand(app, this);
            //TODO TelegramToken
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