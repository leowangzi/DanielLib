using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for ThirdTabControl.xaml
    /// </summary>
    public partial class ThirdTabControl : UserControl
    {
        static ThirdTabControl()
        {
            //var style = Application.Current.FindResource(typeof (CheckBox));
            // DataGridCheckBoxColumn.ElementStyleProperty.OverrideMetadata(typeof(DataGridCheckBoxColumn), new FrameworkPropertyMetadata(style));
            
    }
        public ThirdTabControl()
        {
            InitializeComponent();
        }
    }
}
