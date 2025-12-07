using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge_Shopping_app
{
    public class ShoppingListItemsService
    {
        public ObservableCollection<FridgeItem> ItemsOnList { get; } = new ObservableCollection<FridgeItem>();
    }

}
