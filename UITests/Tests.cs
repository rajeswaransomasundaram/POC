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
            app.ClearText("EntryURL");
            app.EnterText("EntryURL","https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
            app.Tap("Refresh");

            System.Threading.Thread.Sleep(5000);
 
            var countList = app.Query(e => e.Marked("DataList").Descendant().Child()).Length;
             
            Console.WriteLine(countList.ToString());
            app.Screenshot("ListViewAllPresent");
            Assert.AreNotEqual(countList, 0);
            
        }

        [Test]
        public void ListViewNoData()
        {

            System.Threading.Thread.Sleep(8000);
            app.ClearText("EntryURL");
            app.EnterText("EntryURL", "https://dl.dropboxusercontent.com/s/facts.json");
            app.Tap("Refresh");

            System.Threading.Thread.Sleep(5000);

            var countList = app.Query(e => e.Marked("DataList").Descendant().Child()).Length;

            Console.WriteLine(countList.ToString());

            app.Screenshot("ListViewNoData");
            Assert.AreNotEqual(countList, 0);

        }


        
    }
}
