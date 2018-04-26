using System;
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

namespace ILCyclomicComplextityCalculator
{
    /// <summary>
    /// ResultItem.xaml 的交互逻辑
    /// </summary>
    public partial class ResultItem : UserControl
    {
        public ResultItem()
        {
            InitializeComponent();
        }




        public string MethodName
        {
            get { return (string) GetValue(MethodNameProperty); }
            set { SetValue(MethodNameProperty, value); }
        }

        public static readonly DependencyProperty MethodNameProperty =
            DependencyProperty.Register("MethodName", typeof(string), typeof(ResultItem), new PropertyMetadata(string.Empty));



        public string ILCC
        {
            get { return (string) GetValue(ILCCProperty); }
            set { SetValue(ILCCProperty, value); }
        }
        public static readonly DependencyProperty ILCCProperty =
            DependencyProperty.Register("ILCC", typeof(string), typeof(ResultItem), new PropertyMetadata(string.Empty));


        public bool PassOrNot
        {
            get { return (bool) GetValue(PassOrNotProperty); }
            set { SetValue(PassOrNotProperty, value); }
        }
        public static readonly DependencyProperty PassOrNotProperty =
            DependencyProperty.Register("PassOrNot", typeof(bool), typeof(ResultItem), new PropertyMetadata(true,PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
            {
                ((ResultItem) sender).Background = Brushes.Tomato;
            }
        }
    }
}
