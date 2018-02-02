using System;
using Xamarin.Forms;
using TelstraPOC.ViewModels;
using TelstraPOC.Views;
using TelstraPOC.Models;
using System.Linq;
using System.Collections.ObjectModel;
using Plugin.Connectivity;
using System.ComponentModel;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace TelstraPOC.ViewModels
{
    public class MyDataListViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private bool asc=true;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        private string _title
        { get; set; }

        private ObservableCollection<MyDataDetails> m_Items { get; set; }
        /// <summary>
        /// Gets or sets the items from JSON feed.
        /// </summary>
        /// <value>The items.</value>
        public ObservableCollection<MyDataDetails> Items
        {
            get{
                return m_Items;
            }
            set
            {
                m_Items = value;
                OnPropertyChanged("Items");
            }
        }


        /// <summary>
        /// Loads the data from JSON to ListView.
        /// </summary>
        private async void LoadData()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                Uri myUri = new Uri(Settings.JsonURL);
                string host = myUri.Host;
                bool hostReachable = await CrossConnectivity.Current.IsRemoteReachable(host);
                if (hostReachable)
                {
                    try
                    {
                        var client = new System.Net.Http.HttpClient();
                        var response = await client.GetStringAsync(Settings.JsonURL);
                        var tr = JsonConvert.DeserializeObject<MainData>(response);

                        Title = tr.Title;
                        Items = tr.Rows;
                    }
                    catch(Exception ex)
                    {
                        MessagingCenter.Send<string,string>("Error", "AppError",ex.Message);
                    }
                }
                else
                {
                    MessagingCenter.Send<string,string>("Error", "AppError","Host not reachable");
                }
            }
            else
            {
                MessagingCenter.Send<string,string>("Error", "AppError","Network Connection not available");
            }

        }

        /// <summary>
        /// Sorts the data.
        /// </summary>
        private void SortData()
        {
            //Items = new ObservableCollection<MyDataDetails>(from data in Items orderby data.Title select data);
            if (asc)
            {
                Items = new ObservableCollection<MyDataDetails>(Items.OrderBy(x => x.Title).ToList());
                OnPropertyChanged("Items");

            }
            else
            {
                Items = new ObservableCollection<MyDataDetails>(Items.OrderByDescending(x => x.Title).ToList());
                OnPropertyChanged("Items");
            }
            asc = !asc;
        }

        /// <summary>
        /// Runs when Refresh button clicked
        /// </summary>
        /// <value>The refresh command.</value>
        public Command RefreshCommand
        {

            get
            {
                return new Command(() => { LoadData(); });
            }
        }

        /// <summary>
        /// Runs when Sort button clicked
        /// </summary>
        /// <value>The sort command.</value>
        public Command SortCommand
        {
            get
            {
                return new Command(() => { SortData(); });
            }
        }

        /// <summary>
        /// Runs when Load button clicked.
        /// </summary>
        /// <value>The load from file command.</value>
        public Command LoadFromFileCommand
        {
            get
            {
                return new Command(() => { LoadFromFile(); });
            }
        }

        /// <summary>
        /// Load list from File System
        /// </summary>
        private void LoadFromFile()
        {
            try
            {
                var assembly = typeof(MyDataListPage).GetTypeInfo().Assembly;
                using (Stream stream = assembly.GetManifestResourceStream("TelstraPOC.facts.json"))
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {

                        var json = reader.ReadToEnd();
                        var data = JsonConvert.DeserializeObject<MainData>(json);
                        Title = data.Title;
                        Items = data.Rows;
                    }
                }
            }
            catch(Exception ex)
            {
                MessagingCenter.Send<string, string>("Error", "AppError", ex.Message); 
            }

        }

        /// <summary>
        /// To update the UI.
        /// </summary>
        /// <param name="name">Name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        } 
        public MyDataListViewModel()
        {
            
            LoadData();
        }
    }
}
