using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge_Shopping_app
{
    [QueryProperty(nameof(Item), "editItem")]
    internal partial class QuotasEditorPageViewModel : ObservableObject
    {
        [ObservableProperty]
        FridgeItem item;

        [RelayCommand]
        async Task Save()
        {
            var param = new ShellNavigationQueryParameters()
            {
                {"savedItem", Item}
            };
            await Shell.Current.GoToAsync("..", param);
        }
    }
}
