using Flugplatzfest_Terminal.MVVM.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace Flugplatzfest_Terminal.MVVM.Commands
{
    public class SaveMenuCommand : CommandBase
    {
        private App app;
        private readonly SettingsViewModel settingsViewModel;

        public SaveMenuCommand(App app, SettingsViewModel settingsViewModel)
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
                app.SaveMenu(settingsViewModel.MenuString);
                MessageBox.Show("Speisekarte wurde gespeichert.");
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(settingsViewModel.MenuString) && base.CanExecute(parameter);
        }
    }
}