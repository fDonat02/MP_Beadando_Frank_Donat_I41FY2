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
    internal partial class FridgePageViewModel : ObservableObject
    {
        public ObservableCollection<FridgeItem> ItemsInFridge { get; set; }
        public FridgeItem? SelectedItem { get; set; }

        public FridgeItem EditedItem
        {
            set
            {
                if (value != null)
                {
                    if (SelectedItem != null)
                    {
                        ItemsInFridge.Remove(SelectedItem);
                    }
                    ItemsInFridge.Add(value);
                }
            }
        }

        string filePath = Path.Combine(FileSystem.Current.AppDataDirectory, "items_in_fridge.json");

        public FridgePageViewModel()
        {
            ItemsInFridge = new ObservableCollection<FridgeItem>();
        }

        public async Task InitCollectionsAsync()
        {
            if (File.Exists(filePath) && ItemsInFridge.Count() == 0)
            {
                string jsonString = await File.ReadAllTextAsync(filePath);
                if (!string.IsNullOrEmpty(jsonString))
                {
                    var items = JsonSerializer.Deserialize<List<FridgeItem>>(jsonString);
                    foreach (var item in items)
                    {
                        ItemsInFridge.Add(item);
                    }
                }
            }
        }

        public async Task SaveCollectionsAsync()
        {
            try
            {
                string jsonString = JsonSerializer.Serialize(ItemsInFridge);
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
            await Shell.Current.GoToAsync(nameof(EditorPage), param);
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
                await Shell.Current.GoToAsync(nameof(EditorPage), param);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Select an item to edit!");
            }
        }

        [RelayCommand]
        async Task DeleteItemAsync()
        {
            if (SelectedItem != null)
            {
                await Task.Run(() => ItemsInFridge.Remove(SelectedItem));
                SelectedItem = null;
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Select an item to delete!");
            }
        }
    }
}
