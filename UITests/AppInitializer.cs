using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TelstraPOC.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.StartApp();
            }

            return ConfigureApp.iOS.DeviceIdentifier("80641746-B20C-42F2-9975-D925960CAB75").InstalledApp("com.companyname.TelstraPOC").StartApp();
        }
    }
}
