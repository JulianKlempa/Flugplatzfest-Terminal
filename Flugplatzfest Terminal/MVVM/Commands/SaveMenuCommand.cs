using Flugplatzfest_Terminal.MVVM.ViewModels;
using System.ComponentModel;

namespace Flugplatzfest_Terminal.MVVM.Commands
{
    public class SaveMenuCommand : CommandBase
    {
        private readonly App app;
        private readonly SettingsViewModel settingsViewModel;

        public SaveMenuCommand(App app, SettingsViewModel settingsViewModel)
        {
            this.app = app;
            this.settingsViewModel = settingsViewModel;
            settingsViewModel.PropertyChanged += SettingsViewModel_PropertyChanged;
        }

        private void SettingsViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(settingsViewModel.Menu))
            {
                OnCanExecuteChanged();
            }
        }

        public override void Execute(object parameter)
        {
            //TODO
        }

        public override bool CanExecute(object parameter)
        {
            return settingsViewModel.Menu.Count >= 1 && base.CanExecute(parameter);
        }
    }
}