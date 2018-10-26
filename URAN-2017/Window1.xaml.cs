using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace URAN_2017
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
   

        public Window1()
        {
            InitializeComponent();
           
            
        }
       
     
        PointCollection points = new PointCollection();

        public delegate void SizeDelegate();       // Тип делегата   
        public SizeDelegate SizeURANDelegate;
  
   
       
       
      

        private void Button_Click(object sender, RoutedEventArgs e)
        {


             for (int i = 0; i<2000; i++)
             {
                
                // Ch1.SizGr(CanG.ActualWidth, CanG.ActualHeight);
                // Ch1.PoinAdd(i, i);
                int y = 2 * i;
            
            }
            
        }
      

   

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PoLin.StrokeThickness = 1;
            //Ch1.PoinAdd(Convert.ToInt32(XText.Text), Convert.ToInt32(YText.Text));
           
        }
      
      
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        


        }

       
    }
}
