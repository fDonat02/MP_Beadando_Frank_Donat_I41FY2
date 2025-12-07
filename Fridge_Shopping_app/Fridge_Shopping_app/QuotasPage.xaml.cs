using CommunityToolkit.Mvvm.Messaging;

namespace Fridge_Shopping_app
{
    public partial class QuotasPage : ContentPage
    {
        public QuotasPage(FridgeItemsService service, ShoppingListItemsService service1)
        {
            BindingContext = new QuotasPageViewModel(service, service1);
            InitializeComponent();
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            if (this.BindingContext is QuotasPageViewModel vm)
            {
                await vm.InitCollectionsAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (this.BindingContext is QuotasPageViewModel vm)
            {
                vm.SaveCollectionsAsync();
            }
        }
    }
}