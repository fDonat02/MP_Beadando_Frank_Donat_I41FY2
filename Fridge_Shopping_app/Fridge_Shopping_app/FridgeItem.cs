using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge_Shopping_app
{
    public partial class FridgeItem : ObservableObject
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        float quantity = 1;

        [ObservableProperty]
        DateTime exp_date = DateTime.Today;

        public bool IsNearExpiration => Exp_date.Date <= DateTime.Today.AddDays(3);

        public FridgeItem GetCopy()
        {
            return (FridgeItem)this.MemberwiseClone();
        }
    }
}
