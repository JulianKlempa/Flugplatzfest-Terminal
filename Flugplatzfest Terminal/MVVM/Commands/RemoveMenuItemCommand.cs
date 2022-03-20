using Flugplatzfest_Terminal.MVVM.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Flugplatzfest_Terminal.MVVM.Commands
{
    public class RemoveMenuItemCommand : CommandBase
    {
        private readonly SettingsViewModel settingsViewModel;

        public RemoveMenuItemCommand(SettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(settingsViewModel.SelectedMenuItemViewModel))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            if (MessageBox.Show("Sind Sie sicher, dass sie das Element löschen wollen?", "Löschen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                settingsViewModel.Menu.Remove(settingsViewModel.SelectedMenuItemViewModel);
            }
        }

        public override bool CanExecute(object parameter)
        {
            return settingsViewModel.SelectedMenuItemViewModel != null && base.CanExecute(parameter);
        }
    }
}