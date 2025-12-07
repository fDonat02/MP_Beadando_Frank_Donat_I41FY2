using CommunityToolkit.Mvvm.Messaging;

namespace Fridge_Shopping_app
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage(FridgeItemsService service)
        {

            BindingContext = new ShoppingListPageViewModel(service);
            InitializeComponent();
            /*
            // Register recipient to alert messages
            WeakReferenceMessenger.Default.Register<ShoppingListPage, AlertMessage>
                (
                this, (r, msg) =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        DisplayAlert("Notice", msg.Value, "OK");
                    });
                });*/
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