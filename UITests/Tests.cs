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
            //app.Repl();

            //System.Threading.Thread.Sleep(8000);
            Console.WriteLine("Waiting for Entry enable");
            app.WaitFor(()=> app.Query(v => v.Marked("EntryURL")).FirstOrDefault().Enabled);

            Console.WriteLine("Entry Enabled");

            app.ClearText("EntryURL");
            app.EnterText("EntryURL","https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
            app.DismissKeyboard();
            app.Tap("Refresh");
            System.Threading.Thread.Sleep(2000);
            app.WaitFor(() => app.Query(v => v.Marked("EntryURL")).FirstOrDefault().Enabled);

            //System.Threading.Thread.Sleep(5000);

            //var countList = app.Query(e => e.Marked("DataList").Descendant().Child()).Length;
            var countList = app.Query(x => x.Marked("ListGrid")).Count();
            Console.WriteLine(countList.ToString());
            app.Screenshot("ListViewAllPresent");
            Assert.Greater(countList, 0);

        }

        [Test]
        public void ListViewNoData()
        {
            Console.WriteLine("Waiting for Entry enable");
            app.WaitFor(() => app.Query(v => v.Marked("EntryURL")).FirstOrDefault().Enabled);
             
            Console.WriteLine("Entry Enabled");
            // System.Threading.Thread.Sleep(8000);
            app.ClearText("EntryURL");
            app.EnterText("EntryURL", "https://dl.dropboxusercontent.com/s/facts.json");
            app.DismissKeyboard();
            app.Tap("Refresh");
            System.Threading.Thread.Sleep(2000);
            app.WaitFor(() => app.Query(v => v.Marked("EntryURL")).FirstOrDefault().Enabled);

             
            //var countList = app.Query(e => e.Marked("DataList").Descendant().Child()).Length;
            var countList = app.Query(x => x.Marked("ListGrid")).Count();

            Console.WriteLine(countList.ToString());

            app.Screenshot("ListViewNoData");
            Assert.Greater(countList, 0);

        }

        [Test]
        public void TestData()
        {
            app.Repl();
        }

        
    }
}
