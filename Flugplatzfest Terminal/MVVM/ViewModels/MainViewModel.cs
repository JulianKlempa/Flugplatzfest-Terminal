using Flugplatzfest_Terminal.MVVM.Stores;

namespace Flugplatzfest_Terminal.MVVM.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private NavigationStore navigationStore;

        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        public MainViewModel(NavigationStore navigationStore)
        {
            this.navigationStore = navigationStore;
            navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}