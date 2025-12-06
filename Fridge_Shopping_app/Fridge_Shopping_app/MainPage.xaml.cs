using CommunityToolkit.Mvvm.Messaging;

namespace Fridge_Shopping_app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Register recipient to alert messages
            WeakReferenceMessenger.Default.Register<MainPage, AlertMessage>(
            this, (r, msg) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    DisplayAlert("Notice", msg.Value, "OK");
                });
            });
        }

        private async void ContentPage_Loaded(object sender, EventArgs e)
        {
            if (this.BindingContext is MainPageViewModel vm)
            {
                await vm.InitCollectionsAsync();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (this.BindingContext is MainPageViewModel vm)
            {
                vm.SaveCollectionsAsync();
            }
        }
    }

}
