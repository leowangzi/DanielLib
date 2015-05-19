using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace JetPackedWPFApplication
{
    /// <summary>
    /// Interaction logic for FirstTabControl.xaml
    /// </summary>
    public partial class FirstTabControl : UserControl
    {
        public FirstTabControl()
        {
            this.InitializeComponent();
            this.DataContext = new DemoViewModel();
        }

        public class DemoViewModel : IDataErrorInfo, INotifyPropertyChanged
        {

            public DemoViewModel()
            {
                IsDataInvalid = true;
            }

            private ObservableCollection<string> _listOfItems = new ObservableCollection<string>() { "One", "Two", "Three" };
            public ObservableCollection<string> ListOfItems
            {
                get { return _listOfItems; }
                set { _listOfItems = value; }
            }



            private string invalidProperty;
            public string InvalidProperty
            {
                get
                {
                    return invalidProperty;
                }
                set
                {

                    if (String.Compare(invalidProperty, value, false) == 0)
                        return;
                    invalidProperty = value;
                    RaisePropertyChanged("InvalidProperty");
                }
            }

            private bool _isDataInvalid;
            public bool IsDataInvalid
            {
                get
                {
                    return _isDataInvalid;
                }
                set
                {
                    if (_isDataInvalid == value)
                        return;

                    _isDataInvalid = value;
                    RaisePropertyChanged("IsDataInvalid");
                    InvalidProperty = IsDataInvalid ? "Invalid data" : "valid data";
                }
            }
            public string Error
            {
                get { return "There is an error."; }
            }

            public string this[string columnName]
            {
                get
                {
                    if (IsDataInvalid && String.Compare(columnName, "InvalidProperty", false) == 0)
                        return "InvalidProperty is not valid.. :-(";

                    return string.Empty;
                }
            }


            public virtual void RaisePropertyChanged(string propName)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propName));
            }
            public event PropertyChangedEventHandler PropertyChanged;


        }
    }
}