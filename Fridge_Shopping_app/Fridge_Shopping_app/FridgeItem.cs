using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge_Shopping_app
{
    internal partial class FridgeItem : ObservableObject
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        float quantity;

        [ObservableProperty]
        DateOnly exp_date;
    }
}
