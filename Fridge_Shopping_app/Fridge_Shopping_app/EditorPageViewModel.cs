using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge_Shopping_app
{
    [QueryProperty(nameof(item), "editItem")]
    internal partial class EditorPageViewModel : ObservableObject
    {
        [ObservableProperty]
        FridgeItem item;

        [RelayCommand]
        async Task Save()
        {
            var param = new ShellNavigationQueryParameters()
            {
                {"savedItem", item }
            };
            await Shell.Current.GoToAsync("..", param);
        }
    }
}
