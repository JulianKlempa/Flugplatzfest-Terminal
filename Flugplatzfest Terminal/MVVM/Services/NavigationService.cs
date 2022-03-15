using Flugplatzfest_Terminal.MVVM.Stores;
using Flugplatzfest_Terminal.MVVM.ViewModels;
using System;

namespace Flugplatzfest_Terminal.MVVM.Services
{
    public class NavigationService
    {
        private readonly NavigationStore navigationStore;
        private readonly Func<ViewModelBase> createViewModel;

        public NavigationService(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            this.navigationStore = navigationStore;
            this.createViewModel = createViewModel;
        }

        public void Navigate()
        {
            navigationStore.CurrentViewModel = createViewModel();
        }
    }
}