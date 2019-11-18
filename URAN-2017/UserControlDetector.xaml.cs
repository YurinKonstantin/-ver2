﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для UserControlDetector.xaml
    /// </summary>
    public partial class UserControlDetector : UserControl
    {
        public UserControlDetector()
        {
            InitializeComponent();
        }
        public SolidColorBrush _Dnet;
        public SolidColorBrush Dnet
        {
            get
            {
                return _Dnet;
            }
            set
            {
                _Dnet = value;
                Dneutron.Fill = value;
            }
        }
        public SolidColorBrush _Dsig;
        public SolidColorBrush Dsig
        {
            get
            {
                return _Dsig;
            }
            set
            {
                _Dsig = value;
                DSig.Fill = value;
            }
        }

    }
}
