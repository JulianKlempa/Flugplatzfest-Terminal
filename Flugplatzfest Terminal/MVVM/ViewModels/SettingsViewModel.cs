using Flugplatzfest_Terminal.MVVM.Commands;
using Flugplatzfest_Terminal.MVVM.Model;
using Flugplatzfest_Terminal.MVVM.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand SaveMenuCommand { get; }

        public ObservableCollection<MenuItemViewModel> Menu;

        private readonly List<MenuItemViewModel> menu;
        private readonly App app;
        private MenuItemViewModel selectedMenuItemViewModel;

        public SettingsViewModel(App app, NavigationService terminalViewNavigationService)
        {
            this.app = app;
            menu = new List<MenuItemViewModel>();
            LoadMenu();
            Menu = new ObservableCollection<MenuItemViewModel>(menu);
            NavigateBackCommand = new NavigateCommand(terminalViewNavigationService);
            SaveMenuCommand = new SaveMenuCommand(app, this);
            //TODO TelegramToken
        }

        private void LoadMenu()
        {
            menu.Clear();
            foreach (MenuItem menuItem in app.GetMenu().GetMenu())
            {
                menu.Add(new MenuItemViewModel(menuItem));
            }
        }

        public MenuItemViewModel SelectedMenuItemViewModel
        {
            get { return selectedMenuItemViewModel; }
            set
            {
                selectedMenuItemViewModel = value;
                OnPropertyChanged(nameof(Menu));
            }
        }
    }
}