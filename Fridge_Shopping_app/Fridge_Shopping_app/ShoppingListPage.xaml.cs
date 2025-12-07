using CommunityToolkit.Mvvm.Messaging;

namespace Fridge_Shopping_app
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage(FridgeItemsService service, ShoppingListItemsService service1)
        {
            BindingContext = new ShoppingListPageViewModel(service, service1);
            InitializeComponent();
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            if (this.BindingContext is ShoppingListPageViewModel vm)
            {
                await vm.InitCollectionsAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (this.BindingContext is ShoppingListPageViewModel vm)
            {
                vm.SaveCollectionsAsync();
            }
        }
    }
}