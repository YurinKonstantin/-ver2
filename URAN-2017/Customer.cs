using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;

namespace URAN_2017
{
    class Customer : INotifyPropertyChanged
    {
      
 
     
        private string _customerTime;
        public string CustomerTime
        {
            get
            {
                return this._customerTime;
            }
            set
            {
                this._customerTime = value;
                this.OnPropertyChanged(nameof(CustomerTime));

            }
        }
     
        public event PropertyChangedEventHandler PropertyChanged;
        
     
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
   
}
