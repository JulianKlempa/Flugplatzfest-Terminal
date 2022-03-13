using Flugplatzfest_Terminal.Stores;
using Flugplatzfest_Terminal.ViewModels;
using System;

namespace Flugplatzfest_Terminal.Services
{
    public class NavigationService
    {
        private NavigationStore navigationStore;
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