using Flugplatzfest_Terminal.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Flugplatzfest_Terminal.Commands
{
    public class UseMenuCommand : CommandBase
    {
        private App app;
        private readonly SettingsViewModel settingsViewModel;

        public UseMenuCommand(App app, SettingsViewModel settingsViewModel)
        {
            this.app = app;
            this.settingsViewModel = settingsViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(settingsViewModel.MenuString))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            if (!string.IsNullOrEmpty(settingsViewModel.MenuString))
            {
                app.SetMenu(settingsViewModel.MenuString);
                MessageBox.Show("Speisekarte wurde gesetzt.");
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(settingsViewModel.MenuString) && base.CanExecute(parameter);
        }
    }
}