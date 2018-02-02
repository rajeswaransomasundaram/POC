using Xamarin.Forms;
using TelstraPOC.Views;
namespace TelstraPOC
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MyDataListPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
