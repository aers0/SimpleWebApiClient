using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;

namespace SimpleWebApiClient 
{
    class ViewModel : INotifyPropertyChanged
    {


        private List<RootObject> _jsonCollection = null;
        public ICommand ClickCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        public List<RootObject> jsonCollection
        {
            get { return _jsonCollection; }
            set
            {
                if (_jsonCollection != value)
                {
                    _jsonCollection = value;
                    OnPropertyChanged("jsonCollection");

                }
            }
        }

        public ViewModel()
        {
            ClickCommand = new RelayCommand(arg => ClickMethod());
        }
        private void ClickMethod()
        {
            jsonCollection = getWebAPIString();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private List<RootObject> getWebAPIString()
        {

            string json;
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("http://test-aers.somee.com/api/products");
            }
            var model = JsonConvert.DeserializeObject<List<RootObject>>(json);
            return model;
        }
}

   
    public class RootObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
}
