using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Fridge_Shopping_app;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fridge_Shopping_app
{
    [QueryProperty(nameof(EditedItem), "savedItem")]
    internal partial class ShoppingListPageViewModel : ObservableObject
    {
        public ObservableCollection<FridgeItem> ItemsOnShoppingList { get; set; }
        public FridgeItem? SelectedItem { get; set; }


        public FridgeItem EditedItem
        {
            set
            {
                if (value != null)
                {
                    if (SelectedItem != null)
                    {
                        ItemsOnShoppingList.Remove(SelectedItem);
                    }

                    ItemsOnShoppingList.Add(value);
                }
            }
        }

        string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "items_on_shopping_list.json");

        private FridgeItemsService fridgeService;
        public ShoppingListPageViewModel(FridgeItemsService service, ShoppingListItemsService service1)
        {
            fridgeService = service;
            ItemsOnShoppingList = service1.ItemsOnList;
        }


        // Loading from json file
        public async Task InitCollectionsAsync()
        {
            if (File.Exists(filePath) && ItemsOnShoppingList.Count() == 0)
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var items = JsonSerializer.Deserialize<List<FridgeItem>>(jsonString);
                    foreach (var item in items)
                    {
                        ItemsOnShoppingList.Add(item);
                    }
                }
            }
        }

        // Saving to json file
        public async Task SaveCollectionsAsync()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(ItemsOnShoppingList);
                await File.WriteAllTextAsync(filePath, jsonString);
            }
            catch (Exception e)
            {
                WeakReferenceMessenger.Default.Send("File write error: " + e.Message);
            }
        }

        [RelayCommand]
        async Task NewItemAsync()
        {
            SelectedItem = null;
            var param = new ShellNavigationQueryParameters()
            {
                {"editItem", new FridgeItem() }
            };
            await Shell.Current.GoToAsync(nameof(ShoppingListEditorPage), param);
        }

        [RelayCommand]
        async Task EditItemAsync()
        {
            if (SelectedItem != null)
            {
                var param = new ShellNavigationQueryParameters()
                {
                    {"editItem", SelectedItem.GetCopy() }
                };
                await Shell.Current.GoToAsync(nameof(ShoppingListEditorPage), param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new AlertMessage("Select an item to edit!"));
            }
        }

        [RelayCommand]
        async Task DeleteItemAsync()
        {
            if (SelectedItem != null)
            {
                await Task.Run(() => ItemsOnShoppingList.Remove(SelectedItem));
                SelectedItem = null;
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new AlertMessage("Select an item to delete!"));
            }
        }

        [RelayCommand]
        async Task AddItemToFridgeAsync()
        {
            if (SelectedItem != null)
            {
                fridgeService.ItemsInFridge.Add(SelectedItem);
                await Task.Run(() => ItemsOnShoppingList.Remove(SelectedItem));
                SelectedItem = null;
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new AlertMessage("Select an item to add to fridge!"));
            }
        }


        // Sharing the shopping list
        [RelayCommand]
        async Task ShareShoppingListAsync()
        {
            // Checking for network connectivity
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                WeakReferenceMessenger.Default.Send(
                    new AlertMessage("No internet connection. Cannot share shopping list.")
                );
                return;
            }
            // Only sharing the file if there is network connectivity
            else
            {
                var sb = new StringBuilder();
                sb.AppendLine("Shopping List:");

                foreach (var item in ItemsOnShoppingList)
                {
                    sb.AppendLine($"• {item.Name} — {item.Quantity}");
                }

                await Share.Default.RequestAsync(new ShareTextRequest
                {
                    Text = sb.ToString(),
                    Title = "Shopping List"
                });
            }
        }

    }
}
