using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JetPackedWPFApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ListOfItems = new List<String>() { "Item 1", "Item 2", "Item 3" };
            DataContext = this;

            ListOfComplexItems = new ObservableCollection<ComplexItem>();
            for (int i = 0; i < 50; i++)
            {
                ListOfComplexItems.Add(new ComplexItem()
                {
                    Name = "item " + i,
                    Value = i + 1,
                    Description = "this is the item n°" + i,
                    IsChecked = (i % 2 == 0)
                });
            }

            DataGrid grid;
            DataGridRowsPresenter dd;
        }

        public IList<String> ListOfItems { get; set; }
        public IList<ComplexItem> ListOfComplexItems { get; set; }
    }
}
