using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fridge_Shopping_app
{
    public class FridgeItemsService
    {
        public ObservableCollection<FridgeItem> ItemsInFridge { get; } = new ObservableCollection<FridgeItem>();
    }

}
