using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;
using CSharp_Project2.ViewModel;
using System.Collections.ObjectModel;
using CSharp_Project2.Model;

namespace CSharp_Project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Constructor that takes object of viewModel
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MessengerViewModel();
        }
    }
}
