using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using TelstraPOC.Models;
using TelstraPOC.ViewModels;
namespace TelstraPOC.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;
        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);

        }

        [Test]
        public void ListViewAllDataPresent()
        {
            System.Threading.Thread.Sleep(8000);
            app.WaitForElement(x => x.Marked("Beavers"));
        }

        [Test]
        public void ListViewNoData()
        {
            System.Threading.Thread.Sleep(8000);
            app.WaitForElement(x => x.Marked("HCL"));
        }

        [Test]
        public void TestData()
        {
            app.Repl();
        }

        
    }
}
