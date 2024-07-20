using PM2E2GRUPO3.Views;
namespace PM2E2GRUPO3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new EditUbicacion());
        }
    }
}
