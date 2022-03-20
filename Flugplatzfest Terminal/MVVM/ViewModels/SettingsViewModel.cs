using Flugplatzfest_Terminal.MVVM.Commands;
using Flugplatzfest_Terminal.MVVM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Flugplatzfest_Terminal.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ICommand NavigateBackCommand { get; }
        public ICommand SaveMenuCommand { get; }
        public ICommand AddMenuItemCommand { get; }
        public ICommand RemoveMenuItemCommand { get; }

        public ObservableCollection<MenuItemViewModel> Menu { get; set; }

        private readonly List<MenuItemViewModel> menu;
        private readonly App app;
        private MenuItemViewModel selectedMenuItemViewModel;
        private string newItemName;
        private double newItemPriceDouble;
        private string newItemPrice;
        private ComboBoxItem newItemComboBox;
        private bool botActive = true;

        public SettingsViewModel(App app, NavigationService terminalViewNavigationService)
        {
            this.app = app;
            menu = new List<MenuItemViewModel>();
            LoadMenu();
            Menu = new ObservableCollection<MenuItemViewModel>(menu);
            Menu.CollectionChanged += Menu_CollectionChanged;
            NavigateBackCommand = new NavigateCommand(terminalViewNavigationService);
            SaveMenuCommand = new SaveMenuCommand(app, this);
            AddMenuItemCommand = new AddMenuItemCommand(this);
            RemoveMenuItemCommand = new RemoveMenuItemCommand(this);
            //TODO TelegramToken
            //TODO Add reorder Menu
        }

        private void Menu_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Menu));
            CollectionViewSource.GetDefaultView(Menu).Refresh();
        }

        private void LoadMenu()
        {
            menu.Clear();
            foreach (Model.Menu.MenuItem menuItem in app.GetMenu().GetMenu())
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
                OnPropertyChanged(nameof(SelectedMenuItemViewModel));
            }
        }

        public string NewItemName
        {
            get => newItemName;
            set
            {
                newItemName = value;
                OnPropertyChanged(nameof(NewItemName));
            }
        }

        public string NewItemPrice
        {
            get => newItemPrice;
            set
            {
                if (newItemPrice != value && !value.Contains("."))
                {
                    try
                    {
                        newItemPriceDouble = value != "" ? Convert.ToDouble(value) : 0.0;
                        newItemPrice = value;
                        OnPropertyChanged(nameof(NewItemPrice));
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public bool BotActive
        {
            get => botActive;
            set
            {
                botActive = value;
                app.SetBotActive(botActive);
                OnPropertyChanged(nameof(BotActive));
            }
        }

        public ComboBoxItem SelectedMenuType
        {
            get => newItemComboBox;
            set
            {
                newItemComboBox = value;
                OnPropertyChanged(nameof(SelectedMenuType));
            }
        }

        public double GetNewItemPrice()
        {
            return newItemPriceDouble;
        }
    }
}