using ObjectsFall.View;
using Xamarin.Forms;
using ObjectsFall.Database;
using System.IO;
using System;

namespace ObjectsFall
{
    public partial class App : Application
    {
      
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
