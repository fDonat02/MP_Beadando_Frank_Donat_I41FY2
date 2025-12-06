namespace Fridge_Shopping_app
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FridgeEditorPage), typeof(FridgeEditorPage));
        }
    }
}
