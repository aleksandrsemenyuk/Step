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

namespace DZ_Tree_Proc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            myTreeViewItem root = new myTreeViewItem { Title = "Root" };
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
    public class myTreeViewItem
    {
        public List<myTreeViewItem> Items { set; get; }
        public string Title { set; get; }

        public myTreeViewItem()
        {
            Items = new List<myTreeViewItem>();
        }
    }
}
