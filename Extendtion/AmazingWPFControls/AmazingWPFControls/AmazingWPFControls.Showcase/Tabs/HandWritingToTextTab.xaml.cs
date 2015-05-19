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
using AmazingWPFControls.HandWritingToText;
using System.Collections.ObjectModel;

namespace AmazingWPFControls.Showcase
{
    /// <summary>
    /// Interaction logic for HandWritingToTextTab.xaml
    /// </summary>
    public partial class HandWritingToTextTab : UserControl
    {

        private ObservableCollection<String> _RecognizedWords = new ObservableCollection<string>();
        public ObservableCollection<String> RecognizedWords
        {
            get { return _RecognizedWords; }
            private set { _RecognizedWords = value; }
        }

        public HandWritingToTextTab()
        {
            this.InitializeComponent();
            DataContext = this;
        }

        private void HandWritingToText_TextEntered(object sender, TextEnteredEventArgs e)
        {
            string textToAdd = String.Format("{0:D4} : {1} ", regognizedTextListBox.Items.Count, e.TextEntered);
            RecognizedWords.Insert(0, textToAdd);
            regognizedTextListBox.SelectedItem = textToAdd;
        }


    }
}