﻿using System;
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
                bool hostReachable = await CrossConnectivity.Current.IsRemoteReachable("https://dl.dropboxusercontent.com/");
                if (hostReachable)
                {
                    var client = new System.Net.Http.HttpClient();
                    var response = await client.GetStringAsync("https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
                    //string jsonData = await response.Content.ReadAsStringAsync();
                    var tr = JsonConvert.DeserializeObject<MainData>(response);

                    Title = tr.Title;
                    Items = tr.Rows;
                }
                else
                {
                    MessagingCenter.Send<string>("Error", "HostError");
                }
            }
            else
            {
                MessagingCenter.Send<string>("Error", "ConnectionError");
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

        public Command SortCommand
        {
            get
            {
                return new Command(() => { SortData(); });
            }
        }

        public Command LoadFromFileCommand
        {
            get
            {
                return new Command(() => { LoadFromFile(); });
            }
        }

        private void LoadFromFile()
        {
            var assembly = typeof(MyDataListPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("facts.json");

            using (var reader = new System.IO.StreamReader(stream))
            {

                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<MainData>(json);
                Title = data.Title;
                Items = data.Rows;
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