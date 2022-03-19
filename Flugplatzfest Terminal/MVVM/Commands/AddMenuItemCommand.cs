using Flugplatzfest_Terminal.MVVM.Model;
using Flugplatzfest_Terminal.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (e.PropertyName == nameof(settingsViewModel.NewItemName))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            settingsViewModel.Menu.Add(new MenuItemViewModel(new MenuItem(settingsViewModel.NewItemPrice, settingsViewModel.NewItemName)));
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(settingsViewModel.NewItemName) && base.CanExecute(parameter);
        }
    }
}