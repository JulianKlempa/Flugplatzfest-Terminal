using Flugplatzfest_Terminal.MVVM.Model.Menu;
using Flugplatzfest_Terminal.MVVM.ViewModels;
using System.ComponentModel;

namespace Flugplatzfest_Terminal.MVVM.Commands
{
    public class AddMenuItemCommand : CommandBase
    {
        private readonly SettingsViewModel settingsViewModel;

        public AddMenuItemCommand(SettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(settingsViewModel.NewItemName) || e.PropertyName == nameof(settingsViewModel.SelectedMenuType))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            MenuItemType menuItemType;
            switch (settingsViewModel.SelectedMenuType.Tag)
            {
                case "Drink":
                    menuItemType = MenuItemType.Drink;
                    break;

                default:
                    menuItemType = MenuItemType.Food;
                    break;
            }
            settingsViewModel.Menu.Add(new MenuItemViewModel(new MenuItem(settingsViewModel.GetNewItemPrice(), settingsViewModel.NewItemName, menuItemType)));
            settingsViewModel.NewItemName = "";
            settingsViewModel.NewItemPrice = "";
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(settingsViewModel.NewItemName) && settingsViewModel.SelectedMenuType != null && base.CanExecute(parameter);
        }
    }
}