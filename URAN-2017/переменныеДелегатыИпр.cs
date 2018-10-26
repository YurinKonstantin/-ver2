using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace URAN_2017
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        Customer customer = new Customer();
        ClassParentsBAAK bv = new ClassParentsBAAK();

        public ObservableCollection<BAAK12T> _DataColecViev;
        public ObservableCollection<ClassBAAK12NoTail> _DataColecVievList2;
        
        
             
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            string kl1;

            public string Kl1
            {
                get
                {
                    return kl1;
                }
                set
                {
                    kl1 = value;
                    this.OnPropertyChanged(nameof(value));
                }
            }
            string kl2;
            public string Kl2
            {
                get
                {
                    return kl2;
                }
                set
                {
                    kl2 = value;
                    this.OnPropertyChanged(nameof(value));
                }
            }
        
        public delegate void ConnectDelegate();       // Тип делегата   
        public ConnectDelegate ConnnectURANDelegate;
        public delegate void DiscConnectDelegate();       // Тип делегата   
        public DiscConnectDelegate DiscConnnectURANDelegate;
        public delegate void НастройкаUranDelegate();
        public НастройкаUranDelegate НастройкаURANDelegate;
        public delegate void ДеИнсталяция();
        public ДеИнсталяция ДеИнсталяцияDelegate;   
        BAAK12T Кластер1;
        BAAK12T Кластер2;
       
        BAAK12T Кластер3;
        BAAK12T Кластер4;
        BAAK12T Кластер5;
        BAAK12T Кластер6;
        BAAK12T Кластер7;
        BAAK12T Кластер8;
        BAAK12T Кластер9;

        ClassBAAK12NoTail Кластер1_2;
        ClassBAAK12NoTail Кластер2_2;

        ClassBAAK12NoTail Кластер3_2;
        ClassBAAK12NoTail Кластер4_2;
        ClassBAAK12NoTail Кластер5_2;
        ClassBAAK12NoTail Кластер6_2;
        ClassBAAK12NoTail Кластер7_2;
        ClassBAAK12NoTail Кластер8_2;
        ClassBAAK12NoTail Кластер9_2;
        ClassMC MS;
        ClassMC MS1;
        CancellationTokenSource cancellationTokenSource;
        //DateTime alarm;
        DateTime temp;
        public delegate void ДеИнсталяция1();
        public ДеИнсталяция ДеИнсталяцияDelegate1;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
