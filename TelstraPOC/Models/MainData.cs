using System;
using System.Collections.ObjectModel;
namespace TelstraPOC.Models
{
    public class MainData
    {

        public string Title { get; set; }
        public ObservableCollection<MyDataDetails> Rows { get; set; }

    }
}
